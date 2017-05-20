using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace FileEtl.ShadowCopy
{
    public class ShadowFileCopier
    {
        public static bool FilesChanged(string[] sourceDirectory, string targetDirectory)
        {
            var joinedFiles = JoinedSourceAndTargetDirectoryFiles(sourceDirectory, targetDirectory);
            return joinedFiles.Any(AreDifferent);
        }

        public static void Copy(string[] sourceDirecties, string shadowDirectory, bool createDirectoryIfMissing, int filesunchangedWaitingTimeMilliseconds = 0)
        {
            if (createDirectoryIfMissing && !Directory.Exists(shadowDirectory))
            {
                Directory.CreateDirectory(shadowDirectory);
            }
            else if (!Directory.Exists(shadowDirectory))
            {
                throw new System.Exception($"Directory is missing: '{shadowDirectory}'");
            }

            while (filesunchangedWaitingTimeMilliseconds > 0 && FilesChanged(sourceDirecties, shadowDirectory))
            {
                Copy(sourceDirecties, shadowDirectory);
                Thread.Sleep(filesunchangedWaitingTimeMilliseconds);
            }
        }

        private static IEnumerable<SourceAndTargetFile> JoinedSourceAndTargetDirectoryFiles(string[] sourceDirectory, string targetDirectory)
        {
            var sourceDirectoryFiles = sourceDirectory
                .SelectMany(x => Directory.GetFiles(x, "*.*", SearchOption.TopDirectoryOnly))
                .Select(x => new FileInfo(x));

            // TODO validate uniqueness of filenames
            var targetdirectoryFiles = Directory.GetFiles(targetDirectory, "*.*", SearchOption.TopDirectoryOnly)
                .Select(x => new FileInfo(x));
            return LinqExtensions.OuterJoin(sourceDirectoryFiles, targetdirectoryFiles, x => x.Name, SourceAndTargetFile.New);
        }

        private static bool AreDifferent(SourceAndTargetFile arg)
        {
            // TODO optimistic maybe compare files byte by byte, but this will be slower...
            return arg.SourceFile == null ||
                arg.TargetFile == null ||
                arg.SourceFile.LastWriteTimeUtc != arg.TargetFile.LastWriteTimeUtc;
        }

        public static void Copy(string[] sourceDirectory, string targetDirectory)
        {
            var joinedFiles = JoinedSourceAndTargetDirectoryFiles(sourceDirectory, targetDirectory);
            var differentFiles = joinedFiles.Where(AreDifferent);
            foreach (var differentFile in differentFiles)
            {
                if (differentFile.SourceFile == null)
                {
                    differentFile.TargetFile.Delete();
                }
                else
                {
                    var targetFile = Path.Combine(targetDirectory, differentFile.SourceFile.Name);
                    differentFile.SourceFile.CopyTo(targetFile, true);
                }
            }
        }

        private class SourceAndTargetFile
        {
            public static SourceAndTargetFile New(FileInfo sourceFile, FileInfo targetFile)
            {
                return new SourceAndTargetFile { SourceFile = sourceFile, TargetFile = targetFile };
            }

            public FileInfo SourceFile { get; set; }

            public FileInfo TargetFile { get; set; }
        }
    }
}
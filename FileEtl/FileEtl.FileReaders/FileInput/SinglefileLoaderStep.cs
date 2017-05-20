using System.IO;
using FileEtl.Core;

namespace FileEtl.FileReaders.FileInput
{
    public class SingleFileLoaderStep : IEtlStep, IConfigurableEtlStep<SinglefileLoaderStepConfig>
    {
        public SinglefileLoaderStepConfig Configuration { get; set; }

        [EtlStepMethod]
        public FileInfo LoadFileInfo()
        {
            return new FileInfo(Configuration.FileName);
        }
    }
}
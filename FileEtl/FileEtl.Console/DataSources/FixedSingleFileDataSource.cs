using System;
using System.Collections.Generic;
using System.IO;

namespace FileEtl.Console.DataSources
{
    public class FixedSingleFileDataSource : IDataSource<IEnumerable<FileInfo>>, IConfigurable<FixedSingleFileDataSourceConfig>
    {
        public FixedSingleFileDataSourceConfig Configuration { get; set; }

        public string Name { get; set; } = "FixedSingleFileDataSource";

        public FileInfo Run()
        {
            return new FileInfo(Configuration.FileName);
        }

        IEnumerable<FileInfo> IDataSource<IEnumerable<FileInfo>>.Run()
        {
            throw new NotImplementedException();
        }
    }
}
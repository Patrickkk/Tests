using System;
using System.Collections.Generic;
using FileEtl.FileReaders.DataTableSetup;

namespace FileEtl.FileReaders.FlatFile
{
    public class FlatFileReaderConfiguration : IDataTableConfig
    {
        public string[] RecordSeperators { get; set; } = { "\r\n", "\r", "\n" };

        public List<IDataTableRecord> Records => throw new NotImplementedException();
    }
}
using System;
using FileEtl.FileReaders.DataTableSetup;

namespace FileEtl.FileReaders.Csv
{
    public class CsvField : IDataRecordField
    {
        public string Name { get; set; } = "__Undefined";

        public Type Type { get; set; } = typeof(string);

        public int Position { get; set; } = -1;

        public object DefaultValue { get; set; } = null;
    }
}
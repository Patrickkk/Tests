using System;
using FileEtl.FileReaders.DataTableSetup;

namespace FileEtl.FileReaders.Csv
{
    public class CsvField : IDataRecordField
    {
        public object DefaultValue { get; set; } = null;

        public string Name { get; set; } = "__Undefined";

        public int Position { get; set; } = -1;

        public Type Type { get; set; } = typeof(string);

        internal static CsvField DefaultExampleConfigField()
        {
            return new CsvField { Name = "OrderNumber", Type = typeof(string) };
        }
    }
}
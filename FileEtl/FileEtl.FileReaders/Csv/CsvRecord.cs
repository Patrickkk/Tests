using System.Collections.Generic;

namespace FileEtl.FileReaders.Csv
{
    public class CsvRecord
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TableName { get; set; }

        public bool Required { get; set; } = true;

        public List<CsvField> Fields { get; set; } = new List<CsvField>();
    }
}
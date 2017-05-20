using System.Collections.Generic;
using System.Text;

namespace FileEtl.FileReaders.Csv
{
    public class CsvReaderConfiguration
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public List<CsvRecord> Records { get; set; } = new List<CsvRecord>();

        public bool SkipHeader { get; set; } = false;

        public bool SkipEmptyRecords { get; set; } = false;
    }
}
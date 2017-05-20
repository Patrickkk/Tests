using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.FileReaders.Csv
{
    public class SingleRecordCsvRecordReaderSelector : ICsvRecordSelector
    {
        private CsvRecord Record { get; set; }

        public CsvRecord SelectRecordForRow(List<CsvRecord> records, CsvReaderConfiguration config, string[] lineData)
        {
            if (!records.Any())
            {
                throw new Exception($"No records found.");
            }
            if (records.Count > 1)
            {
                throw new Exception($"Multiple records found with but SingleRecordCsvRecordReaderSelector supports only one.");
            }
            return records.Single();
        }
    }
}
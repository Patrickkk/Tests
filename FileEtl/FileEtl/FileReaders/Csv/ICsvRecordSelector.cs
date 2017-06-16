using System.Collections.Generic;

namespace FileEtl.FileReaders.Csv
{
    public interface ICsvRecordSelector
    {
        CsvRecord SelectRecordForRow(List<CsvRecord> records, CsvReaderConfiguration config, string[] lineData);
    }
}
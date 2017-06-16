using System.Collections.Generic;
using System.Linq;
using FileEtl.FileReaders.DataTableSetup;

namespace FileEtl.FileReaders.Csv
{
    public class CsvRecord : IDataTableRecord
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string TableName { get; set; }

        public bool Required { get; set; } = true;

        public List<IDataRecordField> Fields { get { return CsvFields.OfType<IDataRecordField>().ToList(); } }

        public List<CsvField> CsvFields { get; set; } = new List<CsvField>();
    }
}
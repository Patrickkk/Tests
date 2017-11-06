using System;
using System.Collections.Generic;
using System.Linq;
using FileEtl.FileReaders.DataTableSetup;
using Newtonsoft.Json;

namespace FileEtl.FileReaders.Csv
{
    public class CsvRecord : IDataTableRecord
    {
        public List<CsvField> CsvFields { get; set; } = new List<CsvField>();
        public string Description { get; set; }

        [JsonIgnore]
        public List<IDataRecordField> Fields { get { return CsvFields.OfType<IDataRecordField>().ToList(); } }

        public string Name { get; set; }
        public bool Required { get; set; } = true;
        public string TableName { get; set; }

        public static CsvRecord DefaultExampleConfigRecord()
        {
            return new CsvRecord
            {
                TableName = "StagedOrders",
                CsvFields = new List<CsvField> { CsvField.DefaultExampleConfigField() },
                Description = "All fields used for stagedorder creation",
                Name = "StagedOrders"
            };
        }
    }
}
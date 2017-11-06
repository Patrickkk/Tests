using System.Collections.Generic;
using System.Linq;
using FileEtl.FileReaders.DataTableSetup;
using SoftFluent.Windows;
using Newtonsoft.Json;

namespace FileEtl.FileReaders.Csv
{
    [PropertyGridOptions(EditorDataTemplateResourceKey = "ObjectEditor", ForcePropertyChanged = true)]
    public class CsvReaderConfiguration : IDataTableConfig
    {
        public List<CsvRecord> CsvRecords { get; set; } = new List<CsvRecord>();
        public string Encoding { get; set; } = "UTF8";

        [JsonIgnore]
        public List<IDataTableRecord> Records { get { return CsvRecords.OfType<IDataTableRecord>().ToList(); } }

        public bool SkipEmptyRecords { get; set; } = false;
        public bool SkipHeader { get; set; } = false;

        public static List<CsvRecord> DefaultExampleconfigRecords()
        {
            return new List<CsvRecord> {
                CsvRecord.DefaultExampleConfigRecord()
            };
        }
    }
}
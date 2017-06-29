using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileEtl.FileReaders.DataTableSetup;
using SoftFluent.Windows;

namespace FileEtl.FileReaders.Csv
{
    [PropertyGridOptions(EditorDataTemplateResourceKey = "ObjectEditor", ForcePropertyChanged = true)]
    public class CsvReaderConfiguration : IDataTableConfig
    {
        public Encoding Encoding { get; set; } = Encoding.UTF8;

        public List<CsvRecord> CsvRecords { get; set; } = new List<CsvRecord>();

        public bool SkipHeader { get; set; } = false;

        public bool SkipEmptyRecords { get; set; } = false;

        public List<IDataTableRecord> Records { get { return CsvRecords.OfType<IDataTableRecord>().ToList(); } }
    }
}
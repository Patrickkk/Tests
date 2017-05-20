using System.Data;
using System.IO;
using CsvHelper;

namespace FileEtl.FileReaders.Csv
{
    public class CsvfileReader
    {
        private readonly ICsvRecordSelector recordSelector;

        public CsvfileReader(ICsvRecordSelector recordSelector)
        {
            this.recordSelector = recordSelector;
        }

        public DataSet ReadOrderFile(FileInfo file, CsvReaderConfiguration config)
        {
            var csvHelperConfig = new CsvHelper.Configuration.CsvConfiguration { SkipEmptyRecords = config.SkipEmptyRecords, ThrowOnBadData = true };
            var result = DataTableCreator.CreateDataSetForConfig(config);
            using (var parser = new CsvParser(file.OpenText(), csvHelperConfig))
            {
                while (true)
                {
                    if (config.SkipHeader)
                    {
                        SkipFirstHeaderLine(parser);
                    }
                    var row = parser.Read();
                    if (row == null)
                    {
                        break;
                    }
                    var recordType = recordSelector.SelectRecordForRow(config.Records, config, row);
                    var table = result.Tables[recordType.TableName];
                    var newRow = table.NewRow();
                    CsvFieldMapper.Mapfields(row, recordType, newRow);
                    table.Rows.Add(newRow);
                }
            }
            return result;
        }

        private static void SkipFirstHeaderLine(CsvParser parser)
        {
            parser.Read();
        }
    }
}
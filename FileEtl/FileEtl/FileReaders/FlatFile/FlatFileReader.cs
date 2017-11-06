using System.Data;
using System.IO;

namespace FileEtl.FileReaders.FlatFile
{
    public class FlatFileReader
    {
        public DataSet ReadOrderFile(FileInfo file, FlatFileReaderConfiguration configuration)
        {
            var dataset = DataTableCreator.CreateDataSetForConfig(configuration);

            //using (var parser = new FlatFileParser(file.OpenText()))
            //{
            //    while (true)
            //    {
            //        var row = parser.Read();
            //        if (row == null)
            //        {
            //            break;
            //        }
            //        var recordType = recordSelector.SelectRecordForRow(config.CsvRecords, config, row);
            //        var table = result.Tables[recordType.TableName];
            //        var newRow = table.NewRow();
            //        CsvFieldMapper.Mapfields(row, recordType, newRow);
            //        table.Rows.Add(newRow);
            //    }
            //}
            //return result;

            return dataset;
        }
    }
}
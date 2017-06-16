using System.Collections.Generic;
using System.Data;
using FileEtl.FileReaders.DataTableSetup;

namespace FileEtl.FileReaders
{
    public class DataTableCreator
    {
        public static DataSet CreateDataSetForConfig(IDataTableConfig config)
        {
            var dataset = new DataSet();
            foreach (var record in config.Records)
            {
                CreateTableforRecord(dataset, record);
            }
            return dataset;
        }

        private static void CreateTableforRecord(DataSet dataset, IDataTableRecord record)
        {
            // TODO unit test
            var table = new DataTable(record.TableName);
            table.Columns.AddRange(CreateColumnsForRecord(record));
            dataset.Tables.Add(table);
        }

        private static DataColumn[] CreateColumnsForRecord(IDataTableRecord record)
        {
            var collumns = new List<DataColumn>();
            foreach (var field in record.Fields)
            {
#pragma warning disable CC0022 // Should dispose object is not required for datacolumn but is an artifact of marshalbyref.
                var collumn = new DataColumn(field.Name, field.Type)
                {
                    DefaultValue = field.DefaultValue
                };
                collumns.Add(collumn);
#pragma warning restore CC0022 // Should dispose object
            }
            return collumns.ToArray();
        }
    }
}
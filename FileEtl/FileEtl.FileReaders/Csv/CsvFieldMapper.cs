using System.Data;

namespace FileEtl.FileReaders.Csv
{
    public class CsvFieldMapper
    {
        public static void Mapfields(string[] values, CsvRecord record, DataRow row)
        {
            // TODO take inejctable dependency to parse different types
            foreach (var field in record.Fields)
            {
                if (values.Length >= field.Position)
                {
                    row[field.Name] = values[field.Position - 1];
                }
            }
        }
    }
}
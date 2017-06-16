using System.Data;
using System.IO;

namespace FileEtl.FileReaders.FlatFile
{
    public class FlatFileReader
    {
        public DataSet ReadOrderFile(FileInfo input, FlatFileReaderConfiguration configuration)
        {
            var dataset = DataTableCreator.CreateDataSetForConfig(configuration);
            return dataset;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using FileEtl.Console.DataSources;

namespace FileEtl.Console.Transformers
{
    public class FlatFileReader : ITransformer<IEnumerable<FileInfo>, DataSet>, IConfigurable<FlatFileReaderConfiguration>
    {
        public FlatFileReaderConfiguration Configuration { get; set; }

        public string Name { get; set; } = "FlatFileReader";

        public DataSet Transform(IEnumerable<FileInfo> input)
        {
            // TODO automatic loading of most specific config. if anything is found in plugin it will override exsisting
            var dataSet = CreateDataset();
            foreach (var file in input)
            {
                ReadFile(file, dataSet);
            }
            return dataSet;
        }

        private void ReadFile(FileInfo file, DataSet dataSet)
        {
            throw new NotImplementedException();
        }

        private static DataSet CreateDataset()
        {
            return new DataSet();
        }
    }
}
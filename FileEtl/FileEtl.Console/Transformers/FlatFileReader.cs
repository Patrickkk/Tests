using System.Collections.Generic;
using System.Data;
using System.IO;

namespace FileEtl.Console.Transformers
{
    public class FlatFileReader : ITransformer<IEnumerable<FileInfo>, DataSet>
    {
        public string Name { get; set; } = "FlatFileReader";
    }
}
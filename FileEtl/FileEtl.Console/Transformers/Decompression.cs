using System;
using System.Collections.Generic;
using System.IO;

namespace FileEtl.Console.Transformers
{
    public class Decompression : ITransformer<IEnumerable<FileInfo>, IEnumerable<FileInfo>>
    {
        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IEnumerable<FileInfo> Transform(IEnumerable<FileInfo> input)
        {
            throw new NotImplementedException();
        }
    }
}
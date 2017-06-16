using System.Data;
using System.IO;
using FileEtl.Core;

namespace FileEtl.FileReaders.FlatFile
{
    internal class FlatFileReaderEtlStep : IEtlStep, IConfigurableEtlStep<FlatFileReaderConfiguration>
    {
        private readonly FlatFileReader fileReader;

        public FlatFileReaderEtlStep(FlatFileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public FlatFileReaderConfiguration Configuration { get; set; }

        [EtlStepRunMethod]
        public DataSet ReadFlatFile(FileInfo input)
        {
            return this.fileReader.ReadOrderFile(input, Configuration);
        }
    }
}
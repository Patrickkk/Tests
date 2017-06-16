using System.Data;
using System.IO;
using FileEtl.Core;

namespace FileEtl.FileReaders.Csv
{
    public class CsvReaderEtlStep : IEtlStep, IConfigurableEtlStep<CsvReaderConfiguration>
    {
        private readonly CsvfileReader fileReader;

        public CsvReaderEtlStep(CsvfileReader fileReader)
        {
            this.fileReader = fileReader;
        }

        public CsvReaderConfiguration Configuration { get; set; }

        [EtlStepRunMethod]
        public DataSet ReadCsvFile(FileInfo input)
        {
            return this.fileReader.ReadOrderFile(input, Configuration);
        }
    }
}
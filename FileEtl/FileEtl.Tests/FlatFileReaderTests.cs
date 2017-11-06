using System.Collections.Generic;
using FileEtl.Core;
using FileEtl.FileReaders.Csv;
using FileEtl.FileReaders.FileInput;
using FileEtl.FileReaders.FlatFile;
using SimpleInjector;
using Xunit;

namespace FileEtl.Tests
{
    public class FlatFileReaderTests
    {
        [Fact]
        public void X()
        {
            var fileReaderConfig = new SinglefileLoaderStepConfig { FileName = "TestCsv.csv" };
            var csvConfig = new FlatFileReaderConfiguration { };

            var etlPipelineConfiguration = new List<ConfiguredEtlStep>
            {
                new ConfiguredEtlStep{ StepType = typeof(SingleFileLoaderStep), Config = fileReaderConfig},
                new ConfiguredEtlStep{ StepType = typeof(CsvReaderEtlStep), Config = csvConfig}
            };

            var container = new Container();
            container.Register<CsvReaderEtlStep>();
            container.Register<SingleFileLoaderStep>();
            container.Register<ICsvRecordSelector, SingleRecordCsvRecordReaderSelector>();
            var pipeline = EtlProcessFactory.CreateEtlPipeline(container, etlPipelineConfiguration);
            PipelineExecutor.RunPipeline(pipeline, x => { }, x => { });
        }
    }
}
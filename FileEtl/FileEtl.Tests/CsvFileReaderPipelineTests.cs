using System;
using System.Collections.Generic;
using FileEtl.Core;
using FileEtl.FileReaders.Csv;
using FileEtl.FileReaders.FileInput;
using SimpleInjector;
using Xunit;

namespace FileEtl.Tests
{
    public class CsvFileReaderPipelineTests
    {
        [Fact]
        public void Test()
        {
            var fileReaderConfig = new SinglefileLoaderStepConfig { FileName = "TestCsv.csv" };
            var csvConfig = new CsvReaderConfiguration
            {
                SkipHeader = true,
                CsvRecords =
                {
                    new CsvRecord{
                        Name = "Test",
                        TableName = "Data",
                        Fields = {
                            new CsvField { Name = "Field1",Position= 1, DefaultValue = "", Type = typeof(string)},
                            new CsvField { Name = "Field2",Position= 2, DefaultValue = null, Type = typeof(DateTime)},
                            new CsvField { Name = "Field3",Position= 3, DefaultValue = 1, Type = typeof(int)},
                        }
                    }
                }
            };

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
            PipelineExecutor.RunPipeline(pipeline, context => { }, context => { });
        }
    }
}
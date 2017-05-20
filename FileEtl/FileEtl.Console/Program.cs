using System;
using System.Collections.Generic;
using System.Linq;
using FileEtl.Core;
using FileEtl.FileReaders.Csv;
using Newtonsoft.Json;

namespace FileEtl.Console
{
    internal class Program
    {
        public static List<EtlStepconfiguration> EtlSteps = new List<EtlStepconfiguration>();
        private static Type[] etlStepTypes;

        private static void Main(string[] args)
        {
            // TODO load all assemblies. that might contains FileEtl
            CsvRecord a = new CsvRecord();
            etlStepTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x => x.GetTypes()).EtlSteps().ToArray();
            WriteAllAvailableEtlSteps();
            while (true)
            {
                WriteAvailableDataTypes();
                var command = System.Console.ReadLine();
                if (command == "q")
                {
                    break;
                }
                else
                {
                    ProcessCommand(command);
                }
            }
        }

        private static void WriteAllAvailableEtlSteps()
        {
            foreach (var type in etlStepTypes)
            {
                System.Console.WriteLine($"type: {type.Name} - {type.EtlStepMethod().ReturnType} {type.EtlStepMethod().Name}({string.Join(",", type.EtlStepMethod().GetParameters().Select(x => x.ParameterType.Name + " " + x.Name))})");
            }
        }

        private static void ProcessCommand(string command)
        {
            var parts = command.Split(' ');
            switch (parts[0].ToLower())
            {
                case "add":
                    AddStep(command);
                    break;

                case "list":
                    WriteCurrentEtlSteps();
                    break;

                case "config":
                    ConfigureStep(command);
                    break;

                case "configfromfiles":
                    ConfigureStep(command);
                    break;

                case "run":
                    Run();
                    break;

                default:
                    break;
            }
        }

        private static void Run()
        {
            //var pipeline = EtlProcessFactory.CreateEtlPipeline(EtlSteps);
        }

        private static void WriteCurrentEtlSteps()
        {
            foreach (var step in EtlSteps)
            {
                System.Console.WriteLine($"type: {step.StepType.Name} - {step.StepType.EtlStepMethod().ReturnType} {step.StepType.EtlStepMethod().Name}({string.Join(",", step.StepType.EtlStepMethod().GetParameters().Select(x => x.ParameterType.Name + " " + x.Name))})");
            }
        }

        private static void ConfigureStep(string command)
        {
            var trimmed = command.TrimStart("config".ToCharArray()).TrimStart(' ');
            var positionText = trimmed.Substring(0, trimmed.IndexOf(' '));
            var position = int.Parse(positionText);
            var json = trimmed.Replace(positionText, "").Trim();

            //if (File.Exists())
            //{
            //}
            var step = EtlSteps[position - 1];
            var configType = step.GetType().GetGenericInterfaceTypeArguments(typeof(IConfigurableEtlStep<>)).ElementAt(0);
            var config = JsonConvert.DeserializeObject(json, configType);
        }

        private static void AddStep(string command)
        {
            var parts = command.Split(' ');
            var types = etlStepTypes.Where(type => type.Name.Contains(parts[1]));

            if (types.None())
            {
                System.Console.WriteLine("No Step found");
                return;
            }

            Type etlStepType;
            if (types.Count() > 1)
            {
                System.Console.WriteLine("Multiple steps found. select index");
                foreach (var type in types)
                {
                    System.Console.WriteLine(type.Name);
                }
                var pos = int.Parse(System.Console.ReadLine());
                etlStepType = types.ElementAt(pos);
            }
            else
            {
                etlStepType = types.Single();
            }

            var position = EtlSteps.Count;

            if (parts.Count() > 2)
            {
                position = int.Parse(parts[2]);
            }
            EtlSteps.Insert(position, new EtlStepconfiguration { StepType = etlStepType, Config = new object() });
        }

        private static void WriteAvailableDataTypes()
        {
            //var availableData = factory.AvailableDataTypesAtStep(factory.EtlSteps.Count);
            //var availableSteps = GetStepsForData(availableData);
            //System.Console.WriteLine("available data:" + string.Join(", ", availableData));
            //System.Console.WriteLine("available steps:" + string.Join(", ", availableSteps));
        }

        private static IEnumerable<Type> GetStepsForData(IEnumerable<Type> availableData)
        {
            throw new NotImplementedException();

            //return types.Where(x => x.ImplementsOpenGenericInterface(typeof(IDataSource<>)))
            //    .Concat(types.Where(x => x.ImplementsOpenGenericInterface(typeof(ITransformer<,>)))
            //                 .Where(x => availableData.Contains(x.GetTransformerInputType())));
        }
    }
}
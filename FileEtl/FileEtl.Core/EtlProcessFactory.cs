using System;
using System.Collections.Generic;
using SimpleInjector;

namespace FileEtl.Core
{
    /// <summary>
    /// build a list of ETL steps from a list of ETL step configurations
    /// </summary>
    public class EtlProcessFactory
    {
        public static List<IEtlStep> CreateEtlPipeline(Container container, List<EtlStepconfiguration> etlSteps)
        {
            var availableInputTypes = new HashSet<Type>();
            return CreateEtlPipeline(container, etlSteps, availableInputTypes);
        }

        public static void Validate()
        {
            throw new NotImplementedException();// TODO just run the createEtlpipeline.
        }

        private static List<IEtlStep> CreateEtlPipeline(Container container, List<EtlStepconfiguration> etlSteps, HashSet<Type> availableInputTypes)
        {
            var pipeline = new List<IEtlStep>();
            foreach (var stepconfiguration in etlSteps)
            {
                var etlStep = (IEtlStep)container.GetInstance(stepconfiguration.StepType);

                // TODO refactor into seperate extensionmethod
                if (stepconfiguration.StepType.ImplementsIConfigurableEtlStep())
                {
                    // TODO refactor into seperate extensionmethod
                    var configurationType = stepconfiguration.StepType.GetIConfigurableConfigurationType();

                    // TODO validate configurationtype with object
                    var configurationProperty = stepconfiguration.StepType.GetProperty("Configuration");
                    configurationProperty.SetValue(etlStep, stepconfiguration.Config);
                }
                pipeline.Add(etlStep);
            }
            return pipeline;
        }
    }
}
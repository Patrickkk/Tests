using System;
using System.Collections.Generic;

namespace FileEtl.Core
{
    /// <summary>
    /// Has Methods to execute a pipline
    /// </summary>
    public class PipelineExecutor
    {
        public static void RunPipeline(List<IEtlStep> steps, params object[] inputForFirstStep)
        {
            Dictionary<Type, object> data = GetInitialData();
            foreach (var step in steps)
            {
                StepWithInput(step, data);
            }
        }

        private static Dictionary<Type, object> GetInitialData(params object[] inputForFirstStep)
        {
            var data = new Dictionary<Type, object>();
            foreach (var inputValu in inputForFirstStep)
            {
                data.Add(inputValu.GetType(), inputValu);
            }
            return data;
        }

        private static void StepWithoutInput(IEtlStep step, Dictionary<Type, object> Data)
        {
            // TODO all kinds of validation
            var runMethod = step.GetType().GetEtlStepRunMethod();
            var outputType = runMethod.ReturnType;
            var result = runMethod.Invoke(step, null);
            Data.Add(result.GetType(), result);
        }

        private static void StepWithInput(IEtlStep step, Dictionary<Type, object> Data)
        {
            // TODO all kinds of validation
            var runMethod = step.GetType().GetEtlStepRunMethod();
            var outputType = runMethod.ReturnType;
            var inputTypes = step.GetType().GetEtlRunMethodInputTypes();
            var parameters = GetParameters(inputTypes, Data);
            var result = runMethod.Invoke(step, parameters.ToArray());
            Data.Add(result.GetType(), result);
        }

        private static List<object> GetParameters(IEnumerable<Type> inputTypes, Dictionary<Type, object> data)
        {
            var parameterValues = new List<object>();
            foreach (var type in inputTypes)
            {
                if (data.ContainsKey(type))
                {
                    parameterValues.Add(data[type]);
                }
                else
                {
                    throw new Exception($"No data found for type {type}");
                }
            }
            return parameterValues;
        }
    }
}
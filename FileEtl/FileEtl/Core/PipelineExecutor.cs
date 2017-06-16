using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Core
{
    /// <summary>
    /// Has Methods to execute a pipline
    /// </summary>
    public class PipelineExecutor
    {
        public static void RunPipeline(List<IEtlStep> steps)
        {
            var data = new Dictionary<Type, object>();
            foreach (var step in steps)
            {
                StepWithInput(step, data);
            }
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
            var result = runMethod.Invoke(step, parameters);
            Data.Add(result.GetType(), result);
        }

        private static object[] GetParameters(Type[] inputTypes, Dictionary<Type, object> data)
        {
            var parameterValues = new object[inputTypes.Length];
            for (int i = 0; i < inputTypes.Count(); i++)
            {
                var type = inputTypes[i];
                if (data.ContainsKey(type))
                {
                    parameterValues[i] = data[type];
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
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace FileEtl.Core
{
    /// <summary>
    /// Has Methods to execute a pipline
    /// </summary>
    public class PipelineExecutor
    {
        public static EtlPipelineContext RunPipeline(List<IEtlStep> steps, Action<EtlPipelineContext> beforeEtlStep, Action<EtlPipelineContext> afterEtlStep, params object[] inputForFirstStep)
        {
            var initialData = GetInitialData(inputForFirstStep);
            var initialcontext = new EtlPipelineContext(steps, initialData, null);
            var stepsResult = steps.Aggregate(initialcontext, (context, step) =>
            {
                beforeEtlStep?.Invoke(context);
                var postStepContext = StepWithInput(step, context);
                afterEtlStep?.Invoke(postStepContext);
                return postStepContext;
            });
            return stepsResult;
        }

        private static ImmutableDictionary<Type, object> GetInitialData(params object[] inputForFirstStep)
        {
            var data2 = ImmutableDictionary<Type, object>.Empty;
            return data2.AddRange(inputForFirstStep.Select(x => new KeyValuePair<Type, object>(x.GetType(), x)));
        }

        private static EtlPipelineContext StepWithInput(IEtlStep step, EtlPipelineContext context)
        {
            // TODO all kinds of validation
            var runMethod = step.GetType().GetEtlStepRunMethod();
            var outputType = runMethod.ReturnType;
            var inputTypes = step.GetType().GetEtlRunMethodInputTypes();
            var parameters = GetParameters(inputTypes, context.CurrentData);
            var result = runMethod.Invoke(step, parameters.ToArray());
            return new EtlPipelineContext(context.EtlSteps, context.CurrentData.Add(outputType, result), step);
        }

        private static List<object> GetParameters(IEnumerable<Type> inputTypes, IReadOnlyDictionary<Type, object> data)
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
using FunctionalSharp.DiscriminatedUnions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Console
{
    public class EtlProcessFactory
    {
        public DiscriminatedUnionWithBaseList<IDataSource, ITransformer, IEtlStep> EtlSteps = new DiscriminatedUnionWithBaseList<IDataSource, ITransformer, IEtlStep>();

        public void Add(IDataSource etlStep)
        {
        }

        public void Add(ITransformer etlStep)
        {
        }

        public IEnumerable<Type> AvailableDataTypesAtStep(DiscriminatedUnionWithBase<IDataSource, ITransformer, IEtlStep> step)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Type> AvailableDataTypesAtStep(int position)
        {
            return EtlSteps.Take(position).Aggregate(Enumerable.Empty<Type>(), AddCurrentStepToAccumilate);
        }

        private IEnumerable<Type> AddCurrentStepToAccumilate(IEnumerable<Type> accumliate, DiscriminatedUnionWithBase<IDataSource, ITransformer, IEtlStep> currentItem)
        {
            return currentItem.Match(
                datasource => accumliate.ConcatSingle(datasource.GetType().GetDataSourceOuputType()),
                transformer => accumliate
                    .ExceptSingle(transformer.GetType().GetDataSourceOuputType())
                    .ConcatSingle(transformer.GetType().GetDataSourceOuputType()));
        }
    }
}
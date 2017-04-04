namespace FileEtl.Console
{
    /// <summary>
    /// Transforms the given Input to the given output type
    /// </summary>
    /// <typeparam name="TInputToTranform"></typeparam>
    /// <typeparam name="TTransformedOuput"></typeparam>
    public interface ITransformer<TInputToTranform, TTransformedOuput> : ITransformer
    {
        TTransformedOuput Transform(TInputToTranform input);
    }

    public interface ITransformer<TInput, TInputToTranform, TTransformedOuput> : ITransformer
    {
        TTransformedOuput Transform(TInput input, TInputToTranform inputTotransform);
    }

    public interface ITransformer : IEtlStep
    {
    }
}
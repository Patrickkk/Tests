namespace FileEtl.Console
{
    public interface ITransformer<TInput, TOuput> : ITransformer
    {
        TOuput Transform(TInput input);
    }

    public interface ITransformer : IEtlStep
    {
    }
}
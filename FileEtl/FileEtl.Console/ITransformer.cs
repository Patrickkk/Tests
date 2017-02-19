namespace FileEtl.Console
{
    public interface ITransformer<TInput, TOuput> : ITransformer
    {
    }

    public interface ITransformer : IEtlStep
    {
    }
}
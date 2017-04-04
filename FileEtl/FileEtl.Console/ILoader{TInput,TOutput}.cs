namespace FileEtl.Console
{
    public interface ILoader<TInput, TOutput> : ILoader
    {
        TOutput Run();
    }

    public interface ILoader : IEtlStep
    {
    }
}
namespace FileEtl.Console
{
    public interface IDataSource<TInput, TOutput> : IDataSource
    {
        TOutput Run();
    }
}
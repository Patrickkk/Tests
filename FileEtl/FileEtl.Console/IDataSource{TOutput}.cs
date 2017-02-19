namespace FileEtl.Console
{
    public interface IDataSource<TOutput> : IDataSource
    {
        TOutput Run();
    }
}
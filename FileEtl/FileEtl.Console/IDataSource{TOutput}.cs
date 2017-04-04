namespace FileEtl.Console
{
    /// <summary>
    /// A Source of data with no input data required.
    /// </summary>
    /// <typeparam name="TOutput"></typeparam>
    public interface IDataSource<TOutput> : IDataSource
    {
        TOutput Run();
    }
}
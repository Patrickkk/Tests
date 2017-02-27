namespace FileEtl.Console.DataSources
{
    public interface IConfigurable<TConfigurationClass>
    {
        TConfigurationClass Configuration { get; set; }
    }
}
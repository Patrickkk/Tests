namespace FileEtl.Core
{
    public interface IConfigurableEtlStep<TConfigurationClass>
    {
        TConfigurationClass Configuration { get; set; }
    }
}
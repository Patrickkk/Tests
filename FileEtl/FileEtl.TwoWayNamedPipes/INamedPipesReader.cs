using System.IO.Pipes;

namespace FileEtl.TwoWayNamedPipes
{
    public interface INamedPipesReader<T>
    {
        T Read(NamedPipeServerStream stream);
    }
}
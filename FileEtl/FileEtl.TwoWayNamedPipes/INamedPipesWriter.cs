using System.IO.Pipes;

namespace FileEtl.TwoWayNamedPipes
{
    public interface INamedPipesWriter<T>
    {
        void Write(T value, NamedPipeClientStream stream);
    }
}
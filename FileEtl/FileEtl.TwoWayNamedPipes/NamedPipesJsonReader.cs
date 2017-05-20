using System.IO;
using System.IO.Pipes;
using Newtonsoft.Json;

namespace FileEtl.TwoWayNamedPipes
{
    public class NamedPipesJsonReader<T> : INamedPipesReader<T>
    {
        private StreamReader reader;
        private JsonSerializer serializer;

        public T Read(NamedPipeServerStream stream)
        {
            if (NotInitialized())
            {
                reader = new StreamReader(stream);
                serializer = new JsonSerializer();
            }
            return (T)serializer.Deserialize(reader, typeof(T));
        }

        private bool NotInitialized()
        {
            return reader == null;
        }
    }
}
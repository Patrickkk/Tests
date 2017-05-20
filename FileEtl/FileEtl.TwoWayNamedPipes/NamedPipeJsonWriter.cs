using System.IO;
using System.IO.Pipes;
using Newtonsoft.Json;

namespace FileEtl.TwoWayNamedPipes
{
    public class NamedPipeJsonWriter<T> : INamedPipesWriter<T>
    {
        private StreamWriter writer;
        private JsonSerializer serializer;

        public void Write(T value, NamedPipeClientStream stream)
        {
            if (NotInitialized())
            {
                writer = new StreamWriter(stream);
                serializer = new JsonSerializer();
            }

            serializer.Serialize(writer, value);
            writer.Flush();
        }

        private bool NotInitialized()
        {
            return writer == null;
        }
    }
}
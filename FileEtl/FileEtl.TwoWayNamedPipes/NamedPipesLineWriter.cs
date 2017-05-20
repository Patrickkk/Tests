using System.IO;
using System.IO.Pipes;
using System.Text;

namespace FileEtl.TwoWayNamedPipes
{
    public class NamedPipesLineWriter : INamedPipesWriter<string>
    {
        public void Write(string value, NamedPipeClientStream stream)
        {
            using (var writer = new StreamWriter(stream, Encoding.UTF8, 512, true))
            {
                writer.WriteLine(value.Replace("\n", "").Replace("\r", ""));
                writer.Flush();
            }
        }
    }
}
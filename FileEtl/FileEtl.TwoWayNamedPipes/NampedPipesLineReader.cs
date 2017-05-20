using System.IO;
using System.IO.Pipes;
using System.Text;

namespace FileEtl.TwoWayNamedPipes
{
    public class NampedPipesLineReader : INamedPipesReader<string>
    {
        public string Read(NamedPipeServerStream stream)
        {
            using (var reader = new StreamReader(stream, Encoding.UTF8, true, 512, true))
            {
                return reader.ReadLine();
            }
        }
    }
}
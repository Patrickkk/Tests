using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace TypescriptGeneration.Test
{
    public static class CloneExtension
    {
        public static T Clone<T>(this T objectToCopy)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                BinaryFormatter binaryFormatter = new BinaryFormatter();
                binaryFormatter.Serialize(memoryStream, objectToCopy);
                memoryStream.Seek(0, SeekOrigin.Begin);
                return (T)binaryFormatter.Deserialize(memoryStream);
            }
        }
    }
}

using FileEtl.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileEtl.Files
{
    public class DecompressionEtlStep : IEtlStep, IConfigurableEtlStep<DecompressionEtlStepConfig>
    {
        public DecompressionEtlStepConfig Configuration { get; set; }

        [EtlStepRunMethod]
        public DecompressionResult LoadFileInfo()
        {
            return new DecompressionResult { };//TODO
        }
    }

    public class DecompressionResult
    {
        public IEnumerable<FileInfo> Files { get; set; } = new List<FileInfo>();
    }
}
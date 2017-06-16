using System;
using System.Collections.Generic;
using System.Linq;

namespace FileEtl.Core
{
    public class EtlStepSignature
    {
        public Type Type { get; set; }

        public string TypeName { get { return Type.FullName; } }

        public string ReturnType { get { return Type.GetEtlStepRunMethod().ReturnType.FullName; } }

        public IEnumerable<string> Parameters { get { return Type.GetEtlStepRunMethod().GetParameters().Select(x => x.ParameterType.Name + " " + x.Name); } }

        public string Signature { get { return string.Join(",", Parameters); } }
    }
}
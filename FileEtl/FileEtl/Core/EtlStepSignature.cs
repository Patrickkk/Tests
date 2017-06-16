using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FileEtl.Core
{
    public class EtlStepSignature
    {
        public Type Type { get; set; }

        public string TypeName { get { return Type.FullName; } }

        public MethodInfo RunMethod { get { return Type.GetEtlStepRunMethod(); } }

        public string ReturnType { get { return Type.GetEtlStepRunMethod().ReturnType.FullName; } }

        public IEnumerable<Type> ParameterTypes { get { return Type.GetEtlStepRunMethod().GetParameters().Select(x => x.ParameterType); } }

        public IEnumerable<string> Parameters { get { return Type.GetEtlStepRunMethod().GetParameters().Select(x => x.ParameterType.Name + " " + x.Name); } }

        public string ParameterSignature { get { return string.Join(",", Parameters); } }

        public override string ToString()
        {
            return $"{Type.FullName}.{RunMethod.Name}({ParameterSignature})";
        }
    }
}
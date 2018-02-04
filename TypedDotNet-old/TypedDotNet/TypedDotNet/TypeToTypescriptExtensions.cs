using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public static class TypeToTypescriptExtensions
    {
        public static TypescriptType ToTypescriptGenericParameter(this Type type)
        {
            // TODO add argument validation
            return new TypescriptGenericTypeParameter { Name = type.Name }.ToTypescriptType();
        }

        public static readonly Dictionary<Type, TypescriptPrimitiveType> primitiveTypes = new Dictionary<Type, TypescriptPrimitiveType>
        {
            {typeof(string), TypescriptPrimitiveType.@string },
            {typeof(bool), TypescriptPrimitiveType.boolean },
            {typeof(int), TypescriptPrimitiveType.number},
            {typeof(uint), TypescriptPrimitiveType.number},
            {typeof(float), TypescriptPrimitiveType.number},
            {typeof(decimal), TypescriptPrimitiveType.number},
            {typeof(double), TypescriptPrimitiveType.number},
            {typeof(long), TypescriptPrimitiveType.number},
            {typeof(ulong), TypescriptPrimitiveType.number},
            {typeof(byte), TypescriptPrimitiveType.number},
            {typeof(short), TypescriptPrimitiveType.number},
            {typeof(ushort), TypescriptPrimitiveType.number},
            {typeof(void), TypescriptPrimitiveType.@void},
            {typeof(Object), TypescriptPrimitiveType.any}
        };

        public static bool IsTypescriptPrimitiveType(this Type type)
        {
            // TODO add argument validation
            return primitiveTypes.ContainsKey(type);
        }

        public static TypescriptType ToTypescriptPrimitiveType(this Type type)
        {
            // TODO add argument validation
            return primitiveTypes[type].ToTypescriptType();
        }

        public static string NameWithoutGeneric(this Type type)
        {
            // TODO add argument validation
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`'));
            }
            else
            {
                return type.Name;
            }
        }

        public static TypescriptGenericTypeArguments GetGenericTypeParametersAsArguments(this Type type)
        {
            // TODO add argument validation
            var parameters = GetGenericTypeParametersFor(type);
            var arguments = new TypescriptGenericTypeArguments();
            parameters.ForEach(x => arguments.Add(x));
            return arguments;
        }

        public static TypescriptGenericTypeParameters GetGenericTypeParametersFor(this Type type)
        {
            // TODO add argument validation
            var result = new TypescriptGenericTypeParameters();
            foreach (var genericTypeArgument in type.GetGenericArguments())
            {
                result.Add(new TypescriptGenericTypeParameter
                {
                    Name = genericTypeArgument.Name
                });
            }
            return result;
        }
    }
}

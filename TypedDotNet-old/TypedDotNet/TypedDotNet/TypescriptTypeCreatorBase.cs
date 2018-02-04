using System;
using TypescriptGeneration.Model;

namespace TypedDotNet
{
    public class TypescriptTypeCreatorBase
    {
        public static string NameWithoutGeneric(Type type)
        {
            if (type.IsGenericType)
            {
                return type.Name.Substring(0, type.Name.IndexOf('`'));
            }
            else
            {
                return type.Name;
            }
        }

        public static TypescriptGenericTypeArguments GetGenericTypeParametersAsArguments(Type type)
        {
            var parameters = GetGenericTypeParametersFor(type);
            var arguments = new TypescriptGenericTypeArguments();
            parameters.ForEach(x => arguments.Add(x));
            return arguments;
        }

        public static TypescriptGenericTypeParameters GetGenericTypeParametersFor(Type type)
        {
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

        public static TypescriptGenericTypeArguments GetGenericTypeArgumentsFor(ITypescriptTypeCreator typeCreator, Type baseType, TypescriptModel model)
        {
            var result = new TypescriptGenericTypeArguments();

            foreach (var genericTypeArgument in baseType.GetGenericArguments())
            {
                var tsType = typeCreator.GetTypeFor(genericTypeArgument, model);

                tsType.Match(
                    primitive => result.Add(primitive),
                    tsClass => result.Add(tsClass),
                    tsInterface => result.Add(tsInterface),
                    tsEnumerable => { throw new Exception("enum cannot be a generic type parameter"); },
                    genericTypeParameter => result.Add(genericTypeParameter));
            }
            return result;
        }
    }
}

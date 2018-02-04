using System.Collections.Generic;
using System.Linq;
using FunctionalSharp.DiscriminatedUnions;
using FunctionalSharp.OptionTypes;
using TypescriptGeneration.Model;
using System;

namespace TypescriptGeneration
{
    /// <summary>
    /// A writer to create typescript code form a tpyescript model.
    /// </summary>
    public class TypescriptWriter
    {
        private TypescriptSyntaxWriter syntaxWriter = new TypescriptSyntaxWriter();

        /// <summary>
        /// TODO.
        /// </summary>
        /// <param name="typescriptFileContent"></param>
        public void WriteTypescriptFileContent(TypescriptFileContent typescriptFileContent)
        {
            typescriptFileContent.Match(
                WriteModule,
                WriteClass,
                WriteInterface,
                WriteFunctionStandAlone,
                WriteEnum,
                WriteCode);
        }

        /// <summary>
        /// Writes a typescript module and its contents.
        /// </summary>
        /// <param name="tsModule"></param>
        public void WriteModule(TypescriptModule tsModule)
        {
            syntaxWriter.Write(TypescriptSyntaxKeywords.module + " " + tsModule.Name);
            syntaxWriter.WriteSpace();
            WriteOpeningBracket();

            tsModule.Content.AllButLast().Match(
                module => { WriteModule(module); syntaxWriter.WriteLine(); },
                tsClass => { WriteClass(tsClass); syntaxWriter.WriteLine(); },
                tsInterface => { WriteInterface(tsInterface); syntaxWriter.WriteLine(); },
                tsFunction => { WriteFunctionStandAlone(tsFunction); syntaxWriter.WriteLine(); },
                tsEnum => { WriteEnum(tsEnum); syntaxWriter.WriteLine(); },
                tsCode => { WriteCode(tsCode); syntaxWriter.WriteLine(); });

            if (tsModule.Content.Any())
            {
                tsModule.Content.Last().Match(
                WriteModule,
                WriteClass,
                WriteInterface,
                WriteFunctionStandAlone,
                WriteEnum,
                WriteCode);
            }
            WriteClosingBracket();
        }

        /// <summary>
        /// Writes a typescript class and its contents.
        /// </summary>
        /// <param name="tsClass">the Class to write.</param>
        public void WriteClass(TypescriptClass tsClass)
        {
            WriteClassStart(tsClass);

            tsClass.Content.Match(
                    WriteTypescriptFunctionWithAccesabilty,
                    WriteProperty,
                    WriteCode);

            WriteClosingBracket();
        }

        /// <summary>
        /// Writes typescript code.
        /// </summary>
        /// <param name="tsCode">The code to write.</param>
        public void WriteCode(TypescriptCode tsCode)
        {
            foreach (var line in tsCode)
            {
                syntaxWriter.WriteLine(line);
            }
        }

        /// <summary>
        /// Writes a typescript enum.
        /// </summary>
        /// <param name="tsEnum"></param>
        public void WriteEnum(TypescriptEnumerable tsEnum)
        {
            syntaxWriter.Write(TypescriptSyntaxKeywords.@enum.ToString() + " " + tsEnum.Name + " ");
            syntaxWriter.WriteOpeningBracket();
            tsEnum.Options.AllButLast().ForEach(value => syntaxWriter.WriteLine(value + ","));
            if (tsEnum.Options.Any())
            {
                syntaxWriter.WriteLine(tsEnum.Options.Last());
            }
            WriteClosingBracket();
        }

        /// <summary>
        /// writes a typescript interface.
        /// </summary>
        /// <param name="tsInterface"></param>
        public void WriteInterface(TypescriptInterface tsInterface)
        {
            syntaxWriter.Write(TypescriptSyntaxKeywords.@interface + " " + tsInterface.Name);
            WriteGenericParameters(tsInterface.GenricTypeParameters);
            WriteInterfaceBaseTypes(tsInterface.BaseType);
            syntaxWriter.WriteSpace();
            WriteOpeningBracket();

            tsInterface.Content.Match(
                tsFunction => { WriteTypescriptFunctionSignature(tsFunction, onlySignature: true); syntaxWriter.WriteLine(); },
                tsProperty => { WriteProperty(tsProperty, WriteAccesibilityModifier: false); });

            WriteClosingBracket();
        }

        private void WriteInterfaceBaseTypes(TypescriptInterfaceBaseTypes baseTypes)
        {
            if (baseTypes.Any())
            {
                syntaxWriter.Write(" ");
                syntaxWriter.Write(TypescriptSyntaxKeywords.extends.ToString());
                syntaxWriter.Write(" ");
                var baseTypesString = string.Join(", ", baseTypes.Select(baseType => TypeScriptBaseTypeAsString(baseType)));
                syntaxWriter.Write(baseTypesString);
            }
        }

        private string TypeScriptBaseTypeAsString(TypescriptInterfaceBaseType baseType)
        {
            return string.Join(", ",
                baseType.Match(
                baseclass => baseclass.Name + GetGenericTypeArgumentsString(baseclass.GenericArguments),
                baseInterface => baseInterface.Name + GetGenericTypeArgumentsString(baseInterface.GenericArguments)));
        }

        /// <summary>
        /// writes a typescript property with the accesability modifier.
        /// </summary>
        /// <param name="tsProperty">the property to write.</param>
        public void WriteProperty(TypescriptProperty tsProperty)
        {
            WriteProperty(tsProperty, true);
        }

        /// <summary>
        /// writes a typescript property.
        /// </summary>
        /// <param name="tsProperty">the property to write.</param>
        /// <param name="WriteAccesibilityModifier">whether to write the accesability modifier.</param>
        public void WriteProperty(TypescriptProperty tsProperty, bool WriteAccesibilityModifier = true)
        {
            if (WriteAccesibilityModifier)
            {
                syntaxWriter.Write(tsProperty.Accesability.ToString());
                syntaxWriter.WriteSpace();
            }
            syntaxWriter.Write(tsProperty.Name);
            WriteTypeAnnotation(tsProperty.Type);
            WriteDefaultValue(tsProperty.DefaultValue);
            syntaxWriter.Write(";");
            syntaxWriter.WriteLine();
        }

        /// <summary>
        /// Writes a typescript function.
        /// </summary>
        /// <param name="tsFunction"></param>
        /// <param name="standaloneFunction"></param>
        public void WriteFunction(TypescriptFunction tsFunction, bool standaloneFunction)
        {
            WriteTypescriptFunctionSignature((TypescriptFunctionSignature)tsFunction, standaloneFunction);
            syntaxWriter.WriteOpeningBracket();
            WriteCode(tsFunction.MethodBody);
            syntaxWriter.WriteClosingBracket();
        }

        /// <summary>
        /// Writes a typescript function.
        /// </summary>
        /// <param name="tsFunction"></param>
        public void WriteFunction(TypescriptFunction tsFunction)
        {
            WriteFunction(tsFunction, false);//this allows us to use this method in match signatures that require 1 parameter
        }

        /// <summary>
        /// Writes a typescript function as a standalone function
        /// </summary>
        /// <param name="tsFunction"></param>
        public void WriteFunctionStandAlone(TypescriptFunction tsFunction)
        {
            WriteFunction(tsFunction, true);//this allows us to use this method in match signatures that require 1 parameter
        }

        /// <summary>
        /// Writes a typescript function signature.
        /// </summary>
        /// <param name="tsFunctionSignature">the function signature to write.</param>
        /// <param name="standaloneFunction">whether is is a standalone function. if true the function keyword is written in fron of the function signature.</param>
        /// <param name="onlySignature">whether only the signature is written, is so it will end with a ';'</param>
        public void WriteTypescriptFunctionSignature(TypescriptFunctionSignature tsFunctionSignature, bool standaloneFunction = false, bool onlySignature = false)
        {
            if (standaloneFunction)
            {
                syntaxWriter.Write(TypescriptSyntaxKeywords.function.ToString());
                syntaxWriter.WriteSpace();
            }
            syntaxWriter.Write(tsFunctionSignature.Name);
            WriteGenericParameters(tsFunctionSignature.GenricTypeParameters);
            WriteParameters(tsFunctionSignature.Parameters);
            WriteTypeAnnotation(tsFunctionSignature.ReturnType);
            if (onlySignature)
            {
                syntaxWriter.Write(";");
            }
        }

        private void Write(TypescriptParameter parameter)
        {
            syntaxWriter.Write(parameter.Name);
            WriteTypeAnnotation(parameter.TypescriptType);
        }

        private void WriteGenericParameters(TypescriptGenericTypeParameters genricTypeParameters)
        {
            if (genricTypeParameters.Any())
            {
                syntaxWriter.Write("<");
                for (int i = 0; i < genricTypeParameters.Count; i++)
                {
                    syntaxWriter.Write(genricTypeParameters[i].Name);
                    if (IsNotLastIndexInCollection(genricTypeParameters, i))
                    {
                        syntaxWriter.Write(", ");
                    }
                }
                syntaxWriter.Write(">");
            }
        }

        private void WriteBaseInterface(TypescriptBaseInterface baseInterface, bool inheritsFromInterface)
        {
            if (baseInterface != null)
            {
                syntaxWriter.WriteSpace();
                if (inheritsFromInterface == true)
                {
                    syntaxWriter.Write(TypescriptSyntaxKeywords.extends.ToString());
                }
                else
                {
                    syntaxWriter.Write(TypescriptSyntaxKeywords.implements.ToString());
                }
                syntaxWriter.WriteSpace();
                syntaxWriter.Write(baseInterface.Name);
                WriteTypescriptGenericArguments(baseInterface.GenericArguments);
            }
        }

        private void WriteClassStart(TypescriptClass tsClass)
        {
            syntaxWriter.Write(TypescriptSyntaxKeywords.@class + " " + tsClass.Name);
            WriteGenericParameters(tsClass.GenricTypeParameters);
            WriteBaseClass(tsClass.BaseClass);
            WriteInterfaceImplementations(tsClass.InterfaceImplementations);
            syntaxWriter.WriteSpace();
            WriteOpeningBracket();
        }

        private void WriteInterfaceImplementations(IList<TypescriptInterface> interfaceImplementations)
        {
            if (interfaceImplementations.Any())
            {
                syntaxWriter.Write(" " + TypescriptSyntaxKeywords.implements.ToString() + " ");
                for (int i = 0; i < interfaceImplementations.Count; i++)
                {
                    syntaxWriter.Write(interfaceImplementations[i].Name);
                    if (IsNotLastIndexInCollection(interfaceImplementations, i))
                    {
                        syntaxWriter.Write(", ");
                    }
                }
            }
        }

        private void WriteClosingBracket()
        {
            syntaxWriter.WriteClosingBracket();
        }

        private void WriteOpeningBracket()
        {
            syntaxWriter.WriteOpeningBracket();
        }

        private void WriteBaseClass(IOption<TypescriptBaseClass> baseClass)
        {
            baseClass.IfNotNullDo(baseClassValue =>
                {
                    syntaxWriter.WriteSpace();
                    syntaxWriter.Write(TypescriptSyntaxKeywords.extends.ToString());
                    syntaxWriter.WriteSpace();
                    syntaxWriter.Write(baseClassValue.Name);
                    WriteTypescriptGenericArguments(baseClassValue.GenericArguments);
                });
        }

        [Obsolete("Todo check if this can be removed")]
        private void WriteTypescriptGenericArguments(IList<TypescriptGenericTypeArgument> genericArguments)
        {
            syntaxWriter.Write(GetGenericTypeArgumentsString(genericArguments));
        }

        private string GetGenericTypeArgumentsString(IList<TypescriptGenericTypeArgument> genericArguments)
        {
            if (genericArguments.Any())
            {
                return "<" + string.Join(", ", genericArguments.Select(genericArgument => GetGenericTypeArgumentString(genericArgument))) + ">";
            }
            else
            {
                return "";
            }
        }

        private string GetGenericTypeArgumentString(TypescriptGenericTypeArgument genericArgument)
        {
            return genericArgument.Match(
                        primitiveType => primitiveType.ToString(),
                        tsClass => tsClass.Name,
                        tsInterface => tsInterface.Name,
                        tsParameter => tsParameter.Name);
        }

        private static bool IsNotLastIndexInCollection<T>(IList<T> genericParameters, int i)
        {
            return i < genericParameters.Count - 1;
        }

        private void WriteTypescriptFunctionWithAccesabilty(TypescriptFunction tsFunction)
        {
            syntaxWriter.Write(tsFunction.Accesability.ToString());
            syntaxWriter.WriteSpace();
            WriteFunction(tsFunction);
        }

        private void WriteParameters(IList<TypescriptParameter> parameters)
        {
            syntaxWriter.Write("(");
            foreach (var parameter in parameters)
            {
                Write(parameter);
            }
            syntaxWriter.Write(")");
        }

        private void WriteDefaultValue(IOption<string> defaultValue)
        {
            defaultValue.IfNotNullDo(value =>
            {
                syntaxWriter.Write(" = ");
                syntaxWriter.Write(value);
            });
        }

        private void WriteTypeAnnotation(TypescriptType type)
        {
            type.Match(
                primitive => syntaxWriter.WriteTypeAnnotation(primitive.ToString()),
                WriteTypeAnnotation,
                WriteTypeAnnotation,
                WriteTypeAnnotation,
                WriteGenericParameter);
        }

        private void WriteGenericParameter(TypescriptGenericTypeParameter genericParameter)
        {
            syntaxWriter.WriteTypeAnnotation(genericParameter.Name);
        }

        private void WriteTypeAnnotation(TypescriptComplexType tsComplexType)
        {
            syntaxWriter.WriteTypeAnnotation(tsComplexType.Name);
            WriteGenericParameters(tsComplexType.GenricTypeParameters);
        }

        private void WriteTypeAnnotation(TypescriptEnumerable tsEnum)
        {
            syntaxWriter.WriteTypeAnnotation(tsEnum.Name);
        }

        /// <summary>
        /// returns the written tpyescript code as a string.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return syntaxWriter.ToString();
        }
    }
}
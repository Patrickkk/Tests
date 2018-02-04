namespace TypescriptGeneration.Model
{
    using FunctionalSharp.DiscriminatedUnions;
    using System;

    /// <summary>
    /// Represents typescript generic type argument. this can be a TypescriptPrimitiveType, TypescriptClass, TypescriptInterface or TypescriptGenericTypeParameter.
    /// TODO: possible duplicate of typescripttype.
    /// </summary>
    [Serializable]
    public class TypescriptGenericTypeArgument : DiscriminatedUnion<TypescriptPrimitiveType, TypescriptClass, TypescriptInterface, TypescriptGenericTypeParameter>
    {
        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument representing a primitive type.
        /// </summary>
        /// <param name="primitive"></param>
        public TypescriptGenericTypeArgument(TypescriptPrimitiveType primitive) : base(primitive) { }
        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument representing a class.
        /// </summary>
        /// <param name="class"></param>
        public TypescriptGenericTypeArgument(TypescriptClass @class) : base(@class) { }
        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument representing a interface.
        /// </summary>
        /// <param name="interface"></param>
        public TypescriptGenericTypeArgument(TypescriptInterface @interface) : base(@interface) { }
        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument representing a generic type parameter.
        /// </summary>
        /// <param name="genericTypeParameter"></param>
        public TypescriptGenericTypeArgument(TypescriptGenericTypeParameter genericTypeParameter) : base(genericTypeParameter) { }
    }
}

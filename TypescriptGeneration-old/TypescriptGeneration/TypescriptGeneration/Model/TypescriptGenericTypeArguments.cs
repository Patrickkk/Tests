namespace TypescriptGeneration.Model
{
    using System.Collections.Generic;
    using System;

    /// <summary>
    /// Represents a list of generic type arguments. 
    /// </summary>
    [Serializable]
    public class TypescriptGenericTypeArguments : List<TypescriptGenericTypeArgument>
    {
        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument using the given primitive.
        /// </summary>
        /// <param name="primitive">The primitive to add.</param>
        public void Add(TypescriptPrimitiveType primitive)
        {
            Add(new TypescriptGenericTypeArgument(primitive));
        }

        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument using the given class.
        /// </summary>
        /// <param name="class">The class to add.</param>
        public void Add(TypescriptClass @class)
        {
            Add(new TypescriptGenericTypeArgument(@class));
        }

        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument using the given interface.
        /// </summary>
        /// <param name="interface">The interface to add.</param>
        public void Add(TypescriptInterface @interface)
        {
            Add(new TypescriptGenericTypeArgument(@interface));
        }

        /// <summary>
        /// Creates a new TypescriptGenericTypeArgument using the given parameter.
        /// </summary>
        /// <param name="parameter">The parameter to add.</param>
        public void Add(TypescriptGenericTypeParameter parameter)
        {
            Add(new TypescriptGenericTypeArgument(parameter));
        }
    }
}

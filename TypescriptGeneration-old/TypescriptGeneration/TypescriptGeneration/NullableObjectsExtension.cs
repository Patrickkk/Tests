using System;

namespace TypescriptGeneration
{
    static class NullableObjectsExtension
    {
        public static TReturn NMatch<T, TReturn>(this T refType, Func<T, TReturn> NotNullFunction, Func<TReturn> NullFunction)
            where T : class
        {
            if (refType == null)
            {
                return NullFunction();
            }
            else
            {
                return NotNullFunction(refType);
            }
        }

        public static void NMatch<T>(this T refType, Action<T> NotNullAction, Action NullAction)
            where T : class
        {
            if (refType == null)
            {
                NullAction();
            }
            else
            {
                NotNullAction(refType);
            }
        }

        public static void NotNull<T, TReturn>(this T refType, Action<T> NotNullAction, Action NullAction)
    where T : class
        {
            if (refType == null)
            {
                NullAction();
            }
            else
            {
                NotNullAction(refType);
            }
        }

        public static void NotNull<T>(this T refType, Action<T> NotNullAction)
where T : class
        {
            if (refType != null)
            {
                NotNullAction(refType);
            }
        }
    }
}

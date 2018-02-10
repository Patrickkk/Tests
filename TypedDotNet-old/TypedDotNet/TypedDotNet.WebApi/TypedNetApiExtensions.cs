namespace TypedDotNet.WebApi
{
    public static class TypedNetApiExtensions
    {
        public static TypedNet Setup()
        {
            return TypedNet.Setup(new TypedWebApiSetup());
        }
    }
}

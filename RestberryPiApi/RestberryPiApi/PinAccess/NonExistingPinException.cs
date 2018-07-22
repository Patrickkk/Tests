using System;
using System.Runtime.Serialization;

namespace RestberryPiApi.PinAccess
{
    [Serializable]
    public class NonExistingPinException : Exception
    {
        public NonExistingPinException()
        {
        }

        public NonExistingPinException(string message) : base(message)
        {
        }

        public NonExistingPinException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NonExistingPinException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
using System;
using System.Runtime.Serialization;

namespace NResult.Tests
{
    public class SomeUsefulException : Exception
    {
        public SomeUsefulException():base()
        {
        }

        public SomeUsefulException(string message) : base(message)
        {
        }

        protected SomeUsefulException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public SomeUsefulException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

using System;
using System.Runtime.Serialization;

namespace Prototype.Fez.BootloaderUtil
{
    internal class FezBootloaderException : Exception
    {
        public FezBootloaderException()
        {
        }

        public FezBootloaderException(string message) : base(message)
        {
        }

        public FezBootloaderException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FezBootloaderException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
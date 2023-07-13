using System.Runtime.Serialization;

namespace SKLaboratory.Infrastructure.Exceptions
{
    [Serializable]
    internal class WidgetNotFoundException : Exception
    {
        public WidgetNotFoundException()
        {
        }

        public WidgetNotFoundException(string? message) : base(message)
        {
        }

        public WidgetNotFoundException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected WidgetNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
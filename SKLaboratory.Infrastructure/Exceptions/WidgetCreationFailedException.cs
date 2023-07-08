using System.Runtime.Serialization;

[Serializable]
internal class WidgetCreationFailedException : Exception
{
    public WidgetCreationFailedException()
    {
    }

    public WidgetCreationFailedException(string? message) : base(message)
    {
    }

    public WidgetCreationFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }

    protected WidgetCreationFailedException(SerializationInfo info, StreamingContext context) : base(info, context)
    {
    }
}
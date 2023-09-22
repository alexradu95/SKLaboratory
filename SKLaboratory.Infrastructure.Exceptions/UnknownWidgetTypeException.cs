namespace SKLaboratory.Infrastructure.Exceptions;

[Serializable]
public class UnknownWidgetTypeException : Exception
{
    public UnknownWidgetTypeException(string message) : base(message)
    {
    }
}
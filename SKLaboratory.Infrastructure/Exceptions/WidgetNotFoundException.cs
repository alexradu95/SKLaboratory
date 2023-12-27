namespace SKLaboratory.Infrastructure.Exceptions;

[Serializable]
public class WidgetNotFoundException : Exception
{
    public WidgetNotFoundException()
    {
    }

    public WidgetNotFoundException(string? message) : base(message)
    {
    }
}
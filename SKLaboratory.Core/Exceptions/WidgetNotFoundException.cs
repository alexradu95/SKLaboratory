namespace SKLaboratory.Core.Exceptions;

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
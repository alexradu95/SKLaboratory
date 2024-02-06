namespace SKLaboratory.Core.BaseClasses;

public abstract class BaseWidget : IWidget
{
	public Guid Id { get; } = Guid.NewGuid();
	public abstract void OnFrameUpdate();
}
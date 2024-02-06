using SKLaboratory.Core.Interfaces;

namespace SKLaboratory.Core.BaseClasses;

public abstract class BaseWidget : IWidget
{
	public Guid Id { get; } = Guid.NewGuid(); // ID is set when the widget is constructed
	public bool IsVisible { get; set; }
	public abstract void OnFrameUpdate();
}
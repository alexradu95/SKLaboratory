using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure;
public abstract class BaseWidget : IWidget
{
    public BaseWidget()
    {
        Id = Guid.NewGuid(); // ID is set when the widget is constructed
    }
    public Guid Id { get; }
    public bool IsVisible { get; set; }
    public abstract void OnFrameUpdate();
}

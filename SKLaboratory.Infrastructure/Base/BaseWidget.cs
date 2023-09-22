using SKLaboratory.Infrastructure.Interfaces;
namespace SKLaboratory.Infrastructure.Base;

public abstract class BaseWidget : IWidget
{
    protected BaseWidget() => (Id, IsActive) = (Guid.NewGuid(), true);

    public Guid Id { get; }
    public bool IsActive { get; set; }

    public abstract void OnFrameUpdate();
}
using SKLaboratory.Infrastructure.Interfaces;
namespace SKLaboratory.Infrastructure.Widgets;

public abstract class BaseWidget : IWidget
{
    public Guid Id { get; } = Guid.NewGuid();
    public bool IsUnique { get; set; }

    public abstract void Init();
    public abstract void Shutdown();
    public abstract void Draw();
}

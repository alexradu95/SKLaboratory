using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;

namespace SKLaboratory.Infrastructure.Base;

public abstract class BaseWidget : IWidget
{
    public BaseWidget()
    {
        Id = Guid.NewGuid();
        IsActive = true;
    }

    public bool IsActive { get; protected set; }
    public Guid Id { get; }

    public abstract Matrix Transform { get; }

    public abstract Pose Pose { get; }


    public abstract void Shutdown();
    public abstract void Draw();
}
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;

namespace SKLaboratory.Infrastructure.Base;

public abstract class BaseWidget : IWidget
{
    protected BaseWidget()
    {
        Id = Guid.NewGuid();
        IsActive = true;
    }


    public Guid Id { get; }
    public bool IsActive { get; set; }

    public abstract void OnFrameUpdate();
}
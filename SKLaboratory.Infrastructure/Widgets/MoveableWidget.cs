using StereoKit;

namespace SKLaboratory.Infrastructure.Widgets;

public abstract class MoveableWidget : BaseWidget
{
    public Pose Pose;

    public override void Init()
    {
        Pose = new Pose(0, 0, -0.5f);
    }

}
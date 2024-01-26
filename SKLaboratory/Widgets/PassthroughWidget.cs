using SKLaboratory.Infrastructure;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;

namespace SKLaboratory;

internal class PassthroughWidget : BaseWidget
{
    private readonly PassthroughStepper passthrough;
    private Pose _pose;

    public PassthroughWidget()
    {
        _pose = new Pose(0, 0, -0.5f);
        passthrough = SK.GetOrCreateStepper<PassthroughStepper>();
    }


public override void OnFrameUpdate()
    {
        if (IsVisible)
        {
            UI.WindowBegin("Passthrough Settings", ref _pose);
            bool toggle = passthrough.Enabled;
            UI.Label(passthrough.Available ? "Passthrough EXT available!" : "No passthrough EXT available :(");
            UI.PushEnabled(passthrough.Available);
            if (UI.Toggle("Passthrough", ref toggle))
                passthrough.Enabled = toggle;
            UI.PopEnabled();
            UI.WindowEnd();
        }
    }
}
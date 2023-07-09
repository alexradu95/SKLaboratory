using SKLaboratory.Infrastructure.Base;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;
using StereoKit.Framework;
using System;

namespace SKLaboratory.Widgets;


class PassthroughWidget : BaseWidget
{

    PassthroughStepper passthrough;

    public override Matrix Transform => _pose.ToMatrix();

    public override Pose Pose => _pose;


    private Pose _pose;

    public override bool Initialize()
    {
        _pose = new Pose(0, 0, -0.5f);
        passthrough = SK.GetOrCreateStepper<PassthroughStepper>();
        return base.Initialize();
    }

    public override void Shutdown()
    {
        IsActive = false;
    }

    public override void Draw()
    {
        if (!IsActive)
        {
            return;
        }
        UI.WindowBegin("Passthrough Settings", ref _pose);
        bool toggle = passthrough.Enabled;
        UI.Label(passthrough.Available
            ? "Passthrough EXT available!"
            : "No passthrough EXT available :(");
        UI.PushEnabled(passthrough.Available);
        if (UI.Toggle("Passthrough", ref toggle))
            passthrough.Enabled = toggle;
        UI.PopEnabled();
        UI.WindowEnd();
    }
}
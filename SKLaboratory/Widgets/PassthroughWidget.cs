using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;
using StereoKit.Framework;
using System;

namespace SKLaboratory.Widgets;


class PassthroughWidget : IWidget
{

    PassthroughStepper passthrough;

    public Matrix Transform => _pose.ToMatrix();

    public Pose Pose => _pose;

    public Guid Id => _id;

    public bool IsActive { get; set; }

    private Guid _id;

    private Pose _pose;

    public bool Initialize()
    {
        _id = Guid.NewGuid();
        // Create assets used by the app
        _pose = new Pose(0, 0, -0.5f);
        passthrough = SK.GetOrCreateStepper<PassthroughStepper>();
        IsActive = true;
        return true;
    }

    public void Shutdown()
    {
        IsActive = false;
    }

    public void Draw()
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
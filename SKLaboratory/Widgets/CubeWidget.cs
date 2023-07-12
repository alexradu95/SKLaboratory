using SKLaboratory.Infrastructure.Base;
using StereoKit;

namespace SKLaboratory;

public class CubeWidget : BaseWidget
{
    private Pose _pose;
    private Model cube;

    public CubeWidget()
    {
        _pose = new Pose(0, 0, -0.5f);
        cube = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), Material.UI);
    }

    public override Matrix Transform => _pose.ToMatrix();

    public override Pose Pose => _pose;

    public override void Shutdown()
    {
        cube = null;
        IsActive = false;
    }

    public override void Draw()
    {
        if (!IsActive) return;
        UI.Handle("Cube", ref _pose, cube.Bounds);
        cube.Draw(Pose.ToMatrix());
    }
}
using SKLaboratory.Infrastructure;
using StereoKit;

namespace SKLaboratory;

public class CubeWidget : BaseWidget
{
    private Pose _pose;
    private Model _cube;

    public CubeWidget()
    {
        _pose = new Pose(0, 0, -0.5f);
        _cube = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), Material.UI);
    }

    public override void OnFrameUpdate()
    {
        if (IsActive)
        {
            UI.Handle("Cube", ref _pose, _cube.Bounds);
            _cube.Draw(_pose.ToMatrix());
        }
    }
}
using SKLaboratory.Infrastructure.Base;
using StereoKit;

namespace SKLaboratory;

public class FloorWidget : BaseWidget
{
    private Matrix _transform;


    private Material floorMaterial;

    public FloorWidget()
    {
        _transform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
        floorMaterial = new Material("floor.hlsl");
        floorMaterial.Transparency = Transparency.Blend;
    }

    public override Matrix Transform => _transform;

    public override Pose Pose => _transform.Pose;

    public override void Shutdown()
    {
        floorMaterial = null;
        IsActive = false;
    }

    public override void Draw()
    {
        if (!IsActive) return;
        if (SK.System.displayType == Display.Opaque)
            Mesh.Cube.Draw(floorMaterial, Transform);
    }
}
using SKLaboratory.Infrastructure;
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


public override void OnFrameUpdate()
{
    if (IsVisible && SK.System.displayType == Display.Opaque)
        Mesh.Cube.Draw(floorMaterial, _transform);
}
}
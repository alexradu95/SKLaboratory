using SKLaboratory.Infrastructure.Widgets;
using StereoKit;
using System;

namespace SKLaboratory.Widgets
{
    public class FloorWidget : StaticWidget
    {

        Material floorMaterial;

        public override void Init()
        {
            floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            floorMaterial = new Material("floor.hlsl");
            floorMaterial.Transparency = Transparency.Blend;
        }

        public override void Shutdown()
        {
            floorMaterial = null;
        }

        public override void Draw()
        {
            if (SK.System.displayType == Display.Opaque)
                Mesh.Cube.Draw(floorMaterial, floorTransform);
        }
    }
}

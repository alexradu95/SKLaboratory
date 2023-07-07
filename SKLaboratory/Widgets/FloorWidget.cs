using SKLaboratory.Infrastructure;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLaboratory.Widgets
{
    internal class FloorWidget : IWidget
    {

        Material floorMaterial;
        Matrix floorTransform;

        public void Init()
        {
            floorTransform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            floorMaterial = new Material("floor.hlsl");
            floorMaterial.Transparency = Transparency.Blend;
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            if (SK.System.displayType == Display.Opaque)
                Mesh.Cube.Draw(floorMaterial, floorTransform);
        }
    }
}

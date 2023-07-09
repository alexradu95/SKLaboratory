using SKLaboratory.Infrastructure.Base;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;

namespace SKLaboratory.Widgets
{
    public class FloorWidget : BaseWidget
    {
        public override Matrix Transform => _transform;

        public override Pose Pose => _transform.Pose;

        private Matrix _transform;




        Material floorMaterial;

        public bool Initialize()
        {

            _transform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            floorMaterial = new Material("floor.hlsl");
            floorMaterial.Transparency = Transparency.Blend;
            return base.Initialize(); ;
        }

        public override void Shutdown()
        {
            floorMaterial = null;
            IsActive = false;
        }

        public override void Draw()
        {
            if (!IsActive)
            {
                return;
            }
            if (SK.System.displayType == Display.Opaque)
                Mesh.Cube.Draw(floorMaterial, Transform);
        }
    }
}

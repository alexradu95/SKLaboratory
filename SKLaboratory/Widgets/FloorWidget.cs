using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;

namespace SKLaboratory.Widgets
{
    public class FloorWidget : IWidget
    {
        public Matrix Transform => _transform;

        private Matrix _transform;

        public Pose Pose => _transform.Pose;

        public Guid Id => _id;

        private Guid _id;


        Material floorMaterial;

        public void Init()
        {
            _id = Guid.NewGuid();
            _transform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
            floorMaterial = new Material("floor.hlsl");
            floorMaterial.Transparency = Transparency.Blend;
        }

        public void Shutdown()
        {
            floorMaterial = null;
        }

        public void Draw()
        {
            if (SK.System.displayType == Display.Opaque)
                Mesh.Cube.Draw(floorMaterial, Transform);
        }
    }
}

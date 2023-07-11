using SKLaboratory.Infrastructure.Base;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;



namespace SKLaboratory.Widgets
{
    public class CubeWidget : BaseWidget
    {
        Model cube;
        private Pose _pose;

        public override Matrix Transform => _pose.ToMatrix();

        public override Pose Pose => _pose;

        public CubeWidget() : base() 
        {
            _pose = new Pose(0, 0, -0.5f);
            cube = Model.FromMesh(Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f), Material.UI);
        }

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
}

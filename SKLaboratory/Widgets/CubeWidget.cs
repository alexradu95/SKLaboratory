using SKLaboratory.Infrastructure.Widgets;
using StereoKit;
using System;

namespace SKLaboratory.Widgets
{
    internal class CubeWidget : MoveableWidget
    {
        Model cube;
        public override void Init()
        {
            // Create assets used by the app
            pose = new Pose(0, 0, -0.5f);
            cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                Material.UI);
        }

        public override void Shutdown()
        {
            cube = null;
        }

        public override void Draw()
        {
            UI.Handle("Cube", ref pose, cube.Bounds);
            cube.Draw(pose.ToMatrix());
        }
    }
}

using SKLaboratory.Infrastructure;
using StereoKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLaboratory.Widgets
{
    internal class CubeWidget : IWidget
    {
        Pose cubePose;
        Model cube;
        public void Init()
        {
            // Create assets used by the app
            cubePose = new Pose(0, 0, -0.5f);
            cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                Material.UI);
        }

        public void Shutdown()
        {
            throw new NotImplementedException();
        }

        public void Update()
        {
            UI.Handle("Cube", ref cubePose, cube.Bounds);
            cube.Draw(cubePose.ToMatrix());
        }
    }
}

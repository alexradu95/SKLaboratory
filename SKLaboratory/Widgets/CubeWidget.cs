﻿using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;

namespace SKLaboratory.Widgets
{
    internal class CubeWidget : IWidget
    {
        Model cube;

        public Matrix Transform => _pose.ToMatrix();

        public Pose Pose => _pose;

        public Guid Id => _id;

        private Guid _id;

        private Pose _pose;

        public bool Initialize()
        {
            _id = Guid.NewGuid();
            // Create assets used by the app
            _pose = new Pose(0, 0, -0.5f);
            cube = Model.FromMesh(
                Mesh.GenerateRoundedCube(Vec3.One * 0.1f, 0.02f),
                Material.UI);
            return true;
        }

        public void Shutdown()
        {
            cube = null;
        }

        public void Draw()
        {
            UI.Handle("Cube", ref _pose, cube.Bounds);
            cube.Draw(Pose.ToMatrix());
        }
    }
}

using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;

namespace SKLaboratory.Infrastructure.Widgets
{
    internal class MockNullWidget : IWidget
    {
        public Guid Id => Guid.Empty;

        public Matrix Transform => Matrix.Identity;

        public Pose Pose => Pose.Identity;

        public void Draw()
        {
            // Nothing
        }

        public bool Initialize()
        {
            return true;
        }

        public void Shutdown()
        {
            // Nothing
        }
    }
}

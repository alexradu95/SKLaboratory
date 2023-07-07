using StereoKit;

namespace SKLaboratory.Infrastructure
{
    public abstract class StaticWidget : IWidget
    {

        public Matrix floorTransform;

        public abstract void Init();

        public abstract void Shutdown();

        public abstract void Update();
    }
}

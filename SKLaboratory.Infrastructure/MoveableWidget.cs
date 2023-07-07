using StereoKit;

namespace SKLaboratory.Infrastructure
{
    public abstract class MoveableWidget : IWidget
    {
        public Pose pose;

        public abstract void Init();

        public abstract void Shutdown();

        public abstract void Update();
    }
}

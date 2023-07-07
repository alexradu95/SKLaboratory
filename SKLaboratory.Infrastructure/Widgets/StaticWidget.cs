using StereoKit;

namespace SKLaboratory.Infrastructure.Widgets
{
    public abstract class StaticWidget : BaseWidget
    {
        public Matrix Transform;

        public override void Init()
        {
            Transform = Matrix.TS(0, -1.5f, 0, new Vec3(30, 0.1f, 30));
        }

    }
}

using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Widgets
{
    public abstract class BaseWidget : IWidget
    {

        public readonly Guid Id = Guid.NewGuid();

        public Boolean IsUnique = false;

        public abstract void Init();

        public abstract void Shutdown();

        public abstract void Draw();
    }
}

namespace SKLaboratory.Infrastructure.Interfaces
{
    public interface IInitializable
    {
        void Init();
    }

    public interface IDrawable
    {
        void Draw();
    }

    public interface IShutdownable
    {
        void Shutdown();
    }

    public interface IWidget : IInitializable, IDrawable, IShutdownable
    {
    }
}

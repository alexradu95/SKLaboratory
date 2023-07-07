using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Interfaces
{
    /// <summary>
    /// Interface for widgets that can be initialized.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Initializes the widget.
        /// </summary>
        void Init();
    }

    /// <summary>
    /// Interface for widgets that can be drawn.
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws the widget.
        /// </summary>
        void Draw();
    }

    /// <summary>
    /// Interface for widgets that can be shut down.
    /// </summary>
    public interface IShutdownable
    {
        /// <summary>
        /// Shuts down the widget.
        /// </summary>
        void Shutdown();
    }

    /// <summary>
    /// Interface for widgets. Widgets are components that can be initialized, drawn, and shut down.
    /// </summary>
    public interface IWidget : IInitializable, IDrawable, IShutdownable
    {
    }

}

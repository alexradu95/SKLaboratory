using StereoKit;

namespace SKLaboratory.Infrastructure.Interfaces
{


    /// <summary>
    /// Interface for widgets that can be initialized.
    /// </summary>
    public interface IInitializable
    {
        /// <summary>
        /// Initializes the Widget
        /// </summary>
        bool Initialize();
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
    /// Interface for widgets have a position in 3D space
    /// </summary>
    public interface IHasPosition
    {
        public Matrix Transform { get; }

        public Pose Pose { get; }
    }

    /// <summary>
    /// Interface for widgets. Widgets are components that can be initialized, drawn, and shut down.
    /// </summary>
    public interface IWidget : IInitializable, IDrawable, IShutdownable, IHasPosition
    {
        /// <summary>
        /// Gets the unique identifier for the widget.
        /// </summary>
        public Guid Id { get; }
    }

}

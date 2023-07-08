using StereoKit;

namespace SKLaboratory.Infrastructure.Interfaces;

/// <summary>
/// Interface for widgets that can be initialized.
/// </summary>
public interface IHasLifecycle
{
    /// <summary>
    /// Initializes the Widget
    /// </summary>
    bool Initialize();

    /// <summary>
    /// Draws the widget.
    /// </summary>
    void Draw();

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
/// Interface for widgets have a position in 3D space
/// </summary>
public interface IHasState
{
    // Could be extended to WidgetState if it is needed in the future
    bool IsActive { get; set; }
}

/// <summary>
/// Interface for widgets. Widgets are components that can be initialized, drawn, and shut down.
/// </summary>
public interface IWidget : IHasLifecycle, IHasPosition
{
    /// <summary>
    /// Gets the unique identifier for the widget.
    /// </summary>
    public Guid Id { get; }
}

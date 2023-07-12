using StereoKit;

namespace SKLaboratory.Infrastructure.Interfaces;


/// <summary>
///     Interface for widgets. Widgets are components that when active, the OnFrameUpdate is run every frame.
/// </summary>
public interface IWidget
{
    /// <summary>
    ///     Gets the unique identifier for the widget.
    /// </summary>
    /// Pose
    public Guid Id { get; }

    // Could be extended to WidgetState if it is needed in the future
    public bool IsActive { get; protected set; }

    /// <summary>
    /// Runs this method, on every frame of SK
    /// </summary>
    void OnFrameUpdate();
}
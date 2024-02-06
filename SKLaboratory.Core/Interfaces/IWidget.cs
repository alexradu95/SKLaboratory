namespace SKLaboratory.Core.Interfaces;

/// <summary>
///     Represents the basic contract for a widget in the application.
/// </summary>
public interface IWidget
{
    /// <summary>
    ///     Gets the unique identifier for the widget.
    /// </summary>
    Guid Id { get; }

    /// <summary>
    ///     Method to update the widget frame. Called once per frame.
    /// </summary>
    void OnFrameUpdate();
}
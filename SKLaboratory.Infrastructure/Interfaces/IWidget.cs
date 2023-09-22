namespace SKLaboratory.Infrastructure.Interfaces;

/// <summary>
///     Interface for widgets. Widgets are components that when active, the OnFrameUpdate is run every frame.
/// </summary>
public interface IWidget
{
    public Guid Id { get; }

    public bool IsActive { get; protected set; }

    void OnFrameUpdate();
}
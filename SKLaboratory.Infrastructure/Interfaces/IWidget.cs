namespace SKLaboratory.Infrastructure.Interfaces;

public interface IWidget
{
    Guid Id { get; }
    bool IsActive { get; protected set; }
    void OnFrameUpdate();
}

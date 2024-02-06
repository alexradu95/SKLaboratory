namespace SKLaboratory.Core.Interfaces;

public interface IWidget
{
    Guid Id { get; }
    void OnFrameUpdate();
}
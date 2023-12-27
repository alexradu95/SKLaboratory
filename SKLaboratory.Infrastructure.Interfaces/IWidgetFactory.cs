namespace SKLaboratory.Infrastructure.Interfaces;
public interface IWidgetFactory
{
    IReadOnlyList<Type> RegisteredWidgetTypes { get; }
    IWidget CreateWidget(Type widgetType);
    void RegisterWidget<T>() where T : IWidget;
}
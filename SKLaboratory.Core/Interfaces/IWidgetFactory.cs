namespace SKLaboratory.Core.Interfaces;
public interface IWidgetFactory
{
    IReadOnlyList<Type> RegisteredWidgetTypes { get; }
    IWidget CreateWidget(Type widgetType);
    void RegisterWidget<T>(IWidgetCreator creator) where T : IWidget;
}
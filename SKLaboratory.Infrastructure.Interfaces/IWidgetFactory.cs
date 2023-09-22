namespace SKLaboratory.Infrastructure.Interfaces;
public interface IWidgetFactory
{
    public List<Type> WidgetTypes { get; }
    IWidget CreateWidget(Type widgetType);
    void RegisterWidget<T>() where T : IWidget;
}
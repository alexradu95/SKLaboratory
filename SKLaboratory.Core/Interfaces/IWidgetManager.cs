namespace SKLaboratory.Core.Interfaces;
public interface IWidgetManager
{
    IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }
    void ToggleWidgetVisibility(Type widgetType);
}
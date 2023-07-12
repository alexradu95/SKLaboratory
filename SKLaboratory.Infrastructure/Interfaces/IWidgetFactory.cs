namespace SKLaboratory.Infrastructure.Interfaces;

public interface IWidgetFactory
{
    public List<Type> WidgetTypes { get; }

    /// <summary>
    ///     Creates a new widget of the specified type.
    /// </summary>
    /// <param name="widgetType">The type of the widget to create.</param>
    /// <returns>A new widget of the specified type.</returns>
    /// <exception cref="UnknownWidgetTypeException">Thrown when the specified widget type is not registered.</exception>
    IWidget CreateWidget(Type widgetType);

    /// <summary>
    ///     Registers a new widget type with the factory.
    /// </summary>
    /// <param name="widgetType">The type of the widget to register.</param>
    /// </example>
    void RegisterWidget<T>() where T : IWidget;
}
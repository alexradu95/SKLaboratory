namespace SKLaboratory.Core.Interfaces;

/// <summary>
///     Defines a factory for creating and managing widgets.
/// </summary>
public interface IWidgetFactory
{
    /// <summary>
    ///     Gets a list of registered widget types.
    /// </summary>
    IReadOnlyList<Type> RegisteredWidgetTypes { get; }

    /// <summary>
    ///     Creates a widget of the specified type.
    /// </summary>
    /// <param name="widgetType">The type of widget to create.</param>
    /// <returns>An instance of the specified widget type.</returns>
    IWidget CreateWidget(Type widgetType);

    /// <summary>
    ///     Registers a widget type with the factory.
    /// </summary>
    /// <typeparam name="T">The type of widget to register, which must implement IWidget.</typeparam>
    /// <param name="creator">The IWidgetCreator instance that knows how to create the widget type.</param>
    void RegisterWidget<T>(IWidgetCreator creator) where T : IWidget;
}
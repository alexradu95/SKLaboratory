using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Factories;

public class WidgetFactory : IWidgetFactory
{
    /// <summary>
    /// A dictionary that maps widget types to functions that create widgets of those types.
    /// </summary>
    private readonly Dictionary<string, Func<IWidget>> _widgetCreators = new Dictionary<string, Func<IWidget>>();

    /// <summary>
    /// Creates a new widget of the specified type.
    /// </summary>
    /// <param name="widgetType">The type of the widget to create.</param>
    /// <returns>A new widget of the specified type.</returns>
    /// <exception cref="UnknownWidgetTypeException">Thrown when the specified widget type is not registered.</exception>
    public IWidget CreateWidget(string widgetType)
    {
        if (_widgetCreators.TryGetValue(widgetType, out var createWidgetFunc))
        {
            return createWidgetFunc();
        }

        throw new UnknownWidgetTypeException($"Unknown widget type: {widgetType}");
    }

    /// <summary>
    /// Registers a new widget type with the factory.
    /// </summary>
    /// <param name="widgetType">The type of the widget to register.</param>
    /// <param name="createWidgetFunc">
    /// A function that creates a new widget of the specified type.
    /// This is a delegate of type <see cref="Func{IWidget}"/>, which represents a function that takes no parameters and returns an <see cref="IWidget"/>.
    /// When you call <see cref="RegisterWidget"/>, you pass it a function that creates the type of widget you want to register.
    /// The factory can then use this function to create new widgets of that type.
    /// </param>
    /// <example>
    /// Here's an example of how you might use <see cref="RegisterWidget"/>:
    /// <code>
    /// widgetFactory.RegisterWidget("CubeWidget", () => new CubeWidget());
    /// </code>
    /// In this example, you're registering the "CubeWidget" type with the factory.
    /// You're passing a function that creates a new <see cref="CubeWidget"/> as the second parameter.
    /// The factory can then use this function to create new <see cref="CubeWidget"/> instances.
    /// </example>
    public void RegisterWidget(string widgetType, Func<IWidget> createWidgetFunc)
    {
        _widgetCreators[widgetType] = createWidgetFunc;
    }

}

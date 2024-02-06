namespace SKLaboratory.Core.Factories;

public class WidgetFactory : IWidgetFactory
{
	private readonly Dictionary<Type, IWidgetCreator> _widgetCreatorMap = new();
	public IReadOnlyList<Type> RegisteredWidgetTypes => _widgetCreatorMap.Keys.ToList();
	public IWidget CreateWidget(Type widgetType)
	{
		if (!_widgetCreatorMap.TryGetValue(widgetType, out var creator))
			throw new UnknownWidgetTypeException($"Widget type not registered: {widgetType}");
		return creator.CreateWidget();
	}
	public void RegisterWidget<T>(IWidgetCreator creator) where T : IWidget
	{
		var widgetType = typeof(T);
		ValidateWidgetType(widgetType);
		_widgetCreatorMap[widgetType] = creator;
	}
	private void ValidateWidgetType(Type widgetType)
	{
		if (!typeof(IWidget).IsAssignableFrom(widgetType))
			throw new ArgumentException("Provided type does not implement IWidget", nameof(widgetType));
		if (_widgetCreatorMap.ContainsKey(widgetType))
			throw new ArgumentException("This widget type is already registered", nameof(widgetType));
	}
}
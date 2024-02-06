namespace SKLaboratory;
public class ApplicationInitializer(IServiceProvider serviceProvider)
{
	private readonly IWidgetManager _widgetManager = serviceProvider.GetService<IWidgetManager>() 
	                                                 ?? throw new ArgumentNullException(nameof(IWidgetManager));
	private readonly IWidgetFactory _widgetFactory = serviceProvider.GetService<IWidgetFactory>() 
	                                                 ?? throw new ArgumentNullException(nameof(IWidgetFactory));
	private readonly WidgetRegistrar _widgetRegistrar = new(serviceProvider); // Assuming WidgetRegistrar can be directly instantiated

	public void Initialize()
	{
		AddPreInitSteppers();
		InitializeStereoKit();
		_widgetRegistrar.RegisterWidgetsToFactory();
		AddPostInitSteppers();
	}

	private void AddPreInitSteppers()
	{
		SK.AddStepper<PassthroughFbExt>();
	}

	private void InitializeStereoKit()
	{
		var settings = new SKSettings
		{
			appName = "SKLaboratory",
			assetsFolder = "Assets"
		};
		SK.Initialize(settings);
	}

	private void AddPostInitSteppers()
	{
		var widgetManagerUi = new WidgetManagerUi(_widgetManager, _widgetFactory);
		SK.AddStepper(widgetManagerUi);
		StartHandMenu.InitializeHandMenuStepper();
	}
}
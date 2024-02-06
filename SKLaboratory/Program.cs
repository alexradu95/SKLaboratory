namespace SKLaboratory;

public class SKLaboratory
{
	private static IServiceProvider _serviceProvider;

	public static void RunSKLaboratory()
	{
		_serviceProvider = BuildServiceProvider();
		InitializeApplication();
		RunMainLoop();
	}
	
	private static void RunMainLoop()
	{
		var widgetManager = _serviceProvider.GetRequiredService<IWidgetManager>();
		SK.Run(() =>
		{
			foreach (var widget in widgetManager.ActiveWidgetsList.Values)
				widget.OnFrameUpdate();
		});
	}

	private static IServiceProvider BuildServiceProvider()
	{
		var serviceCollection = new ServiceCollection();
		serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
		serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
		serviceCollection.AddSingleton<StartHandMenu>();
		serviceCollection.AddSingleton<MessageBus>();
		return serviceCollection.BuildServiceProvider();
	}

	private static void InitializeApplication()
	{
		AddPreInitSteppers();
		InitializeStereoKit();
		RegisterWidgetsToFactory();
		AddPostInitSteppers();
	}

	private static void AddPreInitSteppers()
	{
		SK.AddStepper<PassthroughFbExt>();
	}

	private static void InitializeStereoKit()
	{
		var settings = new SKSettings
		{
			appName = "SKLaboratory",
			assetsFolder = "Assets"
		};
		SK.Initialize(settings);
	}

	private static void RegisterWidgetsToFactory()
	{
		var messageBus = _serviceProvider.GetService<MessageBus>();
		var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();

		widgetFactory.RegisterWidget<ButtonWidget>(new ButtonWidgetCreator(messageBus));
		widgetFactory.RegisterWidget<TextWidget>(new TextWidgetCreator(messageBus));
	}

	private static void AddPostInitSteppers()
	{
		var widgetManager = _serviceProvider.GetService<IWidgetManager>();
		var widgetFactory = _serviceProvider.GetService<IWidgetFactory>();

		// Assuming WidgetManagerUI can be directly instantiated
		var widgetManagerUi = new WidgetManagerUi(widgetManager, widgetFactory);
		SK.AddStepper(widgetManagerUi);
		StartHandMenu.InitializeHandMenuStepper();
	}


}
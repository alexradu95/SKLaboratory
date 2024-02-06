namespace SKLaboratory;

public abstract class SKLaboratoryRunner
{
	private static IServiceProvider _serviceProvider;
	private static ApplicationInitializer _appInitializer;
	private static MainLoopRunner _mainLoopRunner;

	public static void Run()
	{
		_serviceProvider = ServiceProviderBuilder.BuildServiceProvider();
		_appInitializer = new ApplicationInitializer(_serviceProvider);
		_mainLoopRunner = new MainLoopRunner(_serviceProvider);

		_appInitializer.Initialize();
		_mainLoopRunner.RunMainLoop();
	}
}
namespace SKLaboratory;

public abstract class ServiceProviderBuilder
{
	public static IServiceProvider BuildServiceProvider()
	{
		var serviceCollection = new ServiceCollection();
		serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
		serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
		serviceCollection.AddSingleton<StartHandMenu>();
		serviceCollection.AddSingleton<MessageBus>();
		return serviceCollection.BuildServiceProvider();
	}
}
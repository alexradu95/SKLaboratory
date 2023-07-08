using Microsoft.Extensions.DependencyInjection;

namespace SKLaboratory.Initialization;

public class ServiceProviderBuilder
{
    public ServiceProvider Build()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<WidgetManager>();
        serviceCollection.AddSingleton<StereoKitInitializer>();
        serviceCollection.AddSingleton<ServiceProviderBuilder>();
        serviceCollection.AddSingleton<WidgetRegistrar>();

        return serviceCollection.BuildServiceProvider();
    }
}

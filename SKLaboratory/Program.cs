using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Factories;
using StereoKit;

namespace SKLaboratory;

internal class Program
{
    static void Main(string[] args)
    {
        ServiceProvider serviceProvider = BuildServiceProvider();

        var settings = new SKSettings
        {
            appName = "SKLaboratory",
            assetsFolder = "Assets",
        };

        SK.Initialize(settings);

        // Use the service provider to get your services
        var widgetManager = serviceProvider.GetRequiredService<WidgetManager>();


        SK.Run(() =>
        {
            foreach (var widget in widgetManager.ActiveWidgetsList.Values)
            {
                widget.Draw();
            }
        });
    }

    private static ServiceProvider BuildServiceProvider()
    {
        // Create a new service collection
        var serviceCollection = new ServiceCollection();

        // Register your services
        serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
        serviceCollection.AddSingleton<WidgetManager>();

        // Build the service provider
        var serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider;
    }
}

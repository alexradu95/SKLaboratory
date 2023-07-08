using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Widgets;
using StereoKit;
using StereoKit.Framework;
using System;
using System.Linq;

namespace SKLaboratory
{
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The service provider for managing dependencies.
        /// </summary>
        private static IServiceProvider _serviceProvider;

        /// <summary>
        /// The main method for the application.
        /// </summary>
        static void Main(string[] args)
        {
            // Configure the services needed for the application
            ConfigureServices();

            // Initialize the StereoKit library
            InitializeStereoKit();

            // Register the widgets that will be used in the application
            RegisterWidgets();

            // Configure the hand menu for the application
            ConfigureHandMenu();

            // Start the main loop of the application
            RunMainLoop();
        }

        /// <summary>
        /// Configures the services for the application.
        /// </summary>
        private static void ConfigureServices()
        {
            // Create a new service collection to register services
            var serviceCollection = new ServiceCollection();

            // Register the required services
            serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
            serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();

            // Build the service provider from the service collection
            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        /// <summary>
        /// Initializes the StereoKit library.
        /// </summary>
        private static void InitializeStereoKit()
        {
            var settings = new SKSettings
            {
                appName = "SKLaboratory",
                assetsFolder = "Assets",
            };

            SK.Initialize(settings);
        }

        /// <summary>
        /// Registers the widgets for the application.
        /// </summary>
        private static void RegisterWidgets()
        {
            // Get the widget registrar from the service provider and register the widgets
            var widgetRegistrar = _serviceProvider.GetRequiredService<IWidgetFactory>();
            widgetRegistrar.RegisterWidget<CubeWidget>();
            widgetRegistrar.RegisterWidget<FloorWidget>();
        }

        /// <summary>
        /// Configures the hand menu for the application.
        /// </summary>
        private static void ConfigureHandMenu()
        {
            // Get the widget factory and widget manager from the service provider
            var widgetFactory = _serviceProvider.GetRequiredService<IWidgetFactory>();
            var widgetManager = _serviceProvider.GetRequiredService<IWidgetManager>();

            // Create the menu items for the hand menu
            var widgetMenuItems = widgetFactory.WidgetTypes
                .Select(widgetType => new HandMenuItem(widgetType.Name, null, () => widgetManager.ToggleWidget(widgetType)))
                .ToList();

            // Add a back button to the menu items
            widgetMenuItems.Add(new HandMenuItem("Back", null, null, HandMenuAction.Back));

            // Create the hand menu with the menu items
            var handMenu = SK.AddStepper(new HandMenuRadial(
                new HandRadialLayer("Root",
                    new HandMenuItem("Log", null, () => Log.Info("Alex_Radu")),
                    new HandMenuItem("Boss 👻", null, () => Log.Info("Big_Boss")),
                    new HandMenuItem("Widgets", null, null, "Widgets")),
                new HandRadialLayer("Widgets", widgetMenuItems.ToArray())
            ));
        }

        /// <summary>
        /// Runs the main loop of the application.
        /// </summary>
        private static void RunMainLoop()
        {
            // Get the widget provider from the service provider
            var widgetProvider = _serviceProvider.GetRequiredService<IWidgetManager>();

            // Start the main loop of the application
            SK.Run(() =>
            {
                foreach (var widget in widgetProvider.ActiveWidgetsList.Values)
                {
                    widget.Draw();
                }
            });
        }
    }
}

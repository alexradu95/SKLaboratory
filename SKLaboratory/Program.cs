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
    /// The main class of the application.
    /// </summary>
    internal class Program
    {
        // Declare dependencies that will be injected via the constructor
        private readonly IWidgetFactory _widgetFactory;
        private readonly IWidgetManager _widgetManager;

        /// <summary>
        /// Constructs an instance of the Program class.
        /// </summary>
        /// <param name="stereoKitInitializer">An instance of StereoKitInitializer.</param>
        /// <param name="widgetFactory">An instance of IWidgetFactory.</param>
        /// <param name="widgetManager">An instance of IWidgetManager.</param>
        public Program(IWidgetFactory widgetFactory, IWidgetManager widgetManager)
        {
            _widgetFactory = widgetFactory;
            _widgetManager = widgetManager;
        }

        /// <summary>
        /// Runs the application.
        /// </summary>
        public void Run()
        {
            // Initialize StereoKit
            var settings = new SKSettings
            {
                appName = "SKLaboratory",
                assetsFolder = "Assets",
            };

            SK.Initialize(settings);

            // Register Widgets
            _widgetFactory.RegisterWidget<CubeWidget>();
            _widgetFactory.RegisterWidget<FloorWidget>();

            // Configure the hand menu
            ConfigureHandMenu();

            // Run the main loop
            RunMainLoop();
        }

        /// <summary>
        /// Configures the hand menu for the application.
        /// </summary>
        private void ConfigureHandMenu()
        {
            var widgetMenuItems = _widgetFactory.WidgetTypes
                .Select(widgetType => new HandMenuItem(widgetType.Name, null, () => _widgetManager.ToggleWidget(widgetType)))
                .ToList();

            widgetMenuItems.Add(new HandMenuItem("Back", null, null, HandMenuAction.Back));

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
        private void RunMainLoop()
        {
            SK.Run(() =>
            {
                foreach (var widget in _widgetManager.ActiveWidgetsList.Values)
                {
                    widget.Draw();
                }
            });
        }

        /// <summary>
        /// The entry point of the application.
        /// </summary>
        /// <param name="args">Command-line arguments.</param>
        static void Main(string[] args)
        {
            // Build the service provider
            // Create a new service collection to register services
            var serviceCollection = new ServiceCollection();

            // Register the required services
            serviceCollection.AddSingleton<IWidgetFactory, WidgetFactory>();
            serviceCollection.AddSingleton<IWidgetManager, WidgetManager>();
            serviceCollection.AddSingleton<Program>();

            // Build the service provider from the service collection
            var serviceProvider = serviceCollection.BuildServiceProvider();


            // Retrieve an instance of the Program class from the service provider
            var program = serviceProvider.GetRequiredService<Program>();

            // Run the application
            program.Run();
        }
    }
}

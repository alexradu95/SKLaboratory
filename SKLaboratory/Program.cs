// File: SKLaboratory/Program.cs

using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Factories;
using SKLaboratory.Initialization;
using SKLaboratory.Widgets;
using StereoKit;
using StereoKit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SKLaboratory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var serviceProvider = new ServiceProviderBuilder().Build();

            // Initialize StereoKit
            var stereoKitInitializer = serviceProvider.GetRequiredService<StereoKitInitializer>();
            stereoKitInitializer.Initialize();

            // Register Widgets
            var widgetRegistrar = serviceProvider.GetRequiredService<WidgetRegistrar>();

            widgetRegistrar.RegisterWidget<CubeWidget>();
            widgetRegistrar.RegisterWidget<FloorWidget>();

            // Activate default Widgets
            var widgetFactory = serviceProvider.GetRequiredService<IWidgetFactory>();
            var widgetManager = serviceProvider.GetRequiredService<WidgetManager>();
            ConfigureHandMenu(widgetFactory, widgetManager);

            // Run Main loop
            RunMainLoop(serviceProvider);
        }

        private static void ConfigureHandMenu(IWidgetFactory widgetFactory, WidgetManager widgetManager)
        {

            var widgetMenuItems = widgetFactory.WidgetTypes
                .Select(widgetType => new HandMenuItem(widgetType.Name, null, () => widgetManager.ToggleWidget(widgetType)))
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

        private static void RunMainLoop(ServiceProvider serviceProvider)
        {
            var widgetProvider = serviceProvider.GetRequiredService<WidgetManager>();
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

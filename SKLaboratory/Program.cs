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
                .ToArray();

            SK.AddStepper(new HandMenuRadial(
                new HandRadialLayer("Widgets", widgetMenuItems),
                new HandRadialLayer("File",
                    new HandMenuItem("New", null, () => Log.Info("New")),
                    new HandMenuItem("Open", null, () => Log.Info("Open")),
                    new HandMenuItem("Close", null, () => Log.Info("Close")),
                    new HandMenuItem("Back", null, null, HandMenuAction.Back)),
                new HandRadialLayer("Edit",
                    new HandMenuItem("Copy", null, () => Log.Info("Copy")),
                    new HandMenuItem("Paste", null, () => Log.Info("Paste")),
                    new HandMenuItem("Back", null, null, HandMenuAction.Back))));

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

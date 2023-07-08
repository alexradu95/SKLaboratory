// File: SKLaboratory/Program.cs

using Microsoft.Extensions.DependencyInjection;
using SKLaboratory.Factories;
using SKLaboratory.Initialization;
using SKLaboratory.Widgets;
using StereoKit;
using System;
using System.Collections.Generic;

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
            ActivateStartWidgets(serviceProvider);

            // Run Main loop
            RunMainLoop(serviceProvider);
        }

        private static void ActivateStartWidgets(ServiceProvider serviceProvider)
        {
            var widgetProvider = serviceProvider.GetRequiredService<WidgetManager>();
            widgetProvider.ActivateWidget(typeof(CubeWidget));
            widgetProvider.ActivateWidget(typeof(FloorWidget));
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

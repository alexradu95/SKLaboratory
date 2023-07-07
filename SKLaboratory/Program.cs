using SKLaboratory.Infrastructure;
using SKLaboratory.Widgets;
using StereoKit;
using System.Collections.Generic;

namespace SKLaboratory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Initialize StereoKit Engine
            if (!SK.Initialize(ConfigureSettings())) return;

            // Register widgets
            List<IWidget> Widgets = RegisterWidgets();

            // Initialize every widget
            Widgets.ForEach(widget => widget.Init());

            // Core application loop
            SK.Run(() => Widgets.ForEach(widget => widget.Update()));
        }

        private static List<IWidget> RegisterWidgets()
        {
            return new()
            {
                new CubeWidget(),
                new FloorWidget()
            };
        }

        private static SKSettings ConfigureSettings()
        {
            return new SKSettings
            {
                appName = "SKLaboratory",
                assetsFolder = "Assets",
            };
        }
    }
}
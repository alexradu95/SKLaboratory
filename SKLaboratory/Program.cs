using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Widgets;
using StereoKit;
using System.Collections.Generic;

namespace SKLaboratory
{
    internal class Program
    {
        public static List<IWidget> Widgets;

        static void Main(string[] args)
        {
            // Registering Steppers that are required to be initialized before SK
            InitializePreSteppers();

            // Initialize StereoKit Engine
            InitializeStereoKit();

            // Registering Steppers that will be used among the app
            InitializePostInitSteppers();

            RegisterWidgets();

            // We initialize all widgets for now, they will be initialized from the menu in the future
            Widgets.ForEach(widget => widget.Init());

            // Core application loop
            SK.Run(() => Widgets.ForEach(widget => widget.Update()));
        }

        private static void InitializeStereoKit()
        {
            var settings = new SKSettings
            {
                appName = "SKLaboratory",
                assetsFolder = "Assets",
            };

            SK.Initialize(settings);
        }

        private static void InitializePreSteppers()
        {

        }

        private static void InitializePostInitSteppers()
        {

        }

        private static void RegisterWidgets()
        {
            Widgets = new()
            {
                new CubeWidget(),
                new FloorWidget()
            };
        }
    }
}
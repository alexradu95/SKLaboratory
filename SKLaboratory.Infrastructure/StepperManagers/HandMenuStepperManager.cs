using global::SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Steppers;
using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Infrastructure
{
    public class HandMenuStepperManager : IStepperManager
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly IWidgetManager _widgetManager;

        HandMenuRadial? currentHandMenu;

        public HandMenuStepperManager(IWidgetFactory widgetFactory, IWidgetManager widgetManager)
        {
            _widgetFactory = widgetFactory;
            _widgetManager = widgetManager;
        }

        public void Initialize()
        {
            List<HandMenuItem> widgetMenuItems = ConstructWidgetsMenu();

            currentHandMenu = SK.AddStepper(new HandMenuRadial(
                new HandRadialLayer("Root", widgetMenuItems.ToArray())));
        }

        private List<HandMenuItem> ConstructWidgetsMenu()
        {
            var widgetMenuItems = _widgetFactory.WidgetTypes
                .Select(widgetType => new HandMenuItem(widgetType.Name, null, () => _widgetManager.ToggleWidget(widgetType)))
                .ToList();

            return widgetMenuItems;
        }

        public void Shutdown()
        {
            SK.RemoveStepper(currentHandMenu);
            currentHandMenu = null;
        }
    }
}

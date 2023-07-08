using StereoKit;

namespace SKLaboratory.ApplicationLifecycle
{
    public class MainAppLoop
    {
        private WidgetManager _widgetManager;

        public MainAppLoop(WidgetManager widgetManager)
        {
            _widgetManager = widgetManager;
        }

        public void Run()
        {
            SK.Run(() =>
            {
                foreach (var widget in _widgetManager.ActiveWidgetsList.Values)
                {
                    widget.Draw();
                }
            });
        }
    }

}

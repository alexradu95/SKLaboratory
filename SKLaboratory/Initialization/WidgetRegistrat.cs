using SKLaboratory.Widgets;

namespace SKLaboratory.Initialization
{
    public class WidgetRegistrar
    {
        private readonly IWidgetFactory _widgetFactory;

        public WidgetRegistrar(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        public void RegisterWidgets()
        {
            _widgetFactory.RegisterWidget(typeof(CubeWidget));
            _widgetFactory.RegisterWidget(typeof(FloorWidget));
        }
    }
}

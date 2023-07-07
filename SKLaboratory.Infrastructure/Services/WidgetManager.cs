using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Services
{
    public class WidgetManager
    {
        private List<IWidget> ActiveWidgets = new List<IWidget>();
        private IWidgetFactory _widgetFactory;

        public IReadOnlyList<IWidget> ActiveWidgetsList => ActiveWidgets.AsReadOnly();

        public WidgetManager(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        public void ActivateWidget(string widgetType)
        {
            var widget = _widgetFactory.CreateWidget(widgetType);

            if (widget == null || ActiveWidgets.Contains(widget))
            {
                return;
            }

            widget.Init();
            ActiveWidgets.Add(widget);
        }

        public void DeactivateWidget(string widgetType)
        {
            var widget = ActiveWidgets.Find(w => w.GetType().Name == widgetType);

            if (widget != null)
            {
                ActiveWidgets.Remove(widget);
            }
        }
    }
}

using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory
{
    public class WidgetManager
    {
        private List<IWidget> RegisteredWidgets = new List<IWidget>();
        private List<IWidget> ActiveWidgets = new List<IWidget>();

        public void RegisterWidget(IWidget widget, bool isUnique)
        {
            if (isUnique && RegisteredWidgets.Contains(widget))
            {
                // If the widget is unique and already registered, we don't add it again
                return;
            }

            RegisteredWidgets.Add(widget);
        }

        public void ActivateWidget(IWidget widget)
        {
            if (!RegisteredWidgets.Contains(widget))
            {
                // If the widget is not registered, we can't activate it
                return;
            }

            if (!ActiveWidgets.Contains(widget))
            {
                ActiveWidgets.Add(widget);
            }
        }

        public void DeactivateWidget(IWidget widget)
        {
            ActiveWidgets.Remove(widget);
        }

        public void DrawActiveWidgets()
        {
            foreach (var widget in ActiveWidgets)
            {
                widget.Draw();
            }
        }
    }
}

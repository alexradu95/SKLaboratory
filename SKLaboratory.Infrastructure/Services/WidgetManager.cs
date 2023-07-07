using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Widgets;
using System.Collections.Generic;
using System.Linq;

namespace SKLaboratory.Infrastructure.Services
{
    public class WidgetManager
    {
        private List<BaseWidget> RegisteredWidgets = new List<BaseWidget>();
        private List<BaseWidget> ActiveWidgets = new List<BaseWidget>();

        public IReadOnlyList<IWidget> ActiveWidgetsList => ActiveWidgets.AsReadOnly();

        public void RegisterWidget(BaseWidget widget)
        {
            if (RegisteredWidgets.Any(w => w.GetType() == widget.GetType()))
            {
                return;
            }

            RegisteredWidgets.Add(widget);
        }

        public void ActivateWidget(BaseWidget widget)
        {
            if (!RegisteredWidgets.Contains(widget))
            {
                return;
            }

            if (!ActiveWidgets.Contains(widget))
            {
                ActiveWidgets.Add(widget);
                widget.Init();
            }
        }

        public void DeactivateWidget(BaseWidget widget)
        {
            ActiveWidgets.Remove(widget);
        }

        public void ActivateAllWidgets(IWidgetFilter filter)
        {
            foreach (var widget in RegisteredWidgets)
            {
                if (filter.Filter(widget))
                {
                    ActivateWidget(widget);
                }
            }
        }

        public void DeactivateAllWidgets(IWidgetFilter filter)
        {
            var activeWidgetsCopy = new List<BaseWidget>(ActiveWidgets);

            foreach (var widget in activeWidgetsCopy)
            {
                if (filter.Filter(widget))
                {
                    DeactivateWidget(widget);
                }
            }
        }

    }
}

using SKLaboratory.Infrastructure.Interfaces;
using System.Collections.Generic;

namespace SKLaboratory.Infrastructure.Services
{
    /// <summary>
    /// Manages the lifecycle of widgets.
    /// </summary>
    public class WidgetManager
    {
        private readonly List<IWidget> ActiveWidgets = new List<IWidget>();
        private readonly IWidgetFactory _widgetFactory;

        /// <summary>
        /// Gets the list of active widgets.
        /// </summary>
        public IReadOnlyList<IWidget> ActiveWidgetsList => ActiveWidgets.AsReadOnly();

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetManager"/> class.
        /// </summary>
        /// <param name="widgetFactory">The widget factory.</param>
        public WidgetManager(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        /// <summary>
        /// Activates the widget of the specified type.
        /// </summary>
        /// <param name="widgetType">Type of the widget.</param>
        public void ActivateWidget(string widgetType)
        {
            var widget = GetWidget(widgetType);

            if (widget == null || ActiveWidgets.Contains(widget))
            {
                return;
            }

            if (widget.Initialize())
            {
                ActiveWidgets.Add(widget);
            }

        }

        /// <summary>
        /// Deactivates the widget of the specified type.
        /// </summary>
        /// <param name="widgetType">Type of the widget.</param>
        public void DeactivateWidget(string widgetType)
        {
            ActiveWidgets.Remove(GetWidget(widgetType));
        }

        /// <summary>
        /// Gets the widget of the specified type.
        /// </summary>
        /// <param name="widgetType">Type of the widget.</param>
        /// <returns>The widget of the specified type.</returns>
        private IWidget GetWidget(string widgetType)
        {
            return _widgetFactory.CreateWidget(widgetType);
        }
    }
}

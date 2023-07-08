using System;
using System.Collections.Generic;
using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Initialization
{
    public class WidgetRegistrar
    {
        private readonly IWidgetFactory _widgetFactory;

        public WidgetRegistrar(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        public void RegisterWidget<T>() where T : IWidget
        {
            _widgetFactory.RegisterWidget(typeof(T));
        }

        public void RegisterWidgets<T>(List<Type> widgetTypes) where T : IWidget
        {
            foreach (var widgetType in widgetTypes)
            {
                if (typeof(T).IsAssignableFrom(widgetType))
                {
                    _widgetFactory.RegisterWidget(widgetType);
                }
                else
                {
                    throw new ArgumentException($"The widget type '{widgetType.Name}' does not implement the required interface '{typeof(T).Name}'.");
                }
            }
        }
    }
}

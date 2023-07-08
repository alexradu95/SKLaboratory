using SKLaboratory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SKLaboratory.Infrastructure.Services
{
    // WidgetRegistrar.cs
    public class WidgetRegistrar : IWidgetRegistrar
    {
        private readonly IWidgetFactory _widgetFactory;
        private readonly IEnumerable<Type> _widgetTypes;

        public WidgetRegistrar(IWidgetFactory widgetFactory, IEnumerable<Type> widgetTypes)
        {
            _widgetFactory = widgetFactory;
            _widgetTypes = widgetTypes;
        }

        public void RegisterWidgets()
        {
            foreach (var widgetType in _widgetTypes)
            {
                _widgetFactory.RegisterWidget(widgetType);
            }
        }
    }

}

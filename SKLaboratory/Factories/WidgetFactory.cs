using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Widgets;
using System;

namespace SKLaboratory.Factories
{
    public class WidgetFactory : IWidgetFactory
    {
        public IWidget CreateWidget(string widgetType)
        {
            switch (widgetType)
            {
                case "CubeWidget":
                    return new CubeWidget();
                case "FloorWidget":
                    return new FloorWidget();
                // Add more cases as needed for other widget types
                default:
                    throw new ArgumentException($"Invalid widget type: {widgetType}");
            }
        }
    }

}

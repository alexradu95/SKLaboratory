using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Filters;

public class DrawableWidgetFilter : IWidgetFilter
{
    public bool Filter(BaseWidget widget)
    {
        return widget is IDrawable;
    }
}

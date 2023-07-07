using SKLaboratory.Infrastructure.Interfaces;
using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Filters;

public class UniqueWidgetFilter : IWidgetFilter
{
    public bool Filter(BaseWidget widget)
    {
        return widget.IsUnique;
    }
}

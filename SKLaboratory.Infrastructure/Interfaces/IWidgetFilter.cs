using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Interfaces;

public interface IWidgetFilter
{
    bool Filter(BaseWidget widget);
}

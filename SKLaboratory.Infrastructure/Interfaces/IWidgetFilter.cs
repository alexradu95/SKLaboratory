using SKLaboratory.Infrastructure.Widgets;

namespace SKLaboratory.Infrastructure.Interfaces;


/// <summary>
/// Interface for widget filters. Widget filters are used to filter widgets based on certain criteria.
/// </summary>
public interface IWidgetFilter
{
    /// <summary>
    /// Filters a widget.
    /// </summary>
    /// <param name="widget">The widget to filter.</param>
    /// <returns>True if the widget passes the filter, false otherwise.</returns>
    bool Filter(BaseWidget widget);
}


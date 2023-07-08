using SKLaboratory.Factories;
using SKLaboratory.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;

public class WidgetManager
{
    private readonly Dictionary<Type, IWidget> _activeWidgets = new Dictionary<Type, IWidget>();
    private readonly IWidgetFactory _widgetFactory;

    public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

    public WidgetManager(IWidgetFactory widgetFactory)
    {
        _widgetFactory = widgetFactory;
    }

    public bool ToggleWidget(Type widgetType)
    {
        try
        {
            if (_activeWidgets.ContainsKey(widgetType))
            {
                return DeactivateWidget(widgetType);
            }
            else
            {
                return ActivateWidget(widgetType);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return false;
        }
    }

    private bool ActivateWidget(Type widgetType)
    {
        var widget = _widgetFactory.CreateWidget(widgetType);
        if (widget == null || !widget.Initialize())
        {
            return false;
        }

        _activeWidgets.Add(widgetType, widget);
        return true;
    }

    private bool DeactivateWidget(Type widgetType)
    {
        _activeWidgets[widgetType].Shutdown();
        _activeWidgets.Remove(widgetType);
        return true;
    }
}

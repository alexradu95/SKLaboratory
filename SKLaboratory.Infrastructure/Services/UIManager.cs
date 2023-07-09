﻿using StereoKit.Framework;
using StereoKit;
using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Services;

public class UIManager
{

    public UIManager(IWidgetManager widgetManager, IWidgetFactory widgetFactory)
    {
        var widgetMenuItems = widgetFactory.WidgetTypes.Select(widgetType => new HandMenuItem(widgetType.Name, null, () => widgetManager.ToggleWidget(widgetType))).ToArray();

        // Construct a StereoKit Framework HandMenu based on the widgetMenuItems
        SK.AddStepper(new HandMenuRadial(new HandRadialLayer("Root", widgetMenuItems)));
    }

}
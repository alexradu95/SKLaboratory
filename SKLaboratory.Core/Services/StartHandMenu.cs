﻿using StereoKit;
using StereoKit.Framework;

namespace SKLaboratory.Infrastructure.Services;

public class StartHandMenu
{
    public static void InitializeHandMenuStepper()
    {

        var widgetManagerUI = SK.GetOrCreateStepper<WidgetManagerUI>();
        SK.AddStepper(new HandMenuRadial(new HandRadialLayer("Root", new HandMenuItem("Toggle WidgetMenu", null, widgetManagerUI.Toggle))));
    }
}
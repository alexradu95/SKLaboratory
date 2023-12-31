﻿namespace SKLaboratory.Infrastructure.Interfaces;

public interface IWidgetManager
{
    IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }
    void ToggleWidgetActivation(Type widgetType);
}
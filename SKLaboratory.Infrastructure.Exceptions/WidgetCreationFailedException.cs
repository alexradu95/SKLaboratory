﻿using System.Runtime.Serialization;

namespace SKLaboratory.Infrastructure.Exceptions;

[Serializable]
public class WidgetCreationFailedException : Exception
{
    public WidgetCreationFailedException()
    {
    }

    public WidgetCreationFailedException(string? message) : base(message)
    {
    }

    public WidgetCreationFailedException(string? message, Exception ex) : base(message, ex)
    {
    }
}
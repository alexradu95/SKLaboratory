using System;
using System.Runtime.Serialization;

namespace SKLaboratory.Factories
{
    [Serializable]
    public class UnknownWidgetTypeException : Exception
    {
        public UnknownWidgetTypeException(string message) : base(message) { }
    }
}
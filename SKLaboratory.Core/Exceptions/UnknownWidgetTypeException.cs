namespace SKLaboratory.Core.Exceptions;

[Serializable]
public class UnknownWidgetTypeException(string message) : Exception(message);
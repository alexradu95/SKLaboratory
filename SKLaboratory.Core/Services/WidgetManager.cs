namespace SKLaboratory.Core.Services
{
    public class WidgetManager(IWidgetFactory widgetFactory) : IWidgetManager
    {
        private readonly Dictionary<Type, IWidget> _activeWidgets = new();
        public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

        public void ToggleWidgetVisibility(Type widgetType)
        {
            if (IsWidgetActive(widgetType))
                DeactivateWidget(widgetType);
            else
                ActivateWidget(widgetType);
        }

        private void ActivateWidget(Type widgetType)
        {
            try
            {
                _activeWidgets[widgetType] = widgetFactory.CreateWidget(widgetType);
            }
            catch (Exception ex)
            {
                HandleWidgetException(ex, widgetType);
            }
        }

        private void DeactivateWidget(Type widgetType)
        {
            _activeWidgets.Remove(widgetType);
        }

        private void HandleWidgetException(Exception ex, Type widgetType)
        {
            Log.Err(BuildErrorMessage(ex, widgetType));
        }

        private bool IsWidgetActive(Type widgetType)
        {
            return _activeWidgets.ContainsKey(widgetType);
        }

        private string BuildErrorMessage(Exception ex, Type widgetType)
        {
            return ex is WidgetCreationFailedException or UnknownWidgetTypeException
                ? $"Widget operation failed for type {widgetType}: {ex.Message}"
                : $"An unexpected error occurred: {ex.Message}";
        }
    }
}
namespace SKLaboratory.Core.Interfaces
{
    /// <summary>
    /// Defines the contract for managing widgets in the application.
    /// </summary>
    public interface IWidgetManager
    {
        /// <summary>
        /// Gets a read-only dictionary of active widgets, keyed by their type.
        /// </summary>
        IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }

        /// <summary>
        /// Toggles the activation state of a widget of the specified type.
        /// If the widget is currently inactive, it will be activated, and vice versa.
        /// </summary>
        /// <param name="widgetType">The type of widget to toggle activation for.</param>
        void ToggleWidgetVisibility(Type widgetType);
    }
}

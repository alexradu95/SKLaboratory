using SKLaboratory.Infrastructure.Exceptions;
using SKLaboratory.Infrastructure.Interfaces;
using StereoKit;
using System;
using System.Collections.Generic;

namespace SKLaboratory.Infrastructure.Services
{
    /// <summary>
    /// Manages the active widgets in the application.
    /// </summary>
    public class WidgetManager : IWidgetManager
    {
        private readonly Dictionary<Type, IWidget> _activeWidgets = new();
        private readonly IWidgetFactory _widgetFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="WidgetManager"/> class.
        /// </summary>
        /// <param name="widgetFactory">The widget factory used to create widgets.</param>
        public WidgetManager(IWidgetFactory widgetFactory)
        {
            _widgetFactory = widgetFactory;
        }

        /// <summary>
        /// Gets a read-only view of the active widgets.
        /// </summary>
        public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList => _activeWidgets;

        /// <summary>
        /// Toggles the activation state of a widget.
        /// </summary>
        /// <param name="widgetType">The type of the widget to toggle.</param>
        public void ToggleWidget(Type widgetType)
        {
            try
            {
                if (_activeWidgets.ContainsKey(widgetType))
                    DeactivateWidget(widgetType);
                else
                    ActivateWidget(widgetType);
            }
            catch (WidgetCreationFailedException ex)
            {
                Log.Err($"Failed to create or deactivate widget of type: {widgetType}. {ex.Message}");
            }
            catch (UnknownWidgetTypeException ex)
            {
                Log.Err($"Unknown widget type: {widgetType}. {ex.Message}");
            }
            catch (Exception ex)
            {
                Log.Err($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Activates a widget of the specified type.
        /// </summary>
        /// <param name="widgetType">The type of the widget to activate.</param>
        private void ActivateWidget(Type widgetType)
        {
            _activeWidgets.Add(widgetType, _widgetFactory.CreateWidget(widgetType));
        }

        /// <summary>
        /// Deactivates a widget of the specified type.
        /// </summary>
        /// <param name="widgetType">The type of the widget to deactivate.</param>
        private void DeactivateWidget(Type widgetType)
        {
            _activeWidgets.Remove(widgetType);
        }
    }
}

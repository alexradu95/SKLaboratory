using SKLaboratory.Infrastructure.Interfaces;

namespace SKLaboratory.Infrastructure.Widgets
{
    /// <summary>
    /// Base class for all widgets in the system.
    /// </summary>
    public abstract class BaseWidget : IWidget
    {
        /// <summary>
        /// Gets the unique identifier for the widget.
        /// </summary>
        public Guid Id { get; } = Guid.NewGuid();

        /// <summary>
        /// Gets or sets a value indicating whether the widget is unique.
        /// If a widget is unique, only one instance of it can be active at a time.
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// Initializes the widget. This method is called when the widget is activated.
        /// </summary>
        public abstract void Init();

        /// <summary>
        /// Shuts down the widget. This method is called when the widget is deactivated.
        /// </summary>
        public abstract void Shutdown();

        /// <summary>
        /// Draws the widget. This method is called every frame while the widget is active.
        /// </summary>
        public abstract void Draw();
    }
}
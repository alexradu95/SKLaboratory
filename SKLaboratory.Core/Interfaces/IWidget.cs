namespace SKLaboratory.Core.Interfaces
{
    /// <summary>
    /// Represents the basic contract for a widget in the application.
    /// </summary>
    public interface IWidget
    {
        /// <summary>
        /// Gets the unique identifier for the widget.
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the widget is active.
        /// </summary>
        bool IsVisible { get; set; }

        /// <summary>
        /// Method to update the widget frame. Called once per frame.
        /// </summary>
        void OnFrameUpdate();
    }
}

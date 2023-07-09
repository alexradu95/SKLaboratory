

namespace SKLaboratory.Infrastructure.Interfaces
{
    public interface IWidgetManager
    {
        public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }

        public bool ToggleWidget(Type widgetType);
    }
}

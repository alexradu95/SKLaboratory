

namespace SKLaboratory.Infrastructure.Interfaces
{
    public interface IWidgetManager
    {
        public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }

        public void ToggleWidget(Type widgetType);
    }
}



namespace SKLaboratory.Infrastructure.Interfaces
{
    public interface IWidgetManager
    {
        public IReadOnlyDictionary<Type, IWidget> ActiveWidgetsList { get; }
    }
}

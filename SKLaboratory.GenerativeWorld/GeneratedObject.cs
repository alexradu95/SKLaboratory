using Newtonsoft.Json.Linq;
using StereoKit;

namespace SKLaboratory.GenerativeWorld
{
    public class GeneratedObject
    {
        public int Id { get; }
        private Model _model;
        private Color _color;
        private Pose _pose = Pose.Identity;
        private Vec3 _scale = Vec3.One;
        private string Shape { get; set; }

        public GeneratedObject(int id, JObject data)
        {
            Id = id;
            UpdateFromJson(data);
        }

        private readonly Dictionary<string, System.Func<Model>> _shapeToModelFunc = new Dictionary<string, System.Func<Model>>
        {
            { "cube", () => Model.FromMesh(Mesh.Cube, Material.UI) },
            { "sphere", () => Model.FromMesh(Mesh.Sphere, Material.UI) },
            { "cylinder", () => Model.FromMesh(Mesh.GenerateCylinder(1.0f, 1.0f, Vec3.Up), Material.UI) },
        };

        public void UpdateFromJson(JObject data)
        {
            if (TryGetValueFromJson(data, "position", out JObject pos))
            {
                _pose.position = JsonConverter.FromJsonVec3(pos);
            }

            if (TryGetValueFromJson(data, "scale", out JObject scale))
            {
                _scale = JsonConverter.FromJsonVec3(scale);
            }

            if (TryGetValueFromJson(data, "shape", out JToken shape))
            {
                var shapeStr = shape.ToString();
                Shape = shapeStr;

                _model = _shapeToModelFunc.ContainsKey(shapeStr)
                    ? _shapeToModelFunc[shapeStr]()
                    : Model.FromMesh(Mesh.Cube, Material.UI);
            }

            if (TryGetValueFromJson(data, "color", out JObject color))
            {
                _color = JsonConverter.FromJsonColor(color);
            }
        }

        private bool TryGetValueFromJson<T>(JObject data, string propertyName, out T propertyValue)
        {
            var isPropertyPresent = data.TryGetValue(propertyName, out JToken intermediateValue);
            propertyValue = isPropertyPresent ? intermediateValue.ToObject<T>() : default;
            return isPropertyPresent;
        }

        public void Draw()
        {
            var worldPositionOffset = new Vec3(0, -0.5f, -1);
            var worldScaleOffset = new Vec3(0.5f, 0.5f, 0.5f);
            var worldOffset = Matrix.TS(worldPositionOffset, worldScaleOffset);

            _model.Draw(_pose.ToMatrix(_scale) * worldOffset, _color);
        }
    }
}

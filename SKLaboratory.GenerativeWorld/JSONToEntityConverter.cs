using Newtonsoft.Json.Linq;
using StereoKit;
namespace VRWorld
{
	internal static class JSONConverter
	{
		// Function to add elements to JObject
		private static void AddToJSON(JObject obj, string key, float value)
		{
			obj.Add(key, value);
		}

		//Vec3
		public static Vec3 FromJSONVec3(JObject someData) => new Vec3((float)someData.GetValue("x"), (float)someData.GetValue("y"), (float)someData.GetValue("z"));

		public static JObject ToJSON(Vec3 someData)
		{
			JObject result = new JObject();
			AddToJSON(result, "x", someData.x);
			AddToJSON(result, "y", someData.y);
			AddToJSON(result, "z", someData.z);
			return result;
		}

		//Color
		public static Color FromJSONColor(JObject someData) => new Color((float)someData.GetValue("r"), (float)someData.GetValue("g"), (float)someData.GetValue("b"));

		public static JObject ToJSON(Color someData)
		{
			JObject result = new JObject();
			AddToJSON(result, "r", someData.r);
			AddToJSON(result, "g", someData.g);
			AddToJSON(result, "b", someData.b);
			return result;
		}

		//Quaternion
		public static Quat FromJSONQuat(JObject someData) => new Quat((float)someData.GetValue("x"), (float)someData.GetValue("y"), (float)someData.GetValue("z"), (float)someData.GetValue("w"));
		
		public static JObject ToJSON(Quat someData)
		{
			JObject result = new JObject();
			AddToJSON(result, "x", someData.x);
			AddToJSON(result, "y", someData.y);
			AddToJSON(result, "z", someData.z);
			AddToJSON(result, "w", someData.w);
			return result;
		}
	}
}
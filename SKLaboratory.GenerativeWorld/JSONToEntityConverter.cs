using Newtonsoft.Json.Linq;
using StereoKit;

namespace SKLaboratory.GenerativeWorld
{
	internal static class JsonConverter
	{
		// Function to add elements to JObject
		private static void AddToJson(JObject obj, string key, float value)
		{
			obj.Add(key, value);
		}

		//Vec3
		public static Vec3 FromJsonVec3(JObject someData) => new Vec3((float)someData.GetValue("x"), (float)someData.GetValue("y"), (float)someData.GetValue("z"));

		public static JObject ToJson(Vec3 someData)
		{
			var result = new JObject();
			AddToJson(result, "x", someData.x);
			AddToJson(result, "y", someData.y);
			AddToJson(result, "z", someData.z);
			return result;
		}

		//Color
		public static Color FromJsonColor(JObject someData) => new Color((float)someData.GetValue("r"), (float)someData.GetValue("g"), (float)someData.GetValue("b"));

		public static JObject ToJson(Color someData)
		{
			var result = new JObject();
			AddToJson(result, "r", someData.r);
			AddToJson(result, "g", someData.g);
			AddToJson(result, "b", someData.b);
			return result;
		}

		//Quaternion
		public static Quat FromJsonQuat(JObject someData) => new Quat((float)someData.GetValue("x"), (float)someData.GetValue("y"), (float)someData.GetValue("z"), (float)someData.GetValue("w"));
		
		public static JObject ToJson(Quat someData)
		{
			var result = new JObject();
			AddToJson(result, "x", someData.x);
			AddToJson(result, "y", someData.y);
			AddToJson(result, "z", someData.z);
			AddToJson(result, "w", someData.w);
			return result;
		}
	}
}
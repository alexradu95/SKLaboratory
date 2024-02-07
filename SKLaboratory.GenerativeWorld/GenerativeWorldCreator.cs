using Newtonsoft.Json.Linq;
using SKLaboratory.Core.Interfaces;

namespace SKLaboratory.GenerativeWorld;

public class GenerativeWorldCreator : IWidgetCreator
{
	public IWidget CreateWidget()
	{
		var x =  new GenerativeWorldWidget("","","", null,null);

		var content =
			"{\n  \"id\": 0,\n  \"position\": {\n    \"x\": 1,\n    \"y\": 2,\n    \"z\": 1\n  },\n  \"scale\": {\n    \"x\": 1.0,\n    \"y\": 1.0,\n    \"z\": 1.0\n  },\n  \"shape\": \"cube\",\n  \"color\": {\n    \"r\": 1.0,\n    \"g\": 0.0,\n    \"b\": 0.0\n  }\n}\n";
		var jobject = JObject.Parse(content);
		x.AddObject(new GeneratedObject(1, jobject));
		return x;
	}
}
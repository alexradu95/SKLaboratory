using Newtonsoft.Json.Linq;
using OpenAI_API;
using OpenAI_API.Completions;

namespace SKLaboratory.GenerativeWorld;

public class ObjectGenerator : IObjectGenerator
{
       public async Task<CompletionResult> GenerateAiResponse(OpenAIAPI anApi, string aPrompt)
       {
           var request = new CompletionRequest(
               prompt: aPrompt,
               model: OpenAI_API.Models.Model.DefaultModel,
               temperature: 0.1,
               max_tokens: 256,
               top_p: 1.0,
               frequencyPenalty: 0.0,
               presencePenalty: 0.0,
               stopSequences: new string[] { "text:", "json:", "\n" }
           );
           var result = await anApi.Completions.CreateCompletionAsync(request);
           return result;
       }

       public void HandleAiResponse(string aResponce, List<GeneratedObject> someObjects)
       {
           JObject jResponce = JObject.Parse(aResponce);
           int id = (int)jResponce.GetValue("id");

           //Remove
           jResponce.TryGetValue("remove", out JToken jRemove);
           jResponce.TryGetValue("delete", out JToken jDelete);
           bool remove = jRemove != null && (bool)jRemove;
           bool delete = jDelete != null && (bool)jDelete;
           if (remove || delete)
           {
               for (int i = 0; i < someObjects.Count; i++)
               {
                   if (someObjects[i].Id == id)
                   {
                       int lastIndex = someObjects.Count - 1;
                       someObjects[i] = someObjects[lastIndex];
                       someObjects.RemoveAt(lastIndex);
                       i--; //new object at current postion
                       break;
                   }
               }
           }
           else //Update or add new object
           {
               bool foundObject = false;
               for (int i = 0; i < someObjects.Count; i++)
               {
                   if (someObjects[i].Id == id)
                   {
                       someObjects[i].UpdateFromJson(jResponce);
                       foundObject = true;
                       break;
                   }
               }

               if (!foundObject) //Create a new object
               {
                   someObjects.Add(new GeneratedObject(id, jResponce));
               }
           }
       }
}
using OpenAI_API.Completions;

namespace SKLaboratory.GenerativeWorld;

public interface IObjectGenerator 
{
	Task<CompletionResult> GenerateAiResponse(OpenAI_API.OpenAIAPI anApi, string aPrompt);
	void HandleAiResponse(string aResponce, List<GeneratedObject> someObjects);
}
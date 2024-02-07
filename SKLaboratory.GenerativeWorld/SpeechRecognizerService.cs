using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;

namespace SKLaboratory.GenerativeWorld;

public class SpeechRecognizerService : ISpeechRecognizerService
{
	private Action _checkRecordMic;
	private bool _record;
	private SpeechRecognizer _speechRecognizer;
	private string _speechAiText;
	private string _textInput;

	public void CreateSpeechRecognizer()
	{
		var speechConfig = SpeechConfig.FromSubscription("9abb06bd923b40fc9b99692bc077c9e9", "westeurope");
		speechConfig.SpeechRecognitionLanguage = "en-US";
            
		using var audioConfig = AudioConfig.FromDefaultMicrophoneInput();
		using var speechRecognizer = new SpeechRecognizer(speechConfig, audioConfig);
            
            
		speechRecognizer.Recognizing += (s, e) =>
		{
			_speechAiText = e.Result.Text;
		};
		speechRecognizer.Recognized += (s, e) =>
		{
			_textInput += _speechAiText;
			_speechAiText = "";
		};

		_checkRecordMic = () =>
		{
			if (_record)
			{
				speechRecognizer.StartContinuousRecognitionAsync().Wait();
			}
			else
			{
				speechRecognizer.StopContinuousRecognitionAsync().Wait();
			}
		};
	}

	public Action CheckRecordMic()
	{
		return _checkRecordMic;
	}

	// Add Dispose method to dispose SpeechRecognizer and any other IDisposable resources
	public void Dispose()
	{
		_speechRecognizer?.Dispose();
	}
}
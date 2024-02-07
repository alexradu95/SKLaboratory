using Microsoft.CognitiveServices.Speech;
using Microsoft.CognitiveServices.Speech.Audio;
using Newtonsoft.Json.Linq;
using OpenAI_API;
using OpenAI_API.Completions;
using SKLaboratory.Core.BaseClasses;
using StereoKit;

namespace SKLaboratory.GenerativeWorld
{
    public class GenerativeWorldWidget : BaseWidget
    {
        private readonly List<GeneratedObject> _objects = [];
        private readonly OpenAIAPI _openAiApi;

        private string _aiText = "Create a json block from prompt.\nExample:\ntext:Create a blue cube at position zero zero zero\njson:{\"id\": 0, \"position\": {\"x\": 0, \"y\": 0, \"z\": 0}, \"scale\": {\"x\": 1.0, \"y\": 1.0, \"z\": 1.0}, \"shape\": \"cube\", \"color\": {\"r\": 0.0, \"g\": 0.0, \"b\": 1.0}}\ntext:remove or delete the blue cube\njson:{\"id\": 0, \"remove\": true}\nReal start with id 0:\ntext:";
        private const string StartSequence = "\njson:";
        private const string RestartSequence = "\ntext:\n";
        private Task<CompletionResult> _generateTask = null;

        private Pose _windowPose = new Pose(0.4f, 0.09f, -0.32f, Quat.LookDir(-0.7f, 0.09f, 0.71f));
        private Pose _buttonPose = new Pose(0.04f, -0.32f, -0.34f, Quat.LookDir(-0.03f, 0.64f, 0.76f));
        
        private readonly IObjectGenerator _objectGenerator;
        private readonly ISpeechRecognizerService _speechRecognizerService;

        //Microphone and text
        private bool _record = true;
        private string _textInput = "";
        private string _speechAiText = "";

        private Action _checkRecordMic;

        public GenerativeWorldWidget(string openAiKey, string speechKey, string speechRegion, IObjectGenerator objectGenerator, ISpeechRecognizerService speechRecognizerService)
        {
            _objectGenerator = objectGenerator;
            _speechRecognizerService = speechRecognizerService;
            _speechRecognizerService.CreateSpeechRecognizer();

            _speechRecognizerService.CheckRecordMic()();
        }

        public void AddObject(GeneratedObject obj)
        {
            _objects.Add(obj);
        }

        public override void OnFrameUpdate()
        {
                UI.WindowBegin("Open AI chat", ref _windowPose, new Vec2(30, 0) * U.cm);

                //Get the 200 last characters of aiText
                int showLength = 1000;
                string showText = _aiText.Length > showLength ? "..." + _aiText.Substring(_aiText.Length - showLength) : _aiText;
                UI.Text(showText);

                if (_speechAiText == "") //no AI speech == can edit text
                {
                    UI.Input("Input", ref _textInput);
                }
                else //AI speech can not edit text
                {
                    string sum = _textInput + _speechAiText;
                    UI.Input("Input", ref sum);
                }
                UI.WindowEnd();

                UI.WindowBegin("Buttons", ref _buttonPose, new Vec2(30, 0) * U.cm);
                UI.PushTint(_record ? new Color(1, 0.1f, 0.1f) : Color.White); //red when recording
                if (UI.Toggle("Mic(F1)", ref _record))
                {
                    _checkRecordMic();
                }
                if((Input.Key(Key.F1) & BtnState.JustActive) > 0) //keyboard 'M'
                {
                    _record = !_record; //switch value
                    _checkRecordMic();
                }
                
                UI.PopTint();

                UI.SameLine();
                if (UI.Button("Clear(F2)") || (Input.Key(Key.F2) & BtnState.JustActive) > 0)
                {
                    _textInput = "";
                }
                UI.SameLine();
                UI.PushTint(new Color(0.5f, 0.5f, 1));
                bool submit = UI.Button("Submit") || (Input.Key(Key.Return) & BtnState.JustActive) > 0;
                if (_textInput != "" && submit)
                {
                    _aiText += _textInput + StartSequence;
                    _generateTask = _objectGenerator.GenerateAiResponse(_openAiApi, _aiText);
                   
                    _textInput = ""; //Clear input
                    
                }
                UI.PopTint();
                UI.WindowEnd();

                if (_generateTask != null && _generateTask.IsCompleted)
                {
                    string responce = _generateTask.Result.ToString();
                    _objectGenerator.HandleAiResponse(responce, _objects);
                    _aiText += responce + RestartSequence;
                    _generateTask = null;
                }

                foreach(GeneratedObject o in _objects)
                {
                    o.Draw();
                }
        }
    }
}
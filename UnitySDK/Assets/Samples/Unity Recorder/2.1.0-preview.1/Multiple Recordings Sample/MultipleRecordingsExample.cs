#if UNITY_EDITOR

using System;
using System.IO;
using UnityEditor;
using UnityEditor.Recorder;
using UnityEditor.Recorder.Input;

namespace UnityEngine.Recorder.Examples
{
    /// <summary>
    /// This example shows how to setup a recording session via script.
    /// To use this example. Simply add the MultipleRecordingsExample component to a GameObject.
    /// 
    /// Entering playmode will start the recording.
    /// The recording will automatically stops when exiting playmode (or when the component is disabled).
    /// 
    /// Recording outputs are saved in [Project Folder]/SampleRecordings (except for the recorded animation which is saved in Assets/SampleRecordings).  
    /// </summary>
    public class MultipleRecordingsExample : MonoBehaviour
    {
        RecorderController m_RecorderController;

        void OnEnable()
        {
            var controllerSettings = ScriptableObject.CreateInstance<RecorderControllerSettings>();
            m_RecorderController = new RecorderController(controllerSettings);

            var mediaOutputFolder = Path.Combine(Application.dataPath, "..", "SampleRecordings");
            // animation output is an asset that must be created in Assets folder
            var animationOutputFolder = Path.Combine(Application.dataPath, "SampleRecordings");

            // Video
            var videoRecorder = ScriptableObject.CreateInstance<MovieRecorderSettings>();
            videoRecorder.name = "My Video Recorder";
            videoRecorder.Enabled = true;

            videoRecorder.OutputFormat = MovieRecorderSettings.VideoRecorderOutputFormat.WebM;
            videoRecorder.VideoBitRateMode = VideoBitrateMode.High;

            videoRecorder.ImageInputSettings = new GameViewInputSettings
            {
                OutputWidth = 1920,
                OutputHeight = 1080
            };

            videoRecorder.AudioInputSettings.PreserveAudio = true;
            var timeStamp = string.Format("{0:yyyy-MM-dd_hh-mm-ss-tt}", DateTime.Now);
           videoRecorder.OutputFile = Path.Combine("C:\\Workspace\\Video Recorder\\", $@"Experiment-{timeStamp}");
            // Setup Recording
            controllerSettings.AddRecorderSettings(videoRecorder);

            controllerSettings.SetRecordModeToManual();
            controllerSettings.FrameRate = 60.0f;

            RecorderOptions.VerboseMode = false;
            m_RecorderController.StartRecording();
        }

        void OnDisable()
        {
            m_RecorderController.StopRecording();
        }
    }
}

#endif

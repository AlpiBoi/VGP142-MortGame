#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class SuppressAudioListenerWarnings
{
    static SuppressAudioListenerWarnings()
    {
        Application.logMessageReceived += HandleLog;
    }

    private static void HandleLog(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Warning && logString.Contains("AudioListeners in the scene"))
        {
            // ignore
            return;
        }
    }
}
#endif

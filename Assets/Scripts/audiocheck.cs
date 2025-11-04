using System.Net.WebSockets;
using UnityEngine;

public class audiocheck : MonoBehaviour
{


    //Check for audio listener
    void Start()
    {
        AudioListener[] listeners = FindObjectsOfType<AudioListener>(true);
        Debug.Log("AudioListeners in scene: " + listeners.Length);
        foreach (var l in listeners)
            Debug.Log("Listener: " + l.gameObject.name);
    }

  
}

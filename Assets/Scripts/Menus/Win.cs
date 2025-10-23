using UnityEngine;
using UnityEngine.UI;
public class Win : MonoBehaviour
{
    [SerializeField] private GameObject winCanvas;

    void Start()
    {
        if (winCanvas != null)
            winCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (winCanvas != null)
                winCanvas.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}

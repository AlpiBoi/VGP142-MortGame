using Unity.Cinemachine;
using UnityEngine;

public class CamTargeting : MonoBehaviour
{
    [SerializeField] private CinemachineCamera freeLookCam;

    private void Awake()
    {
        // Optionally find the FreeLook camera automatically
        if (freeLookCam == null)
            freeLookCam = FindFirstObjectByType<CinemachineCamera>();
    }

    public void SetTarget(Transform player)
    {
        if (freeLookCam == null)
        {
            Debug.LogWarning("No Cinemachine FreeLook camera assigned!");
            return;
        }

        freeLookCam.Follow = player;
        freeLookCam.LookAt = player;
    }

}

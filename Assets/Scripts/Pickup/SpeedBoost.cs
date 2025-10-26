using UnityEngine;

public class SpeedBoost : MonoBehaviour
{

    private float speedMult = 2f;
    private float duration = 1f;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player"))
        {
            playerController player = col.GetComponent<playerController>();
            if (player != null)
            {
                StartCoroutine(ApplySpeedBoost(player));
            }

            Destroy(gameObject);
        }
    }
    
    private System.Collections.IEnumerator ApplySpeedBoost(playerController player)
    {
        player.speed *= speedMult;
        
        yield return new WaitForSeconds(duration);

        player.speed /= speedMult;
        Debug.Log("Back to normal speed");
    }

}

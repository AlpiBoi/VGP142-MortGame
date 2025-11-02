using UnityEngine;

public class Pill : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Player"))
        {
            playerController player = other.GetComponent<playerController>();
            if (player != null)
            {
                throw new System.Exception("The pill's effect isn't implemented yet");
            }

            Destroy(gameObject);
        } 
    }
}

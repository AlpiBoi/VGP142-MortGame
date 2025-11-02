using UnityEngine;

public class TubeOfMeat : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerController player = other.GetComponent<playerController>();
            if (player != null)
            {
                throw new System.Exception("The tube of meat's effect isn't implemented yet");
            }

            Destroy(gameObject);
        }
    }
}

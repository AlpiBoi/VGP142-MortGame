using UnityEngine;

public class PlayerHitbox : MonoBehaviour
{
    private playerHealth pHealth;

    void Start()
    {
        pHealth = GetComponentInParent<playerHealth>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyProjectile"))
        {
            pHealth?.Die();
        }
    }
}

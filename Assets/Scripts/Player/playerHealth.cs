using UnityEngine;

public class playerHealth : MonoBehaviour
{
    private bool isDead = false;

    public void Die()
    {
        if (isDead) return;

        isDead = true;
        Debug.Log("Player has died!");
        GameOverManager.Instance.ShowGameOverScreen();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If hit by an enemy projectile
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            Die();
        }
    }
}

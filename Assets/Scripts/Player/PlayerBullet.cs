using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float speed = 15f;
    public float lifetime = 3f;

    void Start()
    {
        Destroy(gameObject, lifetime); // destroy after a few seconds automatically
    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        ShyEnemy enemy = other.GetComponent<ShyEnemy>();
        if (enemy != null)
        {
            enemy.Die(); // instantly kill enemy
        }

        Destroy(gameObject); // destroy bullet after hitting anything
    }
}

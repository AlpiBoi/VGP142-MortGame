using UnityEngine;

public class ShyEnemy : MonoBehaviour
{
    [Header("Refs")]
    public Transform player;
    public Camera playerCamera;

    [Header("movement stats")]
    public float moveSpeed = 3f; 
    public float rotationSpeed = 5f;
    private float stopDistance = 8f;

    [Header("Attack settings")]
    public GameObject fireballPrefab;
    public Transform firePoint;
    public float fireRate = 2f;
    private float nextFireTime = 0f;

    [Header("shyness")]
    public float viewAngle = 60f;
    public float hiddenAlpha = 0.50f;

    private Renderer enemyRenderer;
    private Color baseColor;


    private void Start()
    {
        enemyRenderer = GetComponent<Renderer>();
        if (enemyRenderer != null)
        {
            baseColor = enemyRenderer.material.color;
        }
    }
    void Update()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }
        if (playerCamera == null)
        {
            FindPlayerCamera();
            return;
        }

     
        bool playerLooking = IsPlayerLooking();

        //fade if out of sight
        SetTransparency(!playerLooking);
        //freeze if looked at
        if (playerLooking) {
            return;
        }

        Vector3 toPlayer =player.position - transform.position;  //distance to player
        float distance = toPlayer.magnitude;
        Vector3 direction = toPlayer.normalized; 
        RotateTowardsPlayer(direction);

        if (distance > stopDistance)
        {
            transform.position += direction * moveSpeed * Time.deltaTime;
        }
        else
        {
            TryShoot(direction);
        }
       
    }

    bool IsPlayerLooking()
    {
        Vector3 cameraPos = playerCamera.transform.position;
        Vector3 cameraForward = playerCamera.transform.forward;

        Vector3 toEnemy = transform.position - cameraPos;
        float distanceToEnemy = toEnemy.magnitude;
        Vector3 dirToEnemy = toEnemy.normalized;

        // Use full 3D angle check (includes vertical)
        float angleToEnemy = Vector3.Angle(cameraForward, dirToEnemy);

        // Check if enemy is inside the view cone
        if (angleToEnemy > viewAngle * 0.5f)
            return false;

        // Check if there’s a clear line of sight (no obstacles between camera and enemy)
        if (Physics.Raycast(cameraPos, dirToEnemy, out RaycastHit hit, distanceToEnemy))
        {
            if (hit.transform == transform || hit.transform.root == transform)
                return true;
        }

        return false;
    }

    void SetTransparency(bool transparent)
    {
        if (enemyRenderer == null) return;

        float alp = transparent ? hiddenAlpha : 1f;

        Color color = baseColor;
        color.a = alp;
        enemyRenderer.material.color = color;

        if (transparent)
        {
            enemyRenderer.material.SetFloat("_Mode", 2); // Fade mode
            enemyRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            enemyRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            enemyRenderer.material.SetInt("_ZWrite", 0);
            enemyRenderer.material.DisableKeyword("_ALPHATEST_ON");
            enemyRenderer.material.EnableKeyword("_ALPHABLEND_ON");
            enemyRenderer.material.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            enemyRenderer.material.renderQueue = 3000;
        }
        else
        {
            enemyRenderer.material.SetFloat("_Mode", 0); // Opaque mode
            enemyRenderer.material.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.One);
            enemyRenderer.material.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.Zero);
            enemyRenderer.material.SetInt("_ZWrite", 1);
            enemyRenderer.material.DisableKeyword("_ALPHABLEND_ON");
            enemyRenderer.material.renderQueue = -1;
        }
    }


    void RotateTowardsPlayer(Vector3 direction)
    {
        direction.y = 0f;

        if(direction.sqrMagnitude > 0.001f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void TryShoot(Vector3 direction)
    {
        if(Time.time >= nextFireTime)
        {
            if(fireballPrefab != null && firePoint != null)
            {
                GameObject fireball = Instantiate(fireballPrefab, firePoint.position, Quaternion.identity);
                fireball.GetComponent<Fireball>().Initialize(direction);
            }

            nextFireTime = Time.time + fireRate;
        }
    }
    void FindPlayer()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
    } //Get the player reference 

    void FindPlayerCamera()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            playerCamera = cam;
        }
    }

    public void Die()
    {
        Debug.Log("enemy Killed");
        Destroy(gameObject);
    }
 
}

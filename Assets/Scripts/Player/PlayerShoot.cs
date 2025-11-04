using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    [Header("References")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletSpeed = 25f;

    private InputSystem_Actions input;

    private void Awake()
    {
        input = new InputSystem_Actions();
        input.Player.SetCallbacks(this);
    }

    private void OnEnable()
    {
        input.Player.Enable();
    }

    private void OnDisable()
    {
        input.Player.Disable();
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Missing fire point or bullet prefab!");
            return;
        }

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Give bullet velocity
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.linearVelocity = firePoint.forward * bulletSpeed;
        }

     
        Destroy(bullet, 5f);
    }

    // Unused interface methods
    public void OnMove(InputAction.CallbackContext context) { }
    public void OnLook(InputAction.CallbackContext context) { }

    void InputSystem_Actions.IPlayerActions.OnInteract(InputAction.CallbackContext context)
    {
       // throw new System.NotImplementedException();
    }

    void InputSystem_Actions.IPlayerActions.OnCrouch(InputAction.CallbackContext context)
    {
       // throw new System.NotImplementedException();
    }

    void InputSystem_Actions.IPlayerActions.OnJump(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    void InputSystem_Actions.IPlayerActions.OnPrevious(InputAction.CallbackContext context)
    {
       // throw new System.NotImplementedException();
    }

    void InputSystem_Actions.IPlayerActions.OnNext(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    void InputSystem_Actions.IPlayerActions.OnSprint(InputAction.CallbackContext context)
    {
       // throw new System.NotImplementedException();
    }
}

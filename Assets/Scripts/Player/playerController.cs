using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions input;      
    CharacterController cc;
    Camera mainCamera;

    [Header("Movement settings")]
    public float speed = 5f;
    [SerializeField] private float rotationSpeed = 5.0f;

    [Header("Jump settings")]
    [SerializeField] private float jumpHeight = 2.0f;
    [SerializeField] private float timeToApex = 0.4f;

   
    //movement vars
    Vector2 direction; //movement direction //no grav applied
    Vector3 velocity;
    float gravity;
    float initialJumpVelocity;
    bool jumpPressed = false;


    void Awake()
    {
        input = new InputSystem_Actions(); 
        input.Player.SetCallbacks(this);
        input.Player.Enable();
        
    }

    void OnEnable()
    {
        input.Enable();
    }

    void OnDisable()
    {
        input.Disable();
    }

    void OnDestroy()
    {
     input.Dispose();   
    }
    public void OnAttack(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
       Vector2 look = context.ReadValue<Vector2>();
    
    }

    public void OnMove(InputAction.CallbackContext context)
    {
       direction = context.ReadValue<Vector2>();
        Debug.Log("Move input:" + direction);
    }

    public void OnNext(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnPrevious(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    void Start()
    {
        cc = GetComponent<CharacterController>();

        CalculateJumpVar();
        if (mainCamera == null)
        {
            findCam();
            return;
        }
    }
   
    void OnValidate()
    {
        CalculateJumpVar();
    }
    void Update()
    {

    }

    void FixedUpdate()
    {
        //apply movement
        Vector3 projectedMoveDir = ProjectedMoveDirection();
        UpdateCharacterVelocity(projectedMoveDir);
        cc.Move(velocity * Time.fixedDeltaTime);

        //apply rotation
        if (direction != Vector2.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(projectedMoveDir);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }


    void findCam()
    {
        Camera cam = Camera.main;
        if(cam != null)
        {
            mainCamera = cam;
        }
    }

    

    #region jump
    float CheckJump() => jumpPressed ? initialJumpVelocity : -cc.skinWidth;
    void CalculateJumpVar()
    {
        gravity = -(2 * jumpHeight) / Mathf.Pow(timeToApex, 2);
        initialJumpVelocity = Mathf.Abs(gravity) * timeToApex;
    }
    #endregion

    #region Movement
    private Vector3 ProjectedMoveDirection()
    {
        if(mainCamera == null)
        {
                Debug.Log("No camera found");
                return Vector3.zero;
        }
        Vector3 cameraFwd = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        cameraFwd.y = 0;
        cameraRight.y = 0;

        cameraFwd.Normalize();
        cameraRight.Normalize();

        return cameraFwd * direction.y + cameraRight * direction.x;
    }

    private void UpdateCharacterVelocity(Vector3 projectedMoveDir)
    {
        velocity.x = projectedMoveDir.x * speed;
        velocity.z = projectedMoveDir.z * speed;

        if (!cc.isGrounded) velocity.y += gravity * Time.fixedDeltaTime;
        else velocity.y = CheckJump();
    }
    #endregion
}

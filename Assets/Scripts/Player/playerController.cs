using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class playerController : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions input;      

    public float speed = 5f;
    Vector3 gravity = Physics.gravity;
    CharacterController cc;

    //movement vars
    Vector2 direction; //movement direction //no grav applied
    Vector3 velocity;

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
        throw new System.NotImplementedException();
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        jumpPressed = context.ReadValueAsButton();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        //throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            direction = context.ReadValue<Vector2>();
            return;
        }

        direction = Vector2.zero;
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
        throw new System.NotImplementedException();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {

        //Vector3 moveVel = new Vector3(hInput * speed, Physics.gravity.y, vInput * speed);

        //moveVel *= Time.deltaTime;

        //cc.Move(moveVel);
    }

    void FixedUpdate()
    {
        Vector3 moveVel = new Vector3(direction.x * speed, gravity.y, direction.y * speed);
        moveVel *= Time.fixedDeltaTime;

        cc.Move(moveVel);

    }
}

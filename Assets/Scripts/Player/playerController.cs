using UnityEngine;

public class playerController : MonoBehaviour
{

    public float speed = 5f;
    Vector3 gravity = Physics.gravity;
    CharacterController cc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cc = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis("Vertical");

        Vector3 moveVel = new Vector3(hInput * speed, Physics.gravity.y, vInput * speed);

        moveVel *= Time.deltaTime;

        //Vector3 desiredMovePos = transform.position + moveVel;

        cc.SimpleMove(moveVel);
    }
}

using UnityEngine;

public class TempPlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    Rigidbody rb;
    void Start()
    {
        rb= GetComponent<Rigidbody>();
    }


    void Update()
    {
        float hInput = Input.GetAxis("Horizontal");
        float vInput = Input.GetAxis ("Vertical");
        Debug.Log(hInput + vInput);

        Vector3 moveVel = new Vector3(hInput * speed, rb.linearVelocity.y, vInput * speed);

        rb.linearVelocity = moveVel;
    }
}

using UnityEngine;
using UnityEngine.InputSystem;

public class NewMonoBehaviourScript : MonoBehaviour
{
    private Rigidbody2D landerRigidbody2D;

    private void Awake()
    {
        landerRigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (Keyboard.current.upArrowKey.isPressed)
        {
            float force = 700f;
            landerRigidbody2D.AddForce(force * transform.up * Time.deltaTime);
            Debug.Log("Up");
        }

        if (Keyboard.current.leftArrowKey.isPressed)
        {
            float turnSpeed = +100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            Debug.Log("Left");
        }

        if (Keyboard.current.rightArrowKey.isPressed)
        {
            float turnSpeed = -100f;
            landerRigidbody2D.AddTorque(turnSpeed * Time.deltaTime);
            Debug.Log("Right");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision2D)
    {
        float SoftLandingVelocityMagnitude = 4f;
        if (collision2D.relativeVelocity.magnitude > SoftLandingVelocityMagnitude)
        {
            Debug.Log("Landed too hard!");
            return;
        }
        Debug.Log("Successful landing");
    }
}


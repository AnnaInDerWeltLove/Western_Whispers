using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");

        // Vorwärts und rückwärts bewegen
        transform.Translate(
            Vector3.forward * moveInput * moveSpeed * Time.deltaTime
        );

        // Nach links und rechts drehen
        transform.Rotate(
            Vector3.up * turnInput * rotationSpeed * Time.deltaTime
        );
    }
}
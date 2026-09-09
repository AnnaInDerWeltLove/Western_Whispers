using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 120f;

    void Update()
    {
        float moveInput = Input.GetAxis("Vertical");
        float turnInput = Input.GetAxis("Horizontal");
        
        transform.Translate(
            Vector3.forward * moveInput * moveSpeed * Time.deltaTime
        );
        
        transform.Rotate(
            Vector3.up * turnInput * rotationSpeed * Time.deltaTime
        );
    }
}
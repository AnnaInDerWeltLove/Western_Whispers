using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    void Update()
    {
        float moveVerticalInput = Input.GetAxis("Vertical");
        float moveHorizontalInput = Input.GetAxis("Horizontal");
        
        transform.Translate(
            Vector3.forward * moveVerticalInput * moveSpeed * Time.deltaTime
        );
        
        transform.Translate(
            Vector3.right * moveHorizontalInput * moveSpeed * Time.deltaTime
        );
    }
}
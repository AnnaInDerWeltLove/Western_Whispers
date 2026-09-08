using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float rotationSpeed = 100f;
    public float jumpForce = 5f;

    [Header("Ground Check")]
    public float groundCheckDistance = 2f;
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true; // verhindert Umkippen durch Kollisionen
    }

    void Update()
    {
        float rotationInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);

        // Ground-Check per Raycast nach unten
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance, groundLayer);

        // Sprung
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
    void FixedUpdate()
    {
        float moveInput = Input.GetAxis("Vertical");
        Vector3 move = transform.forward * moveInput * moveSpeed;
        rb.MovePosition(rb.position + move * Time.fixedDeltaTime);
    }
}

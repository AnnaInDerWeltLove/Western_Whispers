using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    [Header("Bewegung")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    
    [Header("Sprung")]
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float groundCheckDistance = 20f;
    [SerializeField] private LayerMask groundLayer;
    
    
    private Rigidbody rb;
    private bool isGrounded;
    private bool jumpRequested;

    //Claude Movement Codebeispiel mit Rigidbody für Physik, Bewegung relativ zu Kameraposition, Sprungfunktion
    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Falls keine Kamera zugewiesen wurde, automatisch die Hauptkamera verwenden
        if (cameraTransform == null && Camera.main != null)
        {
            cameraTransform = Camera.main.transform;
        }
    }

    void Update()
    {
        // Sprung-Eingabe in Update abfragen, damit kein Tastendruck verloren geht
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequested = true;
        }
    }

    void FixedUpdate()
    {
        CheckGrounded();
        Move();

        if (jumpRequested)
        {
            Jump();
            jumpRequested = false;
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Kamera-Vorwärts- und Rechts-Vektor holen, Y-Komponente ignorieren
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Bewegungsrichtung relativ zur Kamera berechnen
        Vector3 movement = (camForward * vertical + camRight * horizontal);

        Vector3 newPosition = rb.position + movement * speed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 0.1f, groundLayer);
    }
}

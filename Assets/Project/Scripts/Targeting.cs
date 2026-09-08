using UnityEngine;

[RequireComponent(typeof(Camera))]
public class Targeting : MonoBehaviour
{
    [Header("Referenzen")]
    [SerializeField] private Transform playerBody;      // Wird zur Blickrichtung gedreht
    [SerializeField] private Transform cameraPivot;      // Punkt, um den die Kamera rotiert
    [SerializeField] private Camera aimCamera;

    [Header("Maus-Einstellungen")]
    [SerializeField] private float mouseSensitivity = 2f;
    [SerializeField] private bool invertY = false;

    [Header("Zielmodus")]
    [SerializeField] private KeyCode aimKey = KeyCode.Mouse1; // Rechte Maustaste
    [SerializeField] private float aimMoveSpeedMultiplier = 0.5f; // Für Bewegung während des Zielens

    [Header("Zoom")]
    [SerializeField] private float normalFOV = 60f;
    [SerializeField] private float aimFOV = 35f;
    [SerializeField] private float fovTransitionSpeed = 10f;

    [Header("Kamera-Grenzen")]
    [SerializeField] private float minPitch = -60f;
    [SerializeField] private float maxPitch = 80f;

    [Header("Ziel-Erkennung")]
    [SerializeField] private float maxAimDistance = 100f;
    [SerializeField] private LayerMask aimLayerMask = ~0;

    private float yaw;
    private float pitch;
    private bool isAiming;

    public bool IsAiming => isAiming;
    public float CurrentSpeedMultiplier => isAiming ? aimMoveSpeedMultiplier : 1f;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (aimCamera == null)
            aimCamera = GetComponent<Camera>();

        yaw = playerBody != null ? playerBody.eulerAngles.y : 0f;
        aimCamera.fieldOfView = normalFOV;
    }

    // Update is called once per frame
    void Update()
    {
        HandleAimInput();
        HandleMouseLook();
        HandleZoom();
    }
    private void HandleAimInput()
    {
        isAiming = Input.GetKeyDown(aimKey);
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * (invertY ? 1f : -1f);

        yaw += mouseX;
        pitch += mouseY;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Spielerkörper horizontal drehen
        if (playerBody != null)
            playerBody.rotation = Quaternion.Euler(0f, yaw, 0f);

        // Kamera vertikal + horizontal (falls kein separater Body) drehen
        if (cameraPivot != null)
            cameraPivot.localRotation = Quaternion.Euler(pitch, 0f, 0f);
        else
            transform.rotation = Quaternion.Euler(pitch, yaw, 0f);
    }

    private void HandleZoom()
    {
        float targetFOV = isAiming ? aimFOV : normalFOV;
        aimCamera.fieldOfView = Mathf.Lerp(
            aimCamera.fieldOfView,
            targetFOV,
            Time.deltaTime * fovTransitionSpeed
        );
    }

    /// <summary>
    /// Gibt den Punkt zurück, auf den gerade gezielt wird (Bildschirmmitte -> Raycast).
    /// Nützlich für Waffen, Fadenkreuz-Ausrichtung, Zielmarkierung etc.
    /// </summary>
    public bool TryGetAimPoint(out RaycastHit hit)
    {
        Ray ray = aimCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        return Physics.Raycast(ray, out hit, maxAimDistance, aimLayerMask);
    }

    public Vector3 GetAimDirection()
    {
        return aimCamera.transform.forward;
    }
}

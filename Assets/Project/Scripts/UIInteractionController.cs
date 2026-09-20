using UnityEngine;
// KI-Generiert
public class UIInteractionController : MonoBehaviour
{
    [SerializeField] private CameraControl mainCameraControl;
    [SerializeField] private CameraControl fpCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;

    private bool uiModeActive;

    private void Start()
    {
        CloseUI();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (uiModeActive)
            {
                CloseUI();
            }
            else
            {
                OpenUI();
            }
        }
    }

    private void OpenUI()
    {
        uiModeActive = true;

        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        mainCameraControl.enabled = false;
        fpCameraControl.enabled = false;
        playerCameraSwitch.enabled = false;
    }

    private void CloseUI()
    {
        uiModeActive = false;

        Time.timeScale = 1f;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        mainCameraControl.enabled = true;
        fpCameraControl.enabled = true;
        playerCameraSwitch.enabled = true;
    }
}


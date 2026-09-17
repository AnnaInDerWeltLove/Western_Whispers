using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera moveCamera;
    [SerializeField] private Camera aimCamera;

    [SerializeField] private KeyCode switchKey = KeyCode.Mouse1;

    private void Start()
    {
        // Sicherstellen, dass zu Beginn nur Kamera 1 aktiv ist
        moveCamera.gameObject.SetActive(true);
        aimCamera.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(switchKey))
        {
            SwitchCamera();
        }
    }

    private void SwitchCamera()
    {
        bool cam1Active = moveCamera.gameObject.activeSelf;
        moveCamera.gameObject.SetActive(!cam1Active);
        aimCamera.gameObject.SetActive(cam1Active);
    }
    
}

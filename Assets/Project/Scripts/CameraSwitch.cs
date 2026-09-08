using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;

    [SerializeField] private KeyCode switchKey = KeyCode.C;

    private void Start()
    {
        // Sicherstellen, dass zu Beginn nur Kamera 1 aktiv ist
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
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
        bool cam1Active = camera1.gameObject.activeSelf;
        camera1.gameObject.SetActive(!cam1Active);
        camera2.gameObject.SetActive(cam1Active);
    }
    
}

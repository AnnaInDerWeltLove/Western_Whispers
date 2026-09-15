using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    
    private void Start()
    {
        // Sicherstellen, dass zu Beginn nur Kamera 1 aktiv ist
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }

    public void SwitchToFightCamera()
    {
       camera1.gameObject.SetActive(false);
       camera2.gameObject.SetActive(true);
       
       Debug.Log("Main Camera aktiv: " + camera1.gameObject.activeSelf);
       Debug.Log("Fight Camera aktiv: " + camera2.gameObject.activeSelf);
       Debug.Log("Fight Camera Name: " + camera2.name);
    }
    

    public void SwitchToMainCamera()
    {
        camera1.gameObject.SetActive(true);
        camera2.gameObject.SetActive(false);
    }
    
}

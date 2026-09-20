using UnityEngine;
// KI-Generiert
public class PlayFightSwitch : MonoBehaviour
{
    [SerializeField] private Camera thirdPersonCamera;
    [SerializeField] private Camera firstPersonCamera;
    [SerializeField] private Camera fightCamera;

    private bool wasFirstPersonActive;

    private void Start()
    {
        fightCamera.gameObject.SetActive(false);

        // Falls aus Versehen keine Spielkamera aktiv ist
        if (!thirdPersonCamera.gameObject.activeSelf &&
            !firstPersonCamera.gameObject.activeSelf)
        {
            thirdPersonCamera.gameObject.SetActive(true);
        }
    }

    public void SwitchToFightCamera()
    {
        // Merken, welche Kamera vorher aktiv war
        wasFirstPersonActive = firstPersonCamera.gameObject.activeSelf;

        thirdPersonCamera.gameObject.SetActive(false);
        firstPersonCamera.gameObject.SetActive(false);
        fightCamera.gameObject.SetActive(true);
    }

    public void SwitchToPlayCamera()
    {
        fightCamera.gameObject.SetActive(false);

        if (wasFirstPersonActive)
        {
            firstPersonCamera.gameObject.SetActive(true);
            thirdPersonCamera.gameObject.SetActive(false);
        }
        else
        {
            thirdPersonCamera.gameObject.SetActive(true);
            firstPersonCamera.gameObject.SetActive(false);
        }
    }
}
using UnityEngine;

public class FightManager : MonoBehaviour
{
   [SerializeField] private GameObject attackPopup;
   [SerializeField] private GhostTimeTimer ghostTimeTimer;
   [SerializeField] private CameraManager cameraManager;
   [SerializeField] private GameObject crosshair;

   private void Start()
   {
      attackPopup.SetActive(false);
      crosshair.SetActive(false);
   }
   public void StartFight(int clueID)
   {
      Debug.Log("Fight gestartet!");
      ghostTimeTimer.FreezeGhostTime();
      attackPopup.SetActive(true);
   }

   public void StartAttack()
   {
      attackPopup.SetActive(false);
      crosshair.SetActive(true);
      cameraManager.SwitchToFightCamera();
      
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = false;
   }
}

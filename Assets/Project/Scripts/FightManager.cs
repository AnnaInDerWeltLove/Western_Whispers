using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
   [SerializeField] private GameObject attackPopup;
   [SerializeField] private GhostTimeTimer ghostTimeTimer;
   [SerializeField] private CameraManager cameraManager;
   [SerializeField] private GameObject crosshair;
   [SerializeField] private LightRevolver lightRevolver;
   [SerializeField] private GameObject victoryPopup;
   [SerializeField] private float maxFightTime = 40f;
   [SerializeField] private GameObject losePanel;
   
   private float currentFightTime;
   private bool fightTimerRunning = false;
   private void Start()
   {
      attackPopup.SetActive(false);
      crosshair.SetActive(false);
      victoryPopup.SetActive(false);
      losePanel.SetActive(false);
   }
   private void Update()
   {
      if (fightTimerRunning)
      {
         currentFightTime -= Time.deltaTime;
         Debug.Log("Kampfzeit: " + currentFightTime);
         if (currentFightTime <= 0)
         {
            currentFightTime = 0;
            fightTimerRunning = false;
            LoseFight();
         }
      }
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
      lightRevolver.EnableShooting();
      currentFightTime = maxFightTime;
      fightTimerRunning = true;
      
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = false;
   }

   public void WinFight()
   {
      victoryPopup.SetActive(true);
      lightRevolver.DisableShooting();
      crosshair.SetActive(false);
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      fightTimerRunning = false;
   }

   public void ExitFight()
   {
      victoryPopup.SetActive(false);
      cameraManager.SwitchToMainCamera();
      ghostTimeTimer.UnfreezeGhostTime();
      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;
   }
   private void LoseFight()
   {
      Debug.Log("Verloren!");
      losePanel.SetActive(true);
      lightRevolver.DisableShooting();
      crosshair.SetActive(false);
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
      Invoke(nameof(RestartGame), 3f);
   }
   private void RestartGame()
   {
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
   }
   
}

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
   [SerializeField] private GameObject ghost1;
   [SerializeField] private GameObject ghost2;
   [SerializeField] private GameObject ghost3;


   private int currentClueID;
   private float currentFightTime;
   private bool fightTimerRunning = false;
   private void Start()
   {
      attackPopup.SetActive(false);
      crosshair.SetActive(false);
      victoryPopup.SetActive(false);
      losePanel.SetActive(false);
      ghost1.SetActive(false);
      ghost2.SetActive(false);
      ghost3.SetActive(false);
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
      currentClueID = clueID;
      Debug.Log("Fight gestartet! Clue ID: " + clueID);
      ghostTimeTimer.FreezeGhostTime();
      attackPopup.SetActive(true);
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
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
      if (currentClueID == 1)
      {
         ghost1.SetActive(true);
         ghost2.SetActive(false);
         ghost3.SetActive(false);
      }
      if (currentClueID == 2)
      {
         ghost2.SetActive(true);
         ghost1.SetActive(false);
         ghost3.SetActive(false);
      }
      if (currentClueID == 3)
      {
         ghost3.SetActive(true);
         ghost1.SetActive(false);
         ghost2.SetActive(false);
      }
      
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

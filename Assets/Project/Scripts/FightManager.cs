using UnityEngine;
using UnityEngine.SceneManagement;

public class FightManager : MonoBehaviour
{
   [Header("UI")]
   [SerializeField] private GameObject attackPopup;
   [SerializeField] private GameObject crosshair;
   [SerializeField] private GameObject victoryPopup;
   [SerializeField] private GameObject losePanel;
   
   [Header("References")]
   [SerializeField] private GhostTimeTimer ghostTimeTimer;
   [SerializeField] private PlayFightSwitch playFightSwitch;
   [SerializeField] private LightRevolver lightRevolver;
   
   
   [Header("Fight Settings")]
   [SerializeField] private float maxFightTime = 40f;
   
   [Header("Ghosts")]
   [SerializeField] private GameObject ghost1;
   [SerializeField] private GameObject ghost2;
   [SerializeField] private GameObject ghost3;
   
   [Header("Bewegungskontrolle")]
   [SerializeField] private Movement movement;
   [SerializeField] private CameraControl thirdPersonCameraControl;
   [SerializeField] private CameraControl firstPersonCameraControl;
   
   [Header("Hint Cutscenes")]
   [SerializeField] private MechanismInteraction mechanism1;
   [SerializeField] private MechanismInteraction mechanism2;
   [SerializeField] private MechanismInteraction mechanism3;
   
   private int currentClueID;
   private float currentFightTime;
   private bool fightTimerRunning;
   
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
      ghostTimeTimer.FreezeGhostTime();
      attackPopup.SetActive(true);
      movement.enabled = false;
      thirdPersonCameraControl.enabled = false;
      firstPersonCameraControl.enabled = false;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = true;
   }

   public void StartAttack()
   {
      attackPopup.SetActive(false);
      crosshair.SetActive(true);
      movement.enabled = true;
      thirdPersonCameraControl.enabled = true;
      firstPersonCameraControl.enabled = true;
      playFightSwitch.SwitchToFightCamera();
      lightRevolver.EnableShooting();
      currentFightTime = maxFightTime;
      fightTimerRunning = true;
      Cursor.lockState = CursorLockMode.None;
      Cursor.visible = false;
      switch (currentClueID)
      {
         case 1:
            ghost1.SetActive(true);
            break;
      
         case 2:
            ghost2.SetActive(true);
            break;
      
         case 3:
            ghost3.SetActive(true);
            break;
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
      playFightSwitch.SwitchToPlayCamera();

      Cursor.lockState = CursorLockMode.Locked;
      Cursor.visible = false;

      switch (currentClueID)
      {
         case 1:
            mechanism1.PlayHintCutscene();
            break;

         case 2:
            mechanism2.PlayHintCutscene();
            break;

         case 3:
            mechanism3.PlayHintCutscene();
            break;
      }
   }
   private void LoseFight()
   {
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

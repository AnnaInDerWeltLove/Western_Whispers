using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MechanismInteraction : MonoBehaviour
{   
    [Header("Cutscenes")]
    [SerializeField] private VideoPlayer hintCutScenePlayer;
    [SerializeField] private GameObject hintCutScenePanel;
    [SerializeField] private VideoClip hintVideo;
    [SerializeField] private VideoPlayer storyCutScenePlayer;
    [SerializeField] private GameObject storyCutScenePanel;
    [SerializeField] private VideoClip storyVideo;
    [SerializeField] private CreditsManager creditsManager;
    
    
    [Header("Reward Part")]
    [SerializeField] private GameObject rewardItemPanel; 
    [SerializeField] private UnityEngine.UI.Image rewardItemImage;
    [SerializeField] private Sprite rewardItemSprite;
    
    [Header("Inventory")]
    [SerializeField] private Inventory inventory;
    
    [Header("Puzzle")]
    [SerializeField] private PuzzleManager puzzleManager; 
    [SerializeField] private int puzzleID = 1;
    [SerializeField] private Movement movement;
    [SerializeField] private GhostTimeTimer ghostTimeTimer;
    
    [Header("Steuerung während Sequenz")]
    [SerializeField] private GameObject playerInterface;
    [SerializeField] private CameraControl thirdPersonCameraControl;
    [SerializeField] private CameraControl firstPersonCameraControl;
    [SerializeField] private PlayerCameraSwitch playerCameraSwitch;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private UIInteractionController uiInteractionController;
    
    
    
    
    private bool playerInside;
    private bool mechanismActivated = false;
    private bool sequenceRunning = false;

    private void Start()
    {
        hintCutScenePanel.SetActive(false);
        storyCutScenePanel.SetActive(false);
        hintCutScenePlayer.loopPointReached += OnHintCutsceneFinished;
        storyCutScenePlayer.loopPointReached += OnStoryCutsceneFinished;
        rewardItemPanel.SetActive(false);
       
    }

    private void Update()
    {
        if (playerInside && !mechanismActivated && !sequenceRunning && Input.GetKeyDown(KeyCode.E))
        {
           StartMechanismSequence();

        }
    }
    public void PlayHintCutscene()
    {
        DisablePlayerControls();
        playerInterface.SetActive(false);

        hintCutScenePanel.SetActive(true);
        hintCutScenePlayer.clip = hintVideo;
        hintCutScenePlayer.Play();
    }
    private void StartMechanismSequence()
    {
        movement.enabled = false;
        mechanismActivated = true;
        sequenceRunning = true;
        storyCutScenePanel.SetActive(true);
        storyCutScenePlayer.clip = storyVideo;
        storyCutScenePlayer.Play();
        DisablePlayerControls();
    }
    private void OnHintCutsceneFinished(VideoPlayer vp)
    {
        hintCutScenePanel.SetActive(false);
        playerInterface.SetActive(true);
        EnablePlayerControls();
        
        ghostTimeTimer.UnfreezeGhostTime();
      
    }
    private void ShowItemInInventory()
    {
        inventory.AddItem(rewardItemSprite);
        inventory.OpenInventory();

        rewardItemPanel.SetActive(false);

        Invoke(nameof(FinishMechanismSequence), 2f);
    }
    private void StartStoryCutscene()
    {
        inventory.CloseInventory();
        storyCutScenePanel.SetActive(true);
        storyCutScenePlayer.clip = storyVideo;
        storyCutScenePlayer.Play();
    }

    private void OnStoryCutsceneFinished(VideoPlayer vp)
    {
        if (!sequenceRunning)
        {
            return;
        }

        storyCutScenePanel.SetActive(false);

        rewardItemImage.sprite = rewardItemSprite;
        rewardItemPanel.SetActive(true);

        Invoke(nameof(ShowItemInInventory), 2f);
    }
    
    private void FinishMechanismSequence()
    {
        inventory.CloseInventory();
        inventory.AddItem(rewardItemSprite);
        rewardItemPanel.SetActive(false);
        EnablePlayerControls();
        

        movement.enabled = true;
        sequenceRunning = false;

        if (puzzleID == 3)
        {
            int currentProgress = PlayerPrefs.GetInt("UnlockedLevel", 0);

            if (currentProgress < 1)
            {
                PlayerPrefs.SetInt("UnlockedLevel", 1);
                PlayerPrefs.Save();
            }

            creditsManager.ShowCredits();
        }
        else
        {
            puzzleManager.CompletedPuzzle(puzzleID);
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
    
    private void DisablePlayerControls()
    {
        movement.enabled = false;
        thirdPersonCameraControl.enabled = false;
        firstPersonCameraControl.enabled = false;
        playerCameraSwitch.enabled = false;
        worldManager.enabled = false;
        uiInteractionController.enabled = false;
        playerInterface.SetActive(false);
    }

    private void EnablePlayerControls()
    {
        movement.enabled = true;
        thirdPersonCameraControl.enabled = true;
        firstPersonCameraControl.enabled = true;
        playerCameraSwitch.enabled = true;
        worldManager.enabled = true;
        uiInteractionController.enabled = true;
        playerInterface.SetActive(true);
    }
}



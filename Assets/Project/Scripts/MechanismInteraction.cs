using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MechanismInteraction : MonoBehaviour
{   [Header("Cutscenes")]
    [SerializeField] private VideoPlayer cutScenePlayer;
    [SerializeField] private GameObject cutScenePanel;
    [SerializeField] private VideoPlayer storyCutScenePlayer;
    
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
    
    
   
    private bool playerInside;
    private bool mechanismActivated = false;
    private bool sequenceRunning = false;

    private void Start()
    {
        cutScenePanel.SetActive(false);
        cutScenePlayer.loopPointReached += OnStoryCutsceneFinished;
        storyCutScenePlayer.loopPointReached += OnHintCutsceneFinished;
        rewardItemPanel.SetActive(false);
       
    }

    private void Update()
    {
        if (playerInside && !mechanismActivated && !sequenceRunning && Input.GetKeyDown(KeyCode.E))
        {
           StartMechanismSequence();

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hat das Mechanismus betreten");
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player hat das Mechanismus verlassen");
            playerInside = false;
        }
    }

    private void StartMechanismSequence()
    {
        movement.enabled = false;
        mechanismActivated = true;
        sequenceRunning = true;
        Debug.Log("Mechanismus ausgelöst!");
        cutScenePanel.SetActive(true);
        cutScenePlayer.Play(); 
        
    }

    private void OnStoryCutsceneFinished(VideoPlayer vp)
    {
        if (!sequenceRunning)
        {
            return;
        }
        Debug.Log("Cutscene abgeschlossen!");
        cutScenePanel.SetActive(false);
        rewardItemImage.sprite = rewardItemSprite;
        rewardItemPanel.SetActive(true);
        Invoke(nameof(ShowItemInInventory), 2f);
    }
    private void ShowItemInInventory()
    {
        inventory.AddItem(rewardItemSprite);
        inventory.OpenInventory();
        rewardItemPanel.SetActive(false);
        Debug.Log("Item in Inventar geschoben!");
        Invoke(nameof(StartHintCutscene), 2f);
    }
    private void StartHintCutscene()
    {
        inventory.CloseInventory();
        cutScenePanel.SetActive(true);
        storyCutScenePlayer.Play();
        Debug.Log("Mechanismus beendet!");
    }

    private void OnHintCutsceneFinished(VideoPlayer vp)
    {
        if (!sequenceRunning)
        {
            return;
        }
        cutScenePanel.SetActive(false);
        movement.enabled = true;
        sequenceRunning = false;
        puzzleManager.CompletedPuzzle(puzzleID);
        Debug.Log("Story Cutscene abgeschlossen!");
    }
}



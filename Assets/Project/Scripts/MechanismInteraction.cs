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
    private void StartMechanismSequence()
    {
        movement.enabled = false;
        mechanismActivated = true;
        sequenceRunning = true;
        hintCutScenePanel.SetActive(true);
        hintCutScenePlayer.clip = hintVideo;
        hintCutScenePlayer.Play(); 
        
    }
    private void OnHintCutsceneFinished(VideoPlayer vp)
    {
        if (!sequenceRunning)
        {
            return;
        }
        hintCutScenePanel.SetActive(false);
        rewardItemImage.sprite = rewardItemSprite;
        rewardItemPanel.SetActive(true);
        Invoke(nameof(ShowItemInInventory), 2f);
      
    }
    private void ShowItemInInventory()
    {
        inventory.AddItem(rewardItemSprite);
        inventory.OpenInventory();
        rewardItemPanel.SetActive(false);
        Invoke(nameof(StartStoryCutscene), 2f);
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
        movement.enabled = true;
        sequenceRunning = false;
        puzzleManager.CompletedPuzzle(puzzleID);
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
}



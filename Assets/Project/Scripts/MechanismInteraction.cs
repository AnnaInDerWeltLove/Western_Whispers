using UnityEngine;
using UnityEngine.Video;

public class MechanismInteraction : MonoBehaviour
{
    [SerializeField] private VideoPlayer cutScenePlayer;
    [SerializeField] private GameObject cutScenePanel;
    [SerializeField] private Movement movement;
    [SerializeField] private GameObject itemRewardPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject inventoryItemImage;
    [SerializeField] private Inventar inventar;
    private bool playerInside;
    private bool mechanismActivated = false;
    private bool sequenceRunning = false;

    void Start()
    {
        cutScenePanel.SetActive(false);
        cutScenePlayer.loopPointReached += OnCutsceneFinished;
        itemRewardPanel.SetActive(false);
        inventoryPanel.SetActive(false);
        inventoryItemImage.SetActive(false);
    }

    void Update()
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

    private void OnCutsceneFinished(VideoPlayer vp)
    {
        Debug.Log("Cutscene abgeschlossen!");
        cutScenePanel.SetActive(false);
        itemRewardPanel.SetActive(true);
        Invoke(nameof(ShowItemInInventory), 5f);
    }
    private void ShowItemInInventory()
    {
        inventoryPanel.SetActive(true);
        inventoryItemImage.SetActive(true);
        Debug.Log("Item in Inventar geschoben!");
        movement.enabled = true;
        sequenceRunning = false;
        Invoke(nameof(FinishMechanismSequence), 3f);
    }
    private void FinishMechanismSequence()
    {
        inventoryPanel.SetActive(false);
        movement.enabled = true;
        sequenceRunning = false;
        itemRewardPanel.SetActive(false);
        Debug.Log("Mechanismus beendet!");
    }
}



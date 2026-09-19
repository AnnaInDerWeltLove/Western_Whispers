using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
  
    [SerializeField] private int clueID;
    private PuzzleManager puzzleManager;
    
    private bool encounterStarted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !encounterStarted)
        {
            encounterStarted = true;
            puzzleManager.StartGhostEncounter(clueID);
        }
    }

    private void Awake()
    {
        puzzleManager = FindObjectOfType<PuzzleManager>();
    }
}
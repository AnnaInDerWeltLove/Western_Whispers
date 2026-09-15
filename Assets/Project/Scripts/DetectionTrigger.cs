using UnityEngine;

public class DetectionTrigger : MonoBehaviour
{
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int clueID;
    
    private bool encounterStarted = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !encounterStarted)
        {
            Debug.Log("Player hat den Trigger betreten.");
            encounterStarted = true;
            puzzleManager.StartGhostEncounter(clueID);
        }
    }
}
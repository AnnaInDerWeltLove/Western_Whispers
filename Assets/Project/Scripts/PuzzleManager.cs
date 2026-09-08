using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject ghostEncounterUI;
    public void StartGhostEncounter(int clueID)
    {
        Debug.Log("Geisterbegegnung gestartet!");
       // ghostEncounterUI.SetActive(true);
    }
}
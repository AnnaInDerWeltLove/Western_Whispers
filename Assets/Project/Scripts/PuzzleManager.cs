using System;
using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject puzzleObject1;
    [SerializeField] private GameObject puzzleObject2;
    [SerializeField] private GameObject puzzleObject3;
    [SerializeField] private GameObject hintObject1;
    [SerializeField] private GameObject hintObject2;
    [SerializeField] private GameObject hintObject3;
    [SerializeField] SphereCollider hint1RigidBody;
    [SerializeField] SphereCollider hint2RigidBody;
    [SerializeField] SphereCollider hint3RigidBody;
    private bool hint1Unlocked;
    private bool hint1IsFound = false;
    private bool isSpiritWorld = false;
    private bool puzzle1Unlocked = false;
    private void Start()
    {
        hint1Unlocked = true;
    }
    private void Update()
    {
            Hint();
    }

    private void Hint()
    {
        if (isSpiritWorld && hint1Unlocked && !hint1IsFound)
        {
            hintObject1.SetActive(true);
        }
        else
        {
            hintObject1.SetActive(false);
        }
        puzzleObject1.SetActive(puzzle1Unlocked);
    }

    public void StartGhostEncounter(int clueID)
    {
        if (clueID == 1)
        {
            hint1IsFound = true;
            puzzle1Unlocked = true;
        }
        
    }
}












/*
 [SerializeField] private GameObject ghostEncounterUI;
     public void StartGhostEncounter(int clueID)
     {
         Debug.Log("Geisterbegegnung gestartet!");
        // ghostEncounterUI.SetActive(true);
     }
*/
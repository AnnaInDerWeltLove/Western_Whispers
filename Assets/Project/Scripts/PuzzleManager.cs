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
    [SerializeField] private FightManager fightManager;
    private bool hint1Unlocked;
    private bool hint2Unlocked;
    private bool hint3Unlocked;
    private bool hint1IsFound = false;
    private bool hint2IsFound = false;
    private bool hint3IsFound = false;
    private bool isSpiritWorld = false;
    private bool puzzle1Unlocked = false;
    private bool puzzle2Unlocked = false;
    private bool puzzle3Unlocked = false;
    private void Start()
    {
        hint1Unlocked = true;
        hint2Unlocked = false;
        hint3Unlocked = false;
    }
    private void Update()
    {
            Hint();
    }

    private void Hint()
    {
        if (isSpiritWorld && hint1Unlocked)
        {
            hintObject1.SetActive(true);
        }
        else
        {
            hintObject1.SetActive(false);
        }
        if (isSpiritWorld && hint2Unlocked)
        {
            hintObject2.SetActive(true);
        }
        else
        {
            hintObject2.SetActive(false);
        }
        if (isSpiritWorld && hint3Unlocked)
        {
            hintObject3.SetActive(true);
        }
        else
        {
            hintObject3.SetActive(false);
        }
        puzzleObject1.SetActive(puzzle1Unlocked && !isSpiritWorld);
        puzzleObject2.SetActive(puzzle2Unlocked && !isSpiritWorld);
        puzzleObject3.SetActive(puzzle3Unlocked && !isSpiritWorld);
    }

    public void CompletedPuzzle(int puzzleID)
    {
        if (puzzleID == 1)
        {
            hint2Unlocked = true;
            Debug.Log("Hinweis 2 freigeschaltet");
        }
        if (puzzleID == 2)
        {
            hint3Unlocked = true;
            Debug.Log("Hinweis 3 freigeschaltet");
        }
    }
    
    private void OnEnable()
    {
        WorldManager.OnWorldChanged += HandleWorldChanged;
    }

    private void OnDisable()
    {
        WorldManager.OnWorldChanged -= HandleWorldChanged;
    }

    private void HandleWorldChanged(bool spiritWorld)
    {
        isSpiritWorld = spiritWorld;
        Debug.Log("PuzzleManager bekommt Weltwechsel: " + spiritWorld);
        Debug.Log("SpiritClue aktiv: " + hintObject1.activeSelf);
    }

    public void StartGhostEncounter(int clueID)
    {
        if (clueID == 1)
        {
            hint1IsFound = true;
            fightManager.StartFight(1);

        }
        if (clueID == 2)
        {
            hint2IsFound = true;
            fightManager.StartFight(2);

        }
        if (clueID == 3)
        {
            hint3IsFound = true;
            fightManager.StartFight(3);

        }
    }

    public void VictoriousGhostEncounter(int clueID)
    {
        if (clueID == 1)
        {
            puzzle1Unlocked = true;
        }
        if (clueID == 2)
        {
            puzzle2Unlocked = true;
        }
        if (clueID == 3)
        {
            puzzle3Unlocked = true;
        }
    }
    
}


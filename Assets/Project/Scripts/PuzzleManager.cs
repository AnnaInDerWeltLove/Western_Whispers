using UnityEngine;

public class PuzzleManager : MonoBehaviour
{
    [Header("Puzzle Objects")]
    [SerializeField] private GameObject puzzleObject1;
    [SerializeField] private GameObject puzzleObject2;
    [SerializeField] private GameObject puzzleObject3;
    
    [Header("Hint Objects")]
    [SerializeField] private GameObject hintObject1;
    [SerializeField] private GameObject hintObject2;
    [SerializeField] private GameObject hintObject3;
    
    [Header("Detection Triggers")]
    [SerializeField] private SphereCollider hint1DetectionTrigger;
    [SerializeField] private SphereCollider hint2DetectionTrigger;
    [SerializeField] private SphereCollider hint3DetectionTrigger;
    
    [Header("Fight Manager")]
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
    }
    private void Hint()
    {
        hintObject1.SetActive(isSpiritWorld && hint1Unlocked);
        hintObject2.SetActive(isSpiritWorld && hint2Unlocked);
        hintObject3.SetActive(isSpiritWorld && hint3Unlocked);
       
        puzzleObject1.SetActive(puzzle1Unlocked && !isSpiritWorld);
        puzzleObject2.SetActive(puzzle2Unlocked && !isSpiritWorld);
        puzzleObject3.SetActive(puzzle3Unlocked && !isSpiritWorld);
    }
    public void StartGhostEncounter(int clueID)
    {
        switch (clueID)
        {
            case 1:
                hint1IsFound = true;
                fightManager.StartFight(1);
                break;
            
            case 2: 
                hint2IsFound = true;
                fightManager.StartFight(2);
                break;
            
            case 3:
                hint3IsFound = true;
                fightManager.StartFight(3);
                break;
        }
    }
    public void VictoriousGhostEncounter(int clueID)
    {
        switch (clueID)
        {
            case 1:
                puzzle1Unlocked = true;
                break;
                 
            case 2:
                puzzle2Unlocked = true;
                break;
                
            case 3:
                puzzle3Unlocked = true;
                break;
        }
    }
    public void CompletedPuzzle(int puzzleID)
    {
        switch (puzzleID)
        {
            case 1:
                hint2Unlocked = true;
                break;

            case 2:
                hint3Unlocked = true;
                break;
        }
    }
}


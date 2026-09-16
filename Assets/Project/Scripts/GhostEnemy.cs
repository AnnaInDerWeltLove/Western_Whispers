
using System;
using UnityEngine;
using Random = System.Random;

public class GhostEnemy : MonoBehaviour
{
    [SerializeField] private Collider ghostCollider;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float maxXDistance = 4f;
    [SerializeField] private float maxYDistance = 4f;
    [SerializeField] private PuzzleManager puzzleManager;
    [SerializeField] private int clueID = 1;
    [SerializeField] private FightManager fightManager;
    
    private Vector3 centerPosition;
    private Vector3 targetPosition;
    
    private void Start()
    {
        centerPosition = transform.position;
        SetNewTarget();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            SetNewTarget();
        }
    }

    private void SetNewTarget()
    {
        float randomX = UnityEngine.Random.Range(-maxXDistance, maxXDistance);
        float randomY = UnityEngine.Random.Range(-maxYDistance, maxYDistance);
        targetPosition = new Vector3(centerPosition.x + randomX, centerPosition.y + randomY, centerPosition.z);
    }

    public void Hit()
    {
        puzzleManager.VictoriousGhostEncounter(clueID);
        fightManager.WinFight();
        Debug.Log("Geist besiegt");
    }
}

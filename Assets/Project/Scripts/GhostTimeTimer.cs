using UnityEngine;

public class GhostTimeTimer : MonoBehaviour
{
    [SerializeField] private float maxGhostTime = 60f;
    [SerializeField] private WorldManager worldManager;
    private float currentGhostTime;
    private bool isInGhostWorld = false;
    void Start()
    {
        currentGhostTime = maxGhostTime;
    }

    private void Update()
    {
        if (isInGhostWorld)
        {
            currentGhostTime -= Time.deltaTime;
            Debug.Log("Geisterwelt: " + currentGhostTime);

            if (currentGhostTime <= 0)
            {
                worldManager.SetNormalWorld();
                currentGhostTime = 0;
                isInGhostWorld = false;
            }
        }
        else
        {
            currentGhostTime += Time.deltaTime;
            Debug.Log("Normale Welt: " + currentGhostTime);
            if (currentGhostTime >= maxGhostTime)
            {
                currentGhostTime = maxGhostTime;
            }
        }
    
    
    }

    public void StartGhostTime()
    {
        
            isInGhostWorld = true;
        
    }
    public void StopGhostTime()
    {
        isInGhostWorld = false;
    }
}

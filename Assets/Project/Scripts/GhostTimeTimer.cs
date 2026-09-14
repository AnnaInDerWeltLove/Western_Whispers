using UnityEngine;

public class GhostTimeTimer : MonoBehaviour
{
    [SerializeField] private float maxGhostTime = 60f;
    [SerializeField] private WorldManager worldManager;
    private float currentGhostTime;
    private bool isSpiritWorld = false;
    void Start()
    {
        currentGhostTime = maxGhostTime;
    }

    private void Update()
    {
        if (isSpiritWorld)
        {
            currentGhostTime -= Time.deltaTime;
            Debug.Log("Geisterwelt: " + currentGhostTime);

            if (currentGhostTime <= 0)
            {
                worldManager.SetNormalWorld();
                currentGhostTime = 0;
                isSpiritWorld = false;
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
        
            isSpiritWorld = true;
        
    }
    public void StopGhostTime()
    {
        isSpiritWorld = false;
    }
}

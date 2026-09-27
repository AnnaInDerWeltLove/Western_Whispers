using UnityEngine;
using UnityEngine.UI;

public class GhostTimeTimer : MonoBehaviour
{
    [SerializeField] private float maxGhostTime = 60f;
    [SerializeField] private WorldManager worldManager;
    [SerializeField] private Image ghostTimerFullImage;
    [SerializeField] private Image ghostTimerEmptyImage;
    private float currentGhostTime;
    private bool isSpiritWorld;
    private bool isFrozen;
    private void Start()
    {
        currentGhostTime = maxGhostTime;
    }

    private void Update()
    {
        if (isFrozen)
        {
            return;
        }
        if (isSpiritWorld)
        {
            currentGhostTime -= Time.deltaTime;
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
            if (currentGhostTime >= maxGhostTime)
            {
                currentGhostTime = maxGhostTime;
            }
        }
        ghostTimerFullImage.fillAmount = currentGhostTime / maxGhostTime;
    }

    public void StartGhostTime()
    {
        
            isSpiritWorld = true;
            ghostTimerEmptyImage.gameObject.SetActive(true);
            ghostTimerFullImage.gameObject.SetActive(true);
        
    }
    public void StopGhostTime()
    {
        isSpiritWorld = false;
        ghostTimerEmptyImage.gameObject.SetActive(false);
        ghostTimerFullImage.gameObject.SetActive(false);
    }

    public void FreezeGhostTime()
    {
        isFrozen = true;
    }

    public void UnfreezeGhostTime()
    {
        isFrozen = false;
    }
}

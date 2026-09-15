using UnityEngine;

public class SpiritClue : MonoBehaviour
{
    [Header("Objekte des Hinweises")]
    [SerializeField] private GameObject visual;
    [SerializeField] private GameObject clueCollider;
    

    private void OnEnable()
    {
        WorldManager.OnWorldChanged += HandleWorldChanged;
    }


    private void OnDisable()
    {
       
        WorldManager.OnWorldChanged -= HandleWorldChanged;
    }


    private void Start()
    {
        SetSpiritWorldActive(false);
    }


    private void HandleWorldChanged(bool isSpiritWorld)
    {
        SetSpiritWorldActive(isSpiritWorld);
    }


    public void SetSpiritWorldActive(bool isActive)
    {
        visual.SetActive(isActive);
        clueCollider.SetActive(isActive);
    }
}
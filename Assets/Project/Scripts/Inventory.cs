using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private Image[] slots;
    
    private int nextFreeSlot = 0;
    
    private void Start()
    {
        inventoryPanel.SetActive(false);
        foreach (Image slot in slots)
        {
            slot.gameObject.SetActive(false);
        }
    }

    public void AddItem(Sprite itemSprite)
    {
        Debug.Log("AddItem wurde aufgerufen.");
        Debug.Log("Item Sprite: " + itemSprite);
        Debug.Log("Nächster Slot: " + nextFreeSlot);
        Debug.Log("Anzahl Slots: " + slots.Length);
        
        
        if (nextFreeSlot >= slots.Length)
        {
            Debug.Log("Kein freier Slot im Inventar");
            return;
        }
        
        Debug.Log("Verwendeter Slot: " + slots[nextFreeSlot]);

        slots[nextFreeSlot].sprite = itemSprite;
        slots[nextFreeSlot].gameObject.SetActive(true);
        
        Debug.Log("Slot aktiv: " + slots[nextFreeSlot].gameObject.activeSelf);
        nextFreeSlot++;
      
    }

    public void OpenInventory()
    {
        inventoryPanel.SetActive(true);
    }

    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}

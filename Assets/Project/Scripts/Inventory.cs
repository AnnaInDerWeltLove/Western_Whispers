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
        if (nextFreeSlot >= slots.Length)
        {
            Debug.Log("Kein freier Slot im Inventar");
            return;
        }

        slots[nextFreeSlot].sprite = itemSprite;
        slots[nextFreeSlot].gameObject.SetActive(true);
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

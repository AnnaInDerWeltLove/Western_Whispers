using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject itemImage;

    private bool hasItem = false;

    private void start()
    {
        inventoryPanel.SetActive(false);
        itemImage.SetActive(false);
    }

    public void AddItem()
    {
        hasItem = true;
        itemImage.SetActive(true);
        Debug.Log("Item ins Inventar aufgenommen");
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

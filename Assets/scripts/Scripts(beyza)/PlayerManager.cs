using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public HUDManager hudManager;
    // InventoryManager'� buraya Inspector'da ba�layaca��z
    public InventoryManager inventoryManager; // Yeni eklenen sat�r!

    public int currentHealth = 100;
    public int currentGold = 0;
    public int currentMaterials = 0;

    void Start()
    {
        hudManager.UpdateHealth(currentHealth);
        hudManager.UpdateGold(currentGold);
        hudManager.UpdateMaterials(currentMaterials);
    }

    // --- YEN� FONKS�YON: E�ya Ekleme ---
    public void AddItem(Item itemToAdd, int amount = 1)
    {
        // Envanter y�neticisine e�yay� eklemesini s�yle
        if (inventoryManager != null) // inventoryManager ba�l� m� kontrol et
        {
            inventoryManager.AddItemToInventory(itemToAdd, amount);
        }

        // E�er e�ya bir alt�n veya malzeme ise HUD'u da g�ncelle
        if (itemToAdd.itemName == "Altın Para") // Item ad�n� kontrol et
        {
            currentGold += amount;
            hudManager.UpdateGold(currentGold);
        }
        else if (itemToAdd.itemName == "Tahta Malzeme") // Item ad�n� kontrol et
        {
            currentMaterials += amount;
            hudManager.UpdateMaterials(currentMaterials);
        }
        // Di�er e�ya t�rleri i�in de buraya else if ekleyebilirsiniz
    }
}
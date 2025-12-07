using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public HUDManager hudManager;
    // InventoryManager'ý buraya Inspector'da baðlayacaðýz
    public InventoryManager inventoryManager; // Yeni eklenen satýr!

    public int currentHealth = 100;
    public int currentGold = 0;
    public int currentMaterials = 0;

    void Start()
    {
        hudManager.UpdateHealth(currentHealth);
        hudManager.UpdateGold(currentGold);
        hudManager.UpdateMaterials(currentMaterials);
    }

    // --- YENÝ FONKSÝYON: Eþya Ekleme ---
    public void AddItem(Item itemToAdd, int amount = 1)
    {
        // Envanter yöneticisine eþyayý eklemesini söyle
        if (inventoryManager != null) // inventoryManager baðlý mý kontrol et
        {
            inventoryManager.AddItemToInventory(itemToAdd, amount);
        }

        // Eðer eþya bir altýn veya malzeme ise HUD'u da güncelle
        if (itemToAdd.itemName == "Altýn Para") // Item adýný kontrol et
        {
            currentGold += amount;
            hudManager.UpdateGold(currentGold);
        }
        else if (itemToAdd.itemName == "Tahta Malzeme") // Item adýný kontrol et
        {
            currentMaterials += amount;
            hudManager.UpdateMaterials(currentMaterials);
        }
        // Diðer eþya türleri için de buraya else if ekleyebilirsiniz
    }
}
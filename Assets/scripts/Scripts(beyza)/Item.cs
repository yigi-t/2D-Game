using UnityEngine;

// Bu, Unity'nin Inspector penceresinde yeni bir menü oluþturmasýný saðlar
[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
    // Eþyanýn temel özellikleri
    [Header("Eþya Bilgileri")]
    public string itemName = "Yeni Eþya";
    public Sprite itemIcon; // Envanterde görünecek resim
    public int stackSize = 99; // Bir yuvada maksimum kaç tane bulunabilir

    // Eþyanýn ne tür olduðunu belirlemek için (Örn: Hazine, Ýnþaat, Ýksir)
    [Header("Eþya Türü")]
    public bool isConsumable = false; // Kullanýlýp tüketilebilir mi?
    public bool isBuildable = false; // Ýnþaatta kullanýlýr mý?
}

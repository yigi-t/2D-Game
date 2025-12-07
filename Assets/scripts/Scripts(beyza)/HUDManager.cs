using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro kullandýðýnýz için bu zorunlu

public class HUDManager : MonoBehaviour
{
    // YALNIZCA GÖRSEL BAÐLANTILAR (Inspector'da atadýðýnýz nesneler)
    public Slider healthSlider; // Can barý için
    public TextMeshProUGUI goldText; // Altýn sayacý için
    public TextMeshProUGUI materialText; // Malzeme sayacý için

    // NOT: Daha önce burada tanýmlý olan "public int currentGold" vb. deðiþkenleri SÝLÝN.

    // ----------------------------------------------------
    // PLAYERMANAGER TARAFINDAN ÇAÐRILACAK GÜNCELLEME FONKSÝYONLARI
    // ----------------------------------------------------

    // 1. Can Barýný Günceller
    public void UpdateHealth(float health)
    {
        // Maksimum deðeri 100 olarak varsayýyoruz.
        healthSlider.maxValue = 100f;
        healthSlider.value = health;
    }

    // 2. Altýn Sayacýný Günceller
    public void UpdateGold(int gold)
    {
        goldText.text = "Altýn: " + gold.ToString();
    }

    // 3. Malzeme Sayacýný Günceller
    public void UpdateMaterials(int materials)
    {
        materialText.text = "Malzeme: " + materials.ToString();
    }
}
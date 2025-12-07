using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro; // TextMeshProUGUI kullandýðýnýz için bu zorunlu

public class InventoryManager : MonoBehaviour
{
    // --- Inspector Baðlantýlarý ---
    [Header("Görsel Baðlantýlar")]
    public GameObject inventoryPanel;

    // Envanterdeki her bir yuvayý (slotu) temsil eden referanslar
    public GameObject[] inventorySlots; // Tüm yuvalarýn referanslarýný tutar

    // --- Envanter Verileri ---
    [Header("Envanter Verisi")]
    public List<Item> items = new List<Item>(); // Þu anki eþyalar ve miktarlarý

    void Start()
    {
        SetInventoryActive(false);
        // Baþlangýçta tüm yuvalarýn boþ olduðundan emin olmak için
        RefreshInventoryUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SetInventoryActive(!inventoryPanel.activeSelf);
        }
    }

    public void SetInventoryActive(bool isActive)
    {
        inventoryPanel.SetActive(isActive);
        // Envanter açýldýðýnda UI'ý yenile
        if (isActive)
        {
            RefreshInventoryUI();
        }
    }

    // --- YENÝ FONKSÝYON: Envantere Eþya Ekle ---
    public void AddItemToInventory(Item itemToAdd, int amount = 1)
    {
        // Örnek olarak, þimdilik sadece listeye ekliyoruz
        // Ýleride yuva kontrolü ve stackleme (üst üste koyma) mantýðý gelecek
        for (int i = 0; i < amount; i++)
        {
            items.Add(itemToAdd);
        }
        Debug.Log(itemToAdd.itemName + " envantere eklendi. Toplam: " + items.Count);
        RefreshInventoryUI(); // UI'ý güncelle
    }

    // --- YENÝ FONKSÝYON: Envanter UI'ýný Yenile ---
    public void RefreshInventoryUI()
    {
        // Þimdilik sadece ilk yuvayý güncelleyelim
        if (inventorySlots.Length > 0 && items.Count > 0)
        {
            // Ýlk yuvayý doldur
            Image slotIcon = inventorySlots[0].transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI slotCount = inventorySlots[0].transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>();

            slotIcon.sprite = items[0].itemIcon;
            slotIcon.enabled = true; // Ýkonu görünür yap
            slotCount.text = items.Count.ToString(); // Þimdilik tüm eþya sayýsýný göster
            slotCount.enabled = true; // Sayýyý görünür yap
        }
        else if (inventorySlots.Length > 0) // Envanter boþsa ilk yuvayý temizle
        {
            Image slotIcon = inventorySlots[0].transform.Find("ItemIcon").GetComponent<Image>();
            TextMeshProUGUI slotCount = inventorySlots[0].transform.Find("ItemCountText").GetComponent<TextMeshProUGUI>();

            slotIcon.sprite = null;
            slotIcon.enabled = false;
            slotCount.text = "";
            slotCount.enabled = false;
        }
    }
}
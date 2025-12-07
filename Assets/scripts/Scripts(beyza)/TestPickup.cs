using UnityEngine;

public class TestPickup : MonoBehaviour
{
    // --- Inspector Baðlantýlarý ---
    // GAME_MANAGER'a (PlayerManager'a) eriþeceðiz
    public PlayerManager playerManager;

    // Projemizdeki Altin_Item veri dosyasýný buraya baðlayacaðýz
    public Item itemToGive;

    void Update()
    {
        // T tuþuna basýnca 1 Altýn ekle
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (playerManager != null && itemToGive != null)
            {
                // PlayerManager'daki AddItem fonksiyonunu çaðýr
                // 1 Altýn eklemesini istiyoruz
                playerManager.AddItem(itemToGive, 1);
            }
            else
            {
                Debug.LogError("PlayerManager veya ItemToGive atanmamýþ!");
            }
        }
    }
}

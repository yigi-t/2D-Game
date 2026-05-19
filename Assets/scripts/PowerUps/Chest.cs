using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Sandýk Ayarlarý")]
    public GameObject powerUpPrefab;       // Ýçinden çýkacak olan PowerUp prefabý
    public Transform spawnPoint;           // Ýksirin fýrlayacaðý nokta

    [Header("Görsel Ayarlar")]
    public Sprite openedChestSprite;       // Sandýk açýlýnca dönüþeceði görsel

    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;         // Sandýðýn birden fazla kez açýlmasýný önler

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OpenChest()
    {
        isOpened = true;

        // Sandýðýn görselini açýk haliyle deðiþtir
        if (openedChestSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = openedChestSprite;
        }

        // Ödülü oluþtur ve fýrlat
        if (powerUpPrefab != null)
        {
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up * 0.5f;
            spawnPosition.z = 0f;

            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        }

        Debug.Log("Sandýk otomatik açýldý!");
    }

    // Kedi sandýðýn çarpýþma alanýna girdiði an (Trigger) çalýþýr
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer çarpan obje kediyse ve sandýk daha önce açýlmadýysa anýnda aç
        if (!isOpened && collision.CompareTag("Cat"))
        {
            OpenChest();
        }
    }
}
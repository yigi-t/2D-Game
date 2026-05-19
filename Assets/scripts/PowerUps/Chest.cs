using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Sandýk Ayarlarý")]
    // Artýk tek bir GameObject yerine bir dizi (Array) kullanýyoruz: []
    public GameObject[] powerUpPrefabs;
    public Transform spawnPoint;

    [Header("Görsel Ayarlar")]
    public Sprite openedChestSprite;

    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;

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

        // Eðer listeye editörden en az bir tane iksir eklediysek fýrlat
        if (powerUpPrefabs != null && powerUpPrefabs.Length > 0)
        {
            // 0 ile listenin uzunluðu arasýnda rastgele bir sayý tut
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);

            // Tutulan sayýya denk gelen iksiri seç
            GameObject selectedPowerUp = powerUpPrefabs[randomIndex];

            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up * 0.5f;
            spawnPosition.z = 0f;

            // Seçilen iksiri yarat
            Instantiate(selectedPowerUp, spawnPosition, Quaternion.identity);
        }

        Debug.Log("Sandýk açýldý ve rastgele bir ödül fýrladý!");
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOpened && collision.CompareTag("Cat"))
        {
            OpenChest();
        }
    }
}
using UnityEngine;

public class Chest : MonoBehaviour
{
    [Header("Sandýk Ayarlarý")]
    public GameObject powerUpPrefab;       // Ýçinden çýkacak olan PowerUp oyun objesi (Prefab)
    public Transform spawnPoint;           // Ýksirin fýrlayacaðý nokta (Sandýðýn biraz üstü)

    [Header("Görsel Ayarlar")]
    public Sprite openedChestSprite;       // Sandýk açýlýnca dönüþeceði "Açýk Sandýk" görseli

    private SpriteRenderer spriteRenderer;
    private bool isOpened = false;         // Sandýk daha önce açýldý mý?
    private bool isPlayerNearby = false;    // Oyuncu sandýðýn yanýnda mý?

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // Oyuncu yakýndaysa, sandýk açýlmadýysa ve "E" tuþuna bastýysa
        if (isPlayerNearby && !isOpened && Input.GetKeyDown(KeyCode.E))
        {
            OpenChest();
        }
    }

    private void OpenChest()
    {
        isOpened = true;

        // 1. Sandýðýn görselini "Açýk Sandýk" olarak deðiþtir
        if (openedChestSprite != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = openedChestSprite;
        }

        // 2. Ýçindeki ödülü (Power-Up) oluþtur/fýrlat
        if (powerUpPrefab != null)
        {
            // Belirlenen noktada iksiri oluþturuyoruz
            Vector3 spawnPosition = spawnPoint != null ? spawnPoint.position : transform.position + Vector3.up * 0.5f;

            // Rapordaki derinlik hatasýný önlemek için Z eksenini yine 0 yapýyoruz
            spawnPosition.z = 0f;

            GameObject spawnedItem = Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);

            // Ýstersen burada fýrlama efekti hissi vermek için objeyi hafif yukarý itebilirsin:
            Rigidbody2D itemRb = spawnedItem.GetComponent<Rigidbody2D>();
            if (itemRb != null)
            {
                itemRb.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            }
        }

        Debug.Log("Sandýk açýldý ve içinden ödül çýktý!");
    }

    // Oyuncunun yaklaþýp yaklaþmadýðýný rapordaki fizik kurallarýna göre (Is Trigger) algýlýyoruz
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            isPlayerNearby = true;
            // Buraya ekrana "Açmak için E'ye bas" yazýsý getirecek bir UI kodu da ekleyebilirsin
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            isPlayerNearby = false;
        }
    }
}
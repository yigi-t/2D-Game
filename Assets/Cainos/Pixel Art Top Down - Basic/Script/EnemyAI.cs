using UnityEngine;

public class EnemyAI_2D : MonoBehaviour
{
    // === Bileşen Referansları ===
    private Rigidbody2D rb;
    // Animator bileşeni kaldırıldı.

    [SerializeField] private Transform player;

    // === Ayarlar ===
    [Tooltip("Düşmanın maksimum hareket hızı.")]
    public float moveSpeed = 3f;

    [Tooltip("Düşmanın oyuncuyu algılayabileceği maksimum mesafe (Görüş Menzili).")]
    public float sightRange = 8f;

    // ===================================
    // 1. BAŞLANGIÇ AYARLARI (START METODU)
    // ===================================
    void Start()
    {
        // Rigidbody2D bileşenini al.
        rb = GetComponent<Rigidbody2D>();
        // Animator bileşeni referansı kaldırıldı.

        // Oyuncuyu Bul (Eğer Editörde Atanmadıysa)
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

            if (playerObject != null)
            {
                player = playerObject.transform;
            }
            else
            {
                Debug.LogError("EnemyAI_2D: 'Player' etiketiyle bir nesne bulunamadı.");
            }
        }

        if (rb == null) Debug.LogError("Rigidbody2D bileşeni bulunamadı!");
    }

    // ===================================
    // 2. ANA DÖNGÜ VE KARAR VERME
    // ===================================

    // Fizik tabanlı hareket FixedUpdate'te.
    void FixedUpdate()
    {
        // Temel güvenlik kontrolü
        if (player == null)
        {
            StopMoving();
            return;
        }

        // Oyuncuya olan mesafeyi hesapla.
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Durum Kontrolü
        if (distanceToPlayer < sightRange)
        {
            ChasePlayer();
        }
        else
        {
            StopMoving();
        }
    }

    // Yüz Çevirme Update'te (Hareket etmese bile bakması için).
    void Update()
    {
        if (player == null) return;

        // Sadece hedefe doğru yüzünü çevir.
        FlipCharacter(player.position);

        // Animasyon kodları kaldırıldı.
    }

    // Karakteri Yatayda Çevirme Metodu (Değişmedi)
    private void FlipCharacter(Vector3 targetPosition)
    {
        // Oyuncunun konumu ile kendi konumumuz arasındaki X ekseni farkı
        float directionX = targetPosition.x - transform.position.x;

        // Karakterin mevcut ölçek değerlerini al
        Vector3 newScale = transform.localScale;

        // Oyuncu sağdaysa ve karakter sola bakıyorsa (newScale.x < 0)
        if (directionX > 0 && newScale.x < 0)
        {
            newScale.x *= -1; // Sağa dönmek için scale.x'i pozitif yap
        }
        // Oyuncu soldaysa ve karakter sağa bakıyorsa (newScale.x > 0)
        else if (directionX < 0 && newScale.x > 0)
        {
            newScale.x *= -1; // Sola dönmek için scale.x'i negatif yap
        }

        // Değiştirilen ölçeği karakterin transformuna uygula
        transform.localScale = newScale;
    }

    // ChasePlayer() metodu (Değişmedi)
    private void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // StopMoving() metodu (Değişmedi)
    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }
}
using UnityEngine;

public class EnemyAI_Optimized : MonoBehaviour
{
    // === Bileşen Referansları ===
    private Rigidbody2D rb;

    [SerializeField] private Transform player;

    // === Ayarlar ===
    public float moveSpeed = 3f;
    [Tooltip("Düşmanın oyuncuyu algılayabileceği maksimum mesafe (Görüş Menzili).")]
    public float sightRange = 8f;

    // Karekök işleminden kaçınmak için sightRange'in karesi
    private float sightRangeSq;

    // ===================================
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // Optimizasyon için sightRange'in karesini Start'ta hesapla.
        sightRangeSq = sightRange * sightRange;

        if (rb == null) Debug.LogError("Rigidbody2D bileşeni bulunamadı!");
        if (player == null) Debug.LogError("Player Transform'u atanmadı!");

        // *NOT:* Oyuncu bulma mantığı, kodun temiz kalması için kaldırıldı.
    }

    // ===================================
    // 2. ANA DÖNGÜ VE KARAR VERME (FİZİK)
    // ===================================
    void FixedUpdate()
    {
        if (player == null)
        {
            StopMoving();
            return;
        }

        // Vektörel farkın karesini (sqrMagnitude) hesapla.
        float distanceSqToPlayer = (player.position - transform.position).sqrMagnitude;

        // Karekök almadan kontrol: (distance < sightRange) yerine (distance^2 < sightRange^2)
        if (distanceSqToPlayer < sightRangeSq)
        {
            ChasePlayer();
        }
        else
        {
            StopMoving();
        }
    }

    // Yüz Çevirme FixedUpdate/Update ayrımı:
    void Update()
    {
        if (player == null) return;
        FlipCharacter(player.position);
    }

    // Yön Bulma ve Hareket (Fizik)
    private void ChasePlayer()
    {
        // Yönü normalize etmeden sadece konum farkını alıp normalize edilebilir
        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    // Hareketi Durdurma
    private void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }

    // Mevcut FlipCharacter metodunuzu buraya ekleyebilirsiniz.
    private void FlipCharacter(Vector3 targetPosition)
    {
        // ... (FlipCharacter kodunuz)
        float directionX = targetPosition.x - transform.position.x;
        Vector3 newScale = transform.localScale;
        if (directionX > 0 && newScale.x < 0)
        {
            newScale.x *= -1;
        }
        else if (directionX < 0 && newScale.x > 0)
        {
            newScale.x *= -1;
        }
        transform.localScale = newScale;
    }

    // Geliştirme aşamasında görüş menzilini gösteren görselleştirme (Gizmos)
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        // Gizmos.DrawWireSphere, karesel değil, normal sightRange ister.
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Mermi Ayarları")]
    public float speed = 15f;
    public float lifeTime = 2f;
    public int damage = 20; 

    [HideInInspector] public GameObject shooter; 
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb != null)
            rb.linearVelocity = transform.right * speed;

        Destroy(gameObject, lifeTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == shooter) return;
        if (collision.CompareTag("Bullet")) return;

        // === YENİ: DOST ATEŞİ ENGELLEME ===
        // Eğer mermiyi sıkan bir düşmansa ve çarptığı şey de bir düşmansa mermi hasar vermeden yok olsun (veya geçsin)
        if (shooter != null && shooter.CompareTag("Enemy") && collision.CompareTag("Enemy")) 
        {
            return; // Çarpmayı görmezden gel
        }

        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, transform.right);
        }

        Destroy(gameObject); 
    }
}
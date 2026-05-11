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
        // 1. Ateş edene çarpma
        if (collision.gameObject == shooter) return;

        // 2. Diğer mermilere çarpma
        if (collision.CompareTag("Bullet")) return;

        // 3. Hedefte Health scripti var mı? (Sadece canlılara hasar ver)
        Health targetHealth = collision.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.TakeDamage(damage, transform.right);
        }

        // Duvara veya hedefe çarpınca yok ol
        Destroy(gameObject); 
    }
}
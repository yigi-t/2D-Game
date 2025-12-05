using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // --- DEĞİŞKENLER ---
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect;

    [Header("Temas Hasarı Ayarları")]
    public int contactDamageAmount = 20; 
    public string enemyTag = "Enemy"; 
    
    [Header("İttirme Ayarları")]
    public float knockbackForce = 15f; 
    public float knockbackDuration = 0.25f; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    // Düşman (Enemy) etiketine sahip ve Trigger'ı açık bir çarpıştırıcıya temas ettiğinde çalışır.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(enemyTag))
        {
            // Vurulan yönü hesapla: Oyuncunun pozisyonu - Düşmanın pozisyonu
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection.Normalize(); 

            // Hasar verme metodunu çağır (yön bilgisini de göndererek)
            TakeDamage(contactDamageAmount, hitDirection);
        }
    }

    // Hasar alma ve ittirme tetikleme metodu
    public void TakeDamage(int damageAmount, Vector3 hitDirection)
    {
        // 1. Canı azalt
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " hasar aldı! Kalan Can: " + currentHealth);

        // 2. İttirme efektini tetikle
        PlayerController player = GetComponent<PlayerController>();
        if (player != null)
        {
            player.ApplyKnockback(hitDirection, knockbackForce, knockbackDuration);
        }

        // 3. Görsel Efektler (VFX)
        DamageFlash flash = GetComponent<DamageFlash>();
        if (flash != null)
        {
            StartCoroutine(flash.FlashEffect());
        }

        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.15f, 0.2f));
        }

        // 4. Ölüm Kontrolü
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log(gameObject.name + " öldü!");

        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
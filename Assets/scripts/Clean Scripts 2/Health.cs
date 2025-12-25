using System.Collections;
using UnityEngine;

public class Health1 : MonoBehaviour
{
    // --- DEĞİŞKENLER ---
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect;

    [Header("Temas Hasarı Ayarları")]
    public int contactDamageAmount = 20; 
    public string enemyTag = "Enemy"; 
    
    // 👇 YENİ: Hasar Cooldown Ayarları
    [Header("Hasar Cooldown")]
    public float invulnerabilityDuration = 0.5f; // Dokunulmazlık süresi (saniye)
    private bool canTakeDamage = true; // Hasar alıp alamayacağını kontrol eder

    [Header("İttirme Ayarları")]
    public float knockbackForce = 15f; 
    public float knockbackDuration = 0.25f; 

    void Start()
    {
        currentHealth = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 👇 Kontrol eklendi: Sadece hasar alabiliyorsa devam et
        if (canTakeDamage && other.CompareTag(enemyTag))
        {
            Vector3 hitDirection = transform.position - other.transform.position;
            hitDirection.Normalize(); 

            TakeDamage(contactDamageAmount, hitDirection);
        }
    }

    public void TakeDamage(int damageAmount, Vector3 hitDirection)
    {
        // 1. Canı azalt
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " hasar aldı! Kalan Can: " + currentHealth);

        // 2. Hasar alındıktan sonra dokunulmazlık Coroutine'ini başlat
        StartCoroutine(InvulnerabilityRoutine());

        // 3. İttirme efektini tetikle
        KnockbackReceiver knockback = GetComponent<KnockbackReceiver>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(hitDirection, knockbackForce, knockbackDuration);
        }

        // 4. Görsel Efektler ve Ölüm Kontrolü
        DamageFlash flash = GetComponent<DamageFlash>();
        if (flash != null)
        {
            StartCoroutine(flash.FlashEffect());
        }

        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.15f, 0.2f));
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // 👇 YENİ: Dokunulmazlık süresini yöneten Coroutine
    private IEnumerator InvulnerabilityRoutine()
    {
        canTakeDamage = false; // Hasar almayı kapat
        yield return new WaitForSeconds(invulnerabilityDuration); // Belirtilen süre bekle
        canTakeDamage = true; // Hasar almayı tekrar aç
    }

    public GameOverManager gameManager; // Inspector'dan Game Manager'ı ata

    void Die() // Karakter öldüğünde çalışan fonksiyon
    {
        // Karakter animasyonunu oynat, ses çal vs.
        Debug.Log("Öldün!");

        // Game Over ekranını çağır
        gameManager.ShowGameOver();
    }
}
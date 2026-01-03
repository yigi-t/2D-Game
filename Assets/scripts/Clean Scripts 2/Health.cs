using System.Collections; // Coroutine için gerekli
using UnityEngine;

public class Health1 : MonoBehaviour
{
    // --- DEĞİŞKENLER ---
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect; // Patlama Efekti Prefab'ı buraya

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

    public GameOverManager gameManager; // Inspector'dan Game Manager'ı ata

    void Start()
    {
        currentHealth = maxHealth;

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameOverManager>();
        }
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

    // 👇 Dokunulmazlık süresini yöneten Coroutine
    private IEnumerator InvulnerabilityRoutine()
    {
        canTakeDamage = false; // Hasar almayı kapat
        yield return new WaitForSeconds(invulnerabilityDuration); // Belirtilen süre bekle
        canTakeDamage = true; // Hasar almayı tekrar aç
    }

    // --- ÖLÜM VE PATLAMA KISMI (Burayı Düzenledim) ---
    void Die()
    {
        // 1. Patlama Efektini Oluştur
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Debug.Log("Öldün! Patlama oynuyor...");

        // 2. Karakteri GİZLE (Yok etme, sadece görünmez yap)
        // Böylece kod çalışmaya devam eder ve süreyi sayabilir.
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        // Ayrıca karakterin hareket scriptini de durdurmak isteyebilirsin (Opsiyonel)
        // GetComponent<PlayerController>().enabled = false; 

        // 3. Bekleme Sayacını Başlat
        StartCoroutine(BekleVeOyunSonu());
    }

    // Game Over ekranını açmadan önce bekleyen özel fonksiyon
    IEnumerator BekleVeOyunSonu()
    {
        // 1.5 Saniye bekle (Patlama animasyonunu izle)
        yield return new WaitForSeconds(1.5f);

        // Game Over ekranını çağır
        if (gameManager != null)
        {
            gameManager.ShowGameOver();
        }

        // Artık karakteri tamamen silebiliriz
        Destroy(gameObject);
    }
}
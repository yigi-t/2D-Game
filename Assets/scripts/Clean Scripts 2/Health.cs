using System.Collections;
using UnityEngine;

public class Health1 : MonoBehaviour
{
    // --- DEĞİŞKENLER ---
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect; // Patlama Efekti Prefab'ı

    // 👇 YENİ: HealthBar Scriptine Referans
    [Header("UI Bağlantısı")]
    public HealthBar healthBar;

    [Header("Temas Hasarı Ayarları")]
    public int contactDamageAmount = 20;
    public string enemyTag = "Enemy";

    [Header("Hasar Cooldown")]
    public float invulnerabilityDuration = 0.5f;
    private bool canTakeDamage = true;

    [Header("İttirme Ayarları")]
    public float knockbackForce = 15f;
    public float knockbackDuration = 0.25f;

    public GameOverManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;

        // 👇 YENİ: Oyun başladığında can barını fulle
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        if (gameManager == null)
        {
            gameManager = FindFirstObjectByType<GameOverManager>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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

        // 👇 YENİ: Hasar alınca Barı güncelle
        if (healthBar != null)
        {
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        // 2. Dokunulmazlık
        StartCoroutine(InvulnerabilityRoutine());

        // 3. İttirme
        KnockbackReceiver knockback = GetComponent<KnockbackReceiver>();
        if (knockback != null)
        {
            knockback.ApplyKnockback(hitDirection, knockbackForce, knockbackDuration);
        }

        // 4. Efektler
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

    private IEnumerator InvulnerabilityRoutine()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(invulnerabilityDuration);
        canTakeDamage = true;
    }

    void Die()
    {
        if (deathEffect != null)
        {
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }

        Debug.Log("Öldün! Patlama oynuyor...");

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        Collider2D col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;

        StartCoroutine(BekleVeOyunSonu());
    }

    IEnumerator BekleVeOyunSonu()
    {
        yield return new WaitForSeconds(1.5f);

        if (gameManager != null)
        {
            gameManager.ShowGameOver();
        }

        Destroy(gameObject);
    }
}
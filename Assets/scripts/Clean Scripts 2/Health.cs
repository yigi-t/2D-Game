using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Can Ayarları")]
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect; 

    [Header("UI Bağlantısı")]
    public HealthBar healthBar;

    [Header("Hasar Kontrolü")]
    public float invulnerabilityDuration = 0.2f; 
    private bool canTakeDamage = true;

    [Header("İttirme (Knockback) Ayarları")]
    public float knockbackForce = 10f;
    public float knockbackDuration = 0.2f;

    [Header("Referanslar")]
    public GameOverManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;

        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (gameManager == null)
            gameManager = Object.FindFirstObjectByType<GameOverManager>();
    }

    public void TakeDamage(int damageAmount, Vector3 hitDirection)
    {
        if (!canTakeDamage) return;

        currentHealth -= damageAmount;
        
        if (healthBar != null)
            healthBar.UpdateHealthBar(currentHealth, maxHealth);

        // İttirme Efekti (Eğer objede KnockbackReceiver varsa)
        KnockbackReceiver knockback = GetComponent<KnockbackReceiver>();
        if (knockback != null)
            knockback.ApplyKnockback(hitDirection, knockbackForce, knockbackDuration);

        // Hasar Flaş Efekti
        DamageFlash flash = GetComponent<DamageFlash>();
        if (flash != null)
            StartCoroutine(flash.FlashEffect());

        // Kamera Sarsıntısı (Sadece oyuncu hasar alınca)
        if (gameObject.CompareTag("Cat") && CameraShake.Instance != null)
            StartCoroutine(CameraShake.Instance.Shake(0.1f, 0.15f));

        StartCoroutine(InvulnerabilityRoutine());

        if (currentHealth <= 0)
            Die();
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
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        if (gameObject.CompareTag("Cat") && gameManager != null)
            gameManager.ShowGameOver();

        Destroy(gameObject);
    }
}
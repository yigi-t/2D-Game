using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Can Ayarları")]
    public int maxHealth = 100;
    public int currentHealth;
    public GameObject deathEffect; 

    [Header("UI Bağlantıları")]
    [Tooltip("Eğer bu obje kedi ise HUD'daki barı buraya bağlayın.")]
    public HealthBar playerHealthBar;

    [Tooltip("Eğer bu obje düşman ise kafasındaki barı buraya bağlayın.")]
    public HealthBarEnemy enemyHealthBar;

    [Header("Hasar Kontrolü")]
    public float invulnerabilityDuration = 0.1f; 
    private bool canTakeDamage = true;

    [Header("İttirme (Knockback) Ayarları")]
    public float knockbackForce = 5f;
    public float knockbackDuration = 0.15f;

    private GameOverManager gameManager;

    void Start()
    {
        currentHealth = maxHealth;
        gameManager = Object.FindFirstObjectByType<GameOverManager>();
        
        // Oyun başında hangi bar tanımlıysa onu fulle
        TriggerBarUpdate();
    }

    public void TakeDamage(int damageAmount, Vector3 hitDirection)
    {
        if (!canTakeDamage) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); 
        
        // Can barlarını güncelle
        TriggerBarUpdate();

        // İttirme Efekti
        KnockbackReceiver knockback = GetComponent<KnockbackReceiver>();
        if (knockback != null)
            knockback.ApplyKnockback(hitDirection, knockbackForce, knockbackDuration);

        // Flaş Efekti
        DamageFlash flash = GetComponent<DamageFlash>();
        if (flash != null)
            StartCoroutine(flash.FlashEffect());

        // Kamera Sarsıntısı (Sadece oyuncu darbe alınca)
        if (gameObject.CompareTag("Cat") && CameraShake.Instance != null)
            StartCoroutine(CameraShake.Instance.Shake(0.1f, 0.15f));

        if (currentHealth <= 0)
            Die();
        else
            StartCoroutine(InvulnerabilityRoutine());
    }

    private void TriggerBarUpdate()
    {
        // Kedi hasar aldıysa oyuncu barını güncelle
        if (playerHealthBar != null)
        {
            playerHealthBar.UpdateHealthBar(currentHealth, maxHealth);
        }

        // Düşman hasar aldıysa düşman barını güncelle
        if (enemyHealthBar != null)
        {
            enemyHealthBar.UpdateHealthBar(currentHealth, maxHealth);
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
            Instantiate(deathEffect, transform.position, Quaternion.identity);

        Debug.Log(gameObject.name + " elendi!");

        if (gameObject.CompareTag("Cat") && gameManager != null)
            gameManager.ShowGameOver();

        Destroy(gameObject);
    }
}
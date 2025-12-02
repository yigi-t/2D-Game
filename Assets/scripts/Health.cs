using System.Collections;
using UnityEngine;

public class Health : MonoBehaviour
{
    // --- DEÐÝÞKENLER ---
    public int maxHealth = 100; // Baþlangýç caný
    public int currentHealth;   // Þu anki can
    public GameObject deathEffect; // Ölüm efekti prefabý buraya

    void Start()
    {
        // Oyuna baþlarken caný fulle
        currentHealth = maxHealth;
    }

    void Update()
    {
        // TEST ÝÇÝN: Klavyeden 'H' tuþuna basarsan karakter hasar alýr.
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }
    }

    // --- SENÝN SORDUÐUN FONKSÝYON BURADA ---
    public void TakeDamage(int damageAmount)
    {
        // 1. Caný azalt
        currentHealth -= damageAmount;
        Debug.Log(gameObject.name + " hasar aldý! Kalan Can: " + currentHealth);

        // --- GÖRSEL EFEKTLER (VFX) ---

        // A) Karakter Kýzarsýn (DamageFlash scripti varsa çalýþtýr)
        DamageFlash flash = GetComponent<DamageFlash>();
        if (flash != null)
        {
            StartCoroutine(flash.FlashEffect());
        }

        // B) Ekran Sallansýn (CameraShake scripti varsa çalýþtýr)
        if (CameraShake.Instance != null)
        {
            StartCoroutine(CameraShake.Instance.Shake(0.15f, 0.2f));
        }

        // -----------------------------

        // 2. Ölüm Kontrolü
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
            // Efekti, ölen objenin tam olduðu yerde oluþtur
            Instantiate(deathEffect, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
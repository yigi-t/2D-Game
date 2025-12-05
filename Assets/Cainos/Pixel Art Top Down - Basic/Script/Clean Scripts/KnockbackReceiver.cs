using System.Collections;
using UnityEngine;

public class KnockbackReceiver : MonoBehaviour
{
    private Rigidbody2D rb;
    private PlayerMovement playerMovement;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerMovement = GetComponent<PlayerMovement>(); 
    }

    public void ApplyKnockback(Vector3 direction, float force, float duration)
    {
        StopAllCoroutines(); 
        StartCoroutine(KnockbackRoutine(direction, force, duration));
    }

    private IEnumerator KnockbackRoutine(Vector3 direction, float force, float duration)
    {
        // 1. KONTROLÜ KAPAT
        if (playerMovement != null)
        {
            playerMovement.CanMove = false; 
        }

        // 2. ANLIK VURUŞ (ForceMode2D.Impulse kullanıldı)
        rb.AddForce(direction * force, ForceMode2D.Impulse);

        // 3. BEKLEME (İttirme süresi)
        float elapsed = 0f;
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            yield return null;
        }

        // 4. YUMUŞAK DURMA (Kayma/Sürtünme Efekti): Hızı yumuşak bir şekilde sıfıra indir.
        float smoothDuration = 0.15f; 
        Vector2 startVelocity = rb.linearVelocity; // linearVelocity kullanıldı

        elapsed = 0f;
        while (elapsed < smoothDuration)
        {
            float t = elapsed / smoothDuration; 
            // linearVelocity kullanıldı
            rb.linearVelocity = Vector2.Lerp(startVelocity, Vector2.zero, t);

            elapsed += Time.deltaTime;
            yield return null;
        }
        
        // 5. KONTROLÜ GERİ VER
        if (playerMovement != null)
        {
            playerMovement.CanMove = true;
        }
        rb.linearVelocity = Vector2.zero; // linearVelocity kullanıldı
    }
}
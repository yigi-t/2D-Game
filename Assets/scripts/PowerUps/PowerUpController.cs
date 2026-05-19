using System.Collections;
using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PowerUpData powerUpData;
    private SpriteRenderer spriteRenderer;

    private bool canBePickedUp = false;
    private Transform playerTransform; // Kedinin konumunu takip edeceðiz

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        if (powerUpData != null && spriteRenderer != null)
        {
            spriteRenderer.sprite = powerUpData.icon;
        }

        // Kediyi sahneden Tag ile bul ve referansýný al
        GameObject cat = GameObject.FindGameObjectWithTag("Cat");
        if (cat != null)
        {
            playerTransform = cat.transform;
        }

        // Fýrlama animasyonunu baþlat
        StartCoroutine(PopOutAnimation());
    }

    private void Update()
    {
        // Eðer alýnabilir durumdaysa ve kedi sahnedeyse mesafe kontrolü yap
        if (canBePickedUp && playerTransform != null)
        {
            // FPS düþüþünü önlemek için sqrMagnitude optimizasyonu kullanýyoruz
            float distanceSqr = (transform.position - playerTransform.position).sqrMagnitude;

            // Eðer aradaki mesafe (karesi) 1.5 birimden küçükse (kedi yeterince yakýnsa) iksiri al
            // (1.5f deðerini kedinin büyüklüðüne göre artýrýp azaltabilirsin)
            if (distanceSqr < 1.5f)
            {
                PlayerPowerUpManager manager = playerTransform.GetComponent<PlayerPowerUpManager>();
                if (manager != null)
                {
                    manager.ApplyPowerUp(powerUpData);
                    Destroy(gameObject);
                }
            }
        }
    }

    private IEnumerator PopOutAnimation()
    {
        Vector2 startPos = transform.position;
        Vector2 randomOffset = new Vector2(Random.Range(-1.5f, 1.5f), Random.Range(-1.2f, -0.5f));
        Vector2 targetPos = startPos + randomOffset;

        float duration = 0.5f;
        float height = 1.2f;
        float timePassed = 0f;

        while (timePassed < duration)
        {
            timePassed += Time.deltaTime;
            float progress = timePassed / duration;

            Vector2 currentPos = Vector2.Lerp(startPos, targetPos, progress);
            float jumpHeight = Mathf.Sin(progress * Mathf.PI) * height;

            transform.position = new Vector3(currentPos.x, currentPos.y + jumpHeight, 0f);
            yield return null;
        }

        transform.position = new Vector3(targetPos.x, targetPos.y, 0f);

        // Animasyon bitti, artýk alýnabilir!
        canBePickedUp = true;
    }
}
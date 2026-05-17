using UnityEngine;

public class PowerUpController : MonoBehaviour
{
    public PowerUpData powerUpData;
    private SpriteRenderer spriteRenderer;

    // Eþyanýn anýnda alýnmasýný engellemek için bir kilit koyuyoruz
    private bool canBePickedUp = false;

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

        // Obje doðduktan tam 0.5 saniye sonra "EnablePickup" fonksiyonunu çalýþtýr
        Invoke("EnablePickup", 0.5f);
    }

    private void EnablePickup()
    {
        canBePickedUp = true; // Yarým saniye doldu, artýk eþya alýnabilir!
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Eðer henüz alýnma süresi dolmadýysa kodun devamýný çalýþtýrma
        if (!canBePickedUp) return;

        if (collision.CompareTag("Cat"))
        {
            PlayerPowerUpManager manager = collision.GetComponent<PlayerPowerUpManager>();
            if (manager != null)
            {
                manager.ApplyPowerUp(powerUpData);
                Destroy(gameObject);
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI; // UI elementlerine erişmek için şart!

public class HealthBar : MonoBehaviour
{
    [Header("UI Görsel Bileşeni")]
    [Tooltip("Dolgulu (Filled) modda çalışan can görseli.")]
    public Image barImage; 

    // Can değişiminde çağrılacak ana fonksiyon
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (barImage != null)
            UpdateFill(currentHealth, maxHealth);
        else
            TryToFindImage(currentHealth, maxHealth);
    }

    // Görsel doluluk oranını güvenli bir şekilde güncelleyen iç fonksiyon
    private void UpdateFill(float current, float max)
    {
        if (max > 0)
        {
            // Oranı hesapla (Örn: 50f / 100f = 0.5f)
            barImage.fillAmount = current / max;
        }
    }

    // Mühendislik Önlemi: Eğer Image atanmadıysa otomatik bulmaya çalışır
    private void TryToFindImage(float current, float max)
    {
        barImage = GetComponent<Image>();
        if (barImage == null)
        {
            barImage = GetComponentInChildren<Image>();
        }

        if (barImage != null)
        {
            UpdateFill(current, max);
        }
        else
        {
            Debug.LogWarning(gameObject.name + " üzerindeki HealthBar scriptine bir 'Image' bileşeni atanmamış!");
        }
    }
}
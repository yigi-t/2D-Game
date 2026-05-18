using UnityEngine;
using UnityEngine.UI; // UI elementleri için şart!

public class HealthBarEnemy : MonoBehaviour
{
    [Header("Düşman UI Görsel Bileşeni")]
    [Tooltip("CanTuvali içindeki dolgulu (Filled) modda çalışan yeşil can görseli.")]
    public Image enemyBarImage; 

    // Sadece düşman can değişiminde çağrılacak fonksiyon
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        if (enemyBarImage != null)
        {
            if (maxHealth > 0)
            {
                // Oranı hesapla ve düşmanın kafasındaki barı güncelle
                enemyBarImage.fillAmount = currentHealth / maxHealth;
            }
        }
        else
        {
            // Eğer Inspector'dan atamayı unuttuysan otomatik bulma garantisi
            enemyBarImage = GetComponent<Image>();
            if (enemyBarImage == null)
            {
                enemyBarImage = GetComponentInChildren<Image>();
            }
        }
    }
}
using UnityEngine;
using UnityEngine.UI; // UI elementlerine eriþmek için bu þart!

public class HealthBar : MonoBehaviour
{
    public Image barImage; // Inspector'dan atayacaðýmýz bar görseli

    // Bu fonksiyonu karakter scriptinden çaðýracaðýz
    public void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        // Oraný hesapla (Örn: 50 / 100 = 0.5 yani yarýsý dolu)
        barImage.fillAmount = currentHealth / maxHealth;
    }
}

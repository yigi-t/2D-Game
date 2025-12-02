using System.Collections;
using UnityEngine;

public class DamageFlash : MonoBehaviour
{
    // Rengini deðiþtireceðimiz Sprite (Karakterin resmi)
    public SpriteRenderer mySprite;

    // Hasar alýnca hangi renk olsun? (Kýrmýzý)
    public Color flashColor = Color.red;

    // Orijinal rengi hafýzada tutmak için
    private Color originalColor;

    void Start()
    {
        // Baþlangýçta karakterin kendi rengini hafýzaya alýyoruz
        if (mySprite != null)
        {
            originalColor = mySprite.color;
        }
    }

    // Dýþarýdan çaðýracaðýmýz asýl fonksiyon bu
    public IEnumerator FlashEffect()
    {
        if (mySprite != null)
        {
            // 1. Rengi Kýrmýzý yap
            mySprite.color = flashColor;

            // 2. Çok kýsa bekle (0.1 saniye)
            yield return new WaitForSeconds(0.1f);

            // 3. Rengi eski haline döndür
            mySprite.color = originalColor;
        }
    }
}

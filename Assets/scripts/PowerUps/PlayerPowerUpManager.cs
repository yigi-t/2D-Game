using System.Collections;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    // Projenizdeki mevcut hareket ve can scriptlerinin isimlerini buraya göre güncelleyebilirsiniz.
    // Örnek olarak yaygýn kullanýlan isimleri açabilmeniz için aþaðýya ekledim:
    private PlayerMovement movementScript; 
    private Health healthScript; 

    private void Awake()
    {
        // movementScript = GetComponent<PlayerMovement>();
        // healthScript = GetComponent<Health>();
    }

    public void ApplyPowerUp(PowerUpData data)
    {
        if (data.isPermanent)
        {
            // Kalýcý etkileri direkt uygula (Örn: Anlýk can yenileme)
            ExecutePowerUp(data, true);
        }
        else
        {
            // Geçici etkileri Coroutine (Zamanlayýcý) ile baþlat
            StartCoroutine(TemporaryPowerUpRoutine(data));
        }
    }

    private IEnumerator TemporaryPowerUpRoutine(PowerUpData data)
    {
        // Etkiyi baþlat (Örn: Hýz arttý)
        ExecutePowerUp(data, true);

        // Scriptable Object'te belirlenen süre kadar bekle
        yield return new WaitForSeconds(data.duration);

        // Süre bitince etkiyi geri al (Örn: Hýz eski haline döndü)
        ExecutePowerUp(data, false);
    }

    private void ExecutePowerUp(PowerUpData data, bool startEffect)
    {
        switch (data.type)
        {
            case PowerUpData.PowerUpType.Speed:
                // Týklanan güce göre hýzý artýrýp azaltacaðýmýz alan
                // float multiplier = startEffect ? data.value : -data.value;
                // movementScript.speed += multiplier;
                Debug.Log($"Hiz Degisimi: {data.powerUpName} | Aktif mi: {startEffect} | Deger: {data.value}");
                break;

            case PowerUpData.PowerUpType.Health:
                if (startEffect)
                {
                    // can ekleme fonksiyonunu buraya baðlayacaðýz
                    // healthScript.Heal(data.value);
                    Debug.Log($"Can Eklendi: {data.powerUpName} -> +{data.value}");
                }
                break;

            case PowerUpData.PowerUpType.Damage:
                // Hasar artýþý mekanizmasý buraya gelecek
                Debug.Log($"Hasar Degisimi: {data.powerUpName} | Aktif mi: {startEffect} | Deger: {data.value}");
                break;
        }
    }
}
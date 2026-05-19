using System.Collections;
using UnityEngine;

public class PlayerPowerUpManager : MonoBehaviour
{
    public void ApplyPowerUp(PowerUpData data)
    {
        if (data.isPermanent)
        {
            ExecutePowerUp(data, true);
        }
        else
        {
            StartCoroutine(TemporaryPowerUpRoutine(data));
        }
    }

    private IEnumerator TemporaryPowerUpRoutine(PowerUpData data)
    {
        ExecutePowerUp(data, true);
        yield return new WaitForSeconds(data.duration);
        ExecutePowerUp(data, false);
    }

    // ÝÞTE SENÝN YAZDIÐIN KISIM BURADA OLMALI:
    private void ExecutePowerUp(PowerUpData data, bool startEffect)
    {
        switch (data.type)
        {
            case PowerUpData.PowerUpType.Speed:
                // movementScript.speed += multiplier;
                Debug.Log($"Hiz Degisimi: {data.powerUpName} | Aktif mi: {startEffect}");
                break;

            case PowerUpData.PowerUpType.Health:
                if (startEffect)
                {
                    // healthScript.Heal(data.value); 
                    Debug.Log($"Can Eklendi: +{data.value} HP");
                }
                break;

            case PowerUpData.PowerUpType.Damage:
                // attackScript.bulletDamage += damageModifier;
                Debug.Log($"Hasar Degisimi: {data.powerUpName} | Aktif mi: {startEffect}");
                break;
        }
    }
}
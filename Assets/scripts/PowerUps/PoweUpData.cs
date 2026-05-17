using UnityEngine;

// Editörde sað týklama menüsüne ekleme yapýyoruz
[CreateAssetMenu(fileName = "NewPowerUp", menuName = "Roguelike/PowerUp Data")]
public class PowerUpData : ScriptableObject
{
    public string powerUpName;          // Güçlendiricinin adý (Örn: "Kedi Nanesi")
    public Sprite icon;                 // Arayüzde veya yerde görünecek simgesi

    // Güçlendiricinin etkileyeceði nitelik türleri
    public enum PowerUpType { Speed, Health, Damage }
    public PowerUpType type;

    public float value;                 // Deðiþim miktarý (Örn: +2 hýz veya +20 can)
    public float duration;              // Etki süresi (Saniye cinsinden. Kalýcý ise 0 yapabiliriz)
    public bool isPermanent;            // Etki kalýcý mý?
}
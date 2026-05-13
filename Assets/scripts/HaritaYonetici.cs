using UnityEngine;
using Unity.Cinemachine;

public class HaritaYonetici : MonoBehaviour
{
    [Header("Kamera Ayarları")]
    public string playerTag = "Cat";

    void Start()
    {
        // Sahne başlar başlamaz sadece kamerayı bağla
        KamerayiKediyeBagla();
    }

    // Bu fonksiyonu public yaptık ki başka yerlerden de çağrılabilsin
    public void KamerayiKediyeBagla()
    {
        GameObject kedi = GameObject.FindWithTag(playerTag);
        
        // Cinemachine kamerasını sahne üzerinde ara
        CinemachineCamera vcam = Object.FindFirstObjectByType<CinemachineCamera>();

        if (kedi != null && vcam != null)
        {
            vcam.Follow = kedi.transform;
            Debug.Log("<color=green>Başarılı:</color> Kamera '" + kedi.name + "' objesine odaklandı.");
        }
        else
        {
            if (kedi == null) Debug.LogWarning("DİKKAT: '" + playerTag + "' etiketli oyuncu bulunamadı!");
            if (vcam == null) Debug.LogWarning("DİKKAT: Sahne üzerinde CinemachineCamera bulunamadı!");
        }
    }
}
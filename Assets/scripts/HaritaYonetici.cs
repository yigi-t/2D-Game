using UnityEngine;
using Unity.Cinemachine; // Cinemachine 3.x kütüphanesi

public class HaritaYonetici : MonoBehaviour
{
    public GameObject haritaPrefabi;

    [Header("Asset Gruplarý")]
    public GameObject[] buyukObjeler;
    public GameObject[] kucukObjeler;

    private GameObject mevcutHarita;

    void Start()
    {
        HaritayiYenile();
    }

    public void HaritayiYenile()
    {
        // 1. Eski haritayý temizle
        if (mevcutHarita != null) Destroy(mevcutHarita);

        // 2. Yeni haritayý oluþtur
        mevcutHarita = Instantiate(haritaPrefabi, Vector3.zero, Quaternion.identity);

        // 3. Eþyalarý noktalara daðýt
        Transform kenarGrubu = mevcutHarita.transform.Find("DogusNoktalari/KenarNoktalar");
        if (kenarGrubu != null) ObjeleriDagit(kenarGrubu, buyukObjeler, 0.6f);

        Transform yolGrubu = mevcutHarita.transform.Find("DogusNoktalari/YolNoktalar");
        if (yolGrubu != null) ObjeleriDagit(yolGrubu, kucukObjeler, 0.2f);

        // 4. KAMERA TAKÝBÝ: Yeni oluþan kediyi "Cat" etiketiyle bul
        GameObject kedi = GameObject.FindWithTag("Cat");

        if (kedi != null)
        {
            // Sahnedeki Cinemachine kamerasýný bul
            CinemachineCamera vcam = FindAnyObjectByType<CinemachineCamera>();

            if (vcam != null)
            {
                // Kamerayý kediye odakla
                vcam.Follow = kedi.transform;
            }
        }
        else
        {
            Debug.LogWarning("DÝKKAT: 'Cat' etiketli bir obje bulunamadý! Lütfen kedi prefabýnýn Tag ayarýný kontrol edin.");
        }
    }

    void ObjeleriDagit(Transform grup, GameObject[] secenekler, float sans)
    {
        foreach (Transform nokta in grup)
        {
            if (Random.value < sans && secenekler.Length > 0)
            {
                int secim = Random.Range(0, secenekler.Length);
                Instantiate(secenekler[secim], nokta.position, Quaternion.identity, nokta);
            }
        }
    }
}
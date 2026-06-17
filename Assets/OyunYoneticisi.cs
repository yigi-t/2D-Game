using UnityEngine;

public class OyunYoneticisi : MonoBehaviour
{
    // Unity editöründen sürükleyip býrakacaðýmýz Kazanma Paneli
    public GameObject kazanmaPaneli;

    // Sahnedeki güncel düþman sayýsýný tutacak
    private int kalanDusmanSayisi = 5;

    void Start()
    {
        // Oyun baþýnda panelin kapalý olduðundan emin olalým
        if (kazanmaPaneli != null)
        {
            kazanmaPaneli.SetActive(false);
        }
    }

    // Her düþman öldüðünde bu fonksiyonu çaðýracaðýz
    public void DusmanOldu()
    {
        kalanDusmanSayisi--;

        // Eðer tüm düþmanlar (5 tanesi de) öldüyse
        if (kalanDusmanSayisi <= 0)
        {
            OyunKazanildi();
        }
    }

    void OyunKazanildi()
    {
        // Kazanma panelini aktif et (Ekrana getirir)
        if (kazanmaPaneli != null)
        {
            kazanmaPaneli.SetActive(true);
        }

        // Ýsteðe baðlý: Oyun dünyasýný durdurmak istersen arka planda her þeyi dondurur
        Time.timeScale = 0f;
    }
}
using UnityEngine;

public class GameStartManager : MonoBehaviour
{
    public GameObject mainMenuPanel; // Yaptýðýmýz paneli buraya atacaðýz

    void Start()
    {
        // Oyun baþlar baþlamaz zamaný donduruyoruz.
        // Böylece düþmanlar hareket etmez, fizik çalýþmaz.
        Time.timeScale = 0f;

        // Menü panelinin açýk olduðundan emin oluyoruz.
        mainMenuPanel.SetActive(true);
    }

    public void OyunaBasla()
    {
        // Zamaný normale döndür (zaman akmaya baþlasýn)
        Time.timeScale = 1f;

        // Menü panelini kapat (gizle)
        mainMenuPanel.SetActive(false);
    }

    public void OyundanCikis()
    {
        Debug.Log("Çýkýþ Yapýldý");
        Application.Quit();
    }
}

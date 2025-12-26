using UnityEngine;
using UnityEngine.SceneManagement; // Sahneler arasý geçiþ için þart

public class GameOverManager : MonoBehaviour
{
    [Header("UI Elemanlarý")]
    public GameObject gameOverPanel; // Panelimizi buraya sürükleyeceðiz

    // Oyun bittiðinde bu fonksiyonu çaðýracaðýz
    public void ShowGameOver()
    {
        Debug.Log("Panel açýlma komutu aldý!"); // BU SATIRI EKLE
        gameOverPanel.SetActive(true); // Paneli görünür yap
        Time.timeScale = 0f; // Oyunu dondur (arka planda hareket dursun)
    }

    // "Tekrar Oyna" butonu için
    public void RestartGame()
    {
        Time.timeScale = 1f; // Zamaný tekrar akýtmayý unutma!
        // Þu anki sahneyi yeniden yükler
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // "Ana Menü" butonu için
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Zamaný normalleþtir
        SceneManager.LoadScene("MainMenu");
    }
}

using UnityEngine;
using UnityEngine.SceneManagement; // Yeniden başlatma ve menü butonları için şart

public class GameWinManager : MonoBehaviour
{
    [Header("UI Elemanları")]
    public GameObject gameWinPanel; // Zafer panelimizi buraya sürükleyeceğiz

    private bool isGameWon = false;

    // Tüm düşmanlar öldüğünde Health.cs bu fonksiyonu tetikleyecek
    public void ShowGameWin()
    {
        if (isGameWon) return;
        isGameWon = true;

        Debug.Log("Tebrikler! Game Win Paneli açılma komutu aldı!");
        if (gameWinPanel != null)
        {
            gameWinPanel.SetActive(true); // Paneli görünür yap
            Time.timeScale = 0f; // Oyunu dondur
        }
    }

    // Zafer ekranındaki "Tekrar Oyna" butonu için
    public void RestartGame()
    {
        Time.timeScale = 1f; // Zamanı akıt
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Zafer ekranındaki "Ana Menü" butonu için
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Zamanı normalleştir
        SceneManager.LoadScene("MainMenu");
    }
}
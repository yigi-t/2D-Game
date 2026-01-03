using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    // Bu deðiþken Inspector'da panelimizi içine koymamýz için
    public GameObject pauseMenuPanel;
    private bool isPaused = false;

    void Update()
    {
        // Klavyeden ESC tuþuna basýldýðýnda
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame(); // Eðer oyun zaten durmuþsa devam ettir
            }
            else
            {
                PauseGame(); // Eðer oyun akýyorsa durdur
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true); // Paneli görünür yap
        Time.timeScale = 0f;            // Oyunu tamamen dondur (Zamaný durdurur)
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false); // Paneli gizle
        Time.timeScale = 1f;             // Oyunu normale döndür
        isPaused = false;
    }

    public void RetryGame()
    {
        Time.timeScale = 1f; // Sahne yüklemeden önce zamaný açmalýyýz yoksa oyun donuk baþlar
        // Þu anki sahneyi baþtan yükle
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0); // 0 numaralý sahne (Ana Menü) yükle
    }
}

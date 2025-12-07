using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    // Arayüzden Ayarlar Panelini buraya baðlayacaðýz
    public GameObject settingsPanel;

    // OYUNA BAÞLA düðmesi
    public void StartGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    // AYARLAR düðmesi: Paneli görünür yapar
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    // Kapama butonu için: Paneli gizler
    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // ÇIKIÞ düðmesi
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
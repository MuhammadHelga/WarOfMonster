using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{

    public GameObject gameStopPanel;

    void Start()
    {
        if (gameStopPanel != null)
        {
            gameStopPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("GameStopPanel GameObject tidak ditetapkan ke PanelController!");
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Kembali ke Menu Utama");
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        Debug.Log("Permainan Dimulai Ulang");
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ContinueGame()
    {
        GameObject gameStopPanel = GameObject.Find("GameStopPanel");
        if (gameStopPanel != null)
        {
            gameStopPanel.SetActive(false);
            Time.timeScale = 1f;
        }
        else
        {
            Debug.LogError("GameStopPanel tidak ditemukan!");
        }
    }

    public void ToggleGameStopPanel()
    {
        if (gameStopPanel != null)
        {
            bool isPanelActive = gameStopPanel.activeSelf;
            gameStopPanel.SetActive(!isPanelActive);
            Time.timeScale = isPanelActive ? 1f : 0f;
        }
        else
        {
            Debug.LogError("GameStopPanel tidak ditugaskan ke PanelController!");
        }
    }
}


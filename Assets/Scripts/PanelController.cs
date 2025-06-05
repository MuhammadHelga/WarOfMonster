using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelController : MonoBehaviour
{

    public GameObject gameStopPanel; // Inspector'dan atanacak

    void Start()
    {
        // GameStopPanel'in başlangıçta kapalı olduğundan emin ol
        if (gameStopPanel != null)
        {
            gameStopPanel.SetActive(false);
        }
        else
        {
            Debug.LogError("GameStopPanel GameObject'i PanelController'a atanmamış!");
        }
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("Ana Menüye Dönülüyor...");
        Time.timeScale = 1f; // Oyunu devam ettir (eğer durdurulmuşsa)
        SceneManager.LoadScene(0); // 0 indexli sahneyi yükle
    }

    public void RestartGame()
    {
        Debug.Log("Oyun Yeniden Başlatılıyor...");
        Time.timeScale = 1f; // Oyunu devam ettir (eğer durdurulmuşsa)
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Mevcut sahneyi yeniden yükle
    }

    public void ContinueGame()
    {
        // Bu fonksiyon GameStopPanel'den çağrılacak ve oyunu devam ettirecek.
        // GameStopPanel'in kendisini kapatma mantığına ihtiyacımız olacak.
        GameObject gameStopPanel = GameObject.Find("GameStopPanel");
        if (gameStopPanel != null)
        {
            gameStopPanel.SetActive(false);
            Time.timeScale = 1f; // Oyunu devam ettir
        }
        else
        {
            Debug.LogError("GameStopPanel bulunamadı!");
        }
    }

    public void ToggleGameStopPanel()
    {
        if (gameStopPanel != null)
        {
            bool isPanelActive = gameStopPanel.activeSelf;
            gameStopPanel.SetActive(!isPanelActive);
            Time.timeScale = isPanelActive ? 1f : 0f; // Panel aktifse devam et, değilse durdur
        }
        else
        {
            Debug.LogError("GameStopPanel PanelController'a atanmamış!");
        }
    }
}


using UnityEngine;
using UnityEngine.SceneManagement; // For scene management
public class MainMenuController : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }
}

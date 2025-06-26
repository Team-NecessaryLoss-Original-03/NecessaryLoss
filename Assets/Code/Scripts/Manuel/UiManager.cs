using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("SCN_LEVEL_ZERO");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
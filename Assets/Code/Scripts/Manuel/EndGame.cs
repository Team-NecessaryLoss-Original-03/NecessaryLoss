using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public void GoToMainMenu()
    {
        SceneManager.LoadScene("SCN_MainMenu");
    }

    private void Quit()
    {
        Application.Quit();
    }
}

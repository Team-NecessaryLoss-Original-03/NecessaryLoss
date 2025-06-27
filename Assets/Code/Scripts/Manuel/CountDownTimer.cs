using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CountdownController : MonoBehaviour
{
    public float countdownTime = 5f;
    public Text countdownText;

    void Update()
    {
        if (countdownTime > 0)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(countdownTime).ToString();
        }
        else
        {
            // Carica la scena di Game Over una volta arrivati a 0
            SceneManager.LoadScene("GameOver");
        }
    }
}
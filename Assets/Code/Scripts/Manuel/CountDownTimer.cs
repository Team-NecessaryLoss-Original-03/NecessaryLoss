using UnityEngine;
using UnityEngine.UI;

public class CountdownController : MonoBehaviour
{
    public float countdownTime = 5f;
    public Text countdownText;
    public GameObject gameOverPanel; // Pannello da attivare

    private bool countdownFinished = false;

    void Start()
    {
        gameOverPanel.SetActive(false); // Nasconde il pannello all'inizio
    }

    void Update()
    {
        if (!countdownFinished)
        {
            countdownTime -= Time.deltaTime;
            countdownText.text = Mathf.Ceil(countdownTime).ToString();

            if (countdownTime <= 0)
            {
                countdownFinished = true;
                countdownText.gameObject.SetActive(false); // Nasconde il testo del countdown
                gameOverPanel.SetActive(true);             // Mostra il pannello Game Over
            }
        }
    }
}
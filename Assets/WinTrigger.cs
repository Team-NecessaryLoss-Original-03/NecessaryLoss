using UnityEngine;

public class WinTrigger : MonoBehaviour
{
    public GameObject winPanel; 

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            winPanel.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
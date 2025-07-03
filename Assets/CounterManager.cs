using UnityEngine;
using UnityEngine.UI;

public class TagCounter : MonoBehaviour
{
    public string tagToCount = "Enemy"; 
    public Text counterText; 

    private int currentCount;

    void Update()
    {
        
        GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tagToCount);
        currentCount = taggedObjects.Length;

        
        if (counterText != null)
        {
            counterText.text = "Oggetti con tag '" + tagToCount + "': " + currentCount.ToString();
        }
    }

}
using UnityEngine;

public class Listener : MonoBehaviour
{
    private void Awake()
    {
        Interactable.OnActivation -= OpenSesamis;
        Interactable.OnActivation += OpenSesamis;
    }

    private void OpenSesamis(string objName)
    {
        Debug.Log($"OpenSesamis from {objName}");
    }
}

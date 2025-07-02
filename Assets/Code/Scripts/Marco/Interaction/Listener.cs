using UnityEngine;

public class Listener : MonoBehaviour
{
    [SerializeField] private string myID;

    private void Awake()
    {
        EnviromentInteractable.OnActivation -= OpenSesamis;
        EnviromentInteractable.OnActivation += OpenSesamis;
    }

    private void OpenSesamis(string objName)
    {
        Debug.Log($"OpenSesamis from {objName}");
        if (objName.Equals(myID))
        {
            this.transform.position += Vector3.up * 12;
        }
    }
}

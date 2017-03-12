using UnityEngine;

public class CloseApp : MonoBehaviour
{
    public GameObject panelExit;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            panelExit.SetActive(true);
        }
    }
}
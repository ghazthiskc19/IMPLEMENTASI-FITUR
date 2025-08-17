using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void OnQuit()
    {
        Debug.Log("Quit Done");
        Application.Quit();
    }
}

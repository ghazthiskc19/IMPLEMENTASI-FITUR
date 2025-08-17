using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    public string nextScene;
    public void SceneSwitch()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }
}

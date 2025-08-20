using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PlayButton : MonoBehaviour
{
    public string nextScene;
    public void SceneSwitch()
    {
        StartCoroutine(SceneSwitchDelay());
    }
    private IEnumerator SceneSwitchDelay()
    {
        yield return new WaitForSeconds(1f);
        Time.timeScale = 1f;
        SceneManager.LoadScene(nextScene);
    }
}

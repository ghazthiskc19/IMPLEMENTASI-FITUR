using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using System.Collections;
public class TransitionScript : MonoBehaviour
{
    public static TransitionScript instance;
    public GameObject panelIn;
    public GameObject panelOut;
    public float duration;
    void Awake()
    {
        // Perlu buat singleton biar bisa diakses sama button lain.
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }

        // setup panel biar hilang dulu
        panelIn.SetActive(false);
        panelOut.SetActive(false);
    }


    // untuk mencegah memory leak, kalau hancur harus di unsubscribe.
    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;    
    }

    public void TransitionTo(string sceneName)
    {
        StartCoroutine(OnTransitionSceneOutAndLoad(sceneName));
    }

    public IEnumerator OnTransitionSceneOutAndLoad(string sceneName)
    {
        // opsional kalau pause game pake timeScale = 0;
        Time.timeScale = 1f;
        panelOut.SetActive(true);
        panelOut.transform.DOMove(Vector3.zero, duration).SetEase(Ease.OutQuint);
        yield return new WaitForSeconds(duration + 0.5f);

        // load async
        var ao = SceneManager.LoadSceneAsync(sceneName);
        while (!ao.isDone)
            yield return null;

    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        panelIn.SetActive(true);
        panelIn.transform.DOMove(new Vector3(30, 0, 0), duration)
        .SetEase(Ease.OutQuint)
        .OnComplete(() =>
        {
            // Reset Position panelIn
            panelIn.SetActive(false);
            panelIn.transform.DOMove(Vector3.zero, 0);
        });

        // hide panel out
        panelOut.SetActive(false);
        panelOut.transform.DOMove(new Vector3(-30, 0, 0), 0);


    } 
}

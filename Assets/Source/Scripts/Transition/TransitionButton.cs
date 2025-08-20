using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TransitionButton : MonoBehaviour
{
    public string SceneName;
    void Awake()
    {
        var btn = GetComponent<Button>();
        if (btn == null) return;

        btn.onClick.AddListener(() =>
        {
            if (TransitionScript.instance != null)
            {
                TransitionScript.instance.TransitionTo(SceneName);
            }
            else
            {
                Debug.Log("TransitionScript not found");
            }
        });
    }

    void OnDestroy()
    {
        var btn = GetComponent<Button>();
        if (btn != null) btn.onClick.RemoveAllListeners();
    } 


}

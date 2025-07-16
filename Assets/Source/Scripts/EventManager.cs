using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    public Action<float> action;
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public void OnActionEvent(float damageAmount)
    {
        action.Invoke(damageAmount);
    }

}

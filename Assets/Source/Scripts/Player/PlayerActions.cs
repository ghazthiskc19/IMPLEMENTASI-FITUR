using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerActions : MonoBehaviour
{
    [Header("Actions Slots")]
    private PlayerAttack playerAttack;
    private Animator animator;
    private float time;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
    }
    void Update()
    {
        time += Time.deltaTime;
    }



}

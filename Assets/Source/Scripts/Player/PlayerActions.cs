using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerActions : MonoBehaviour
{
    [Header("Actions Slots")]
    public PlayerAttack playerAttack;
    private PlayerMovement playerMovement;
    private Animator animator;
    private float time;
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        playerAttack = GetComponent<PlayerAttack>();
        playerMovement = GetComponent<PlayerMovement>();
    }
    void Update()
    {
        time += Time.deltaTime;
    }
    public void OnAttack(InputValue input)
    {
        if (time >= playerAttack.duration)
        {
            animator.SetTrigger("BasicAttack");
            time = 0;
        }
    }

    public void OnMove(InputValue input)
    {
        float direction = input.Get<float>();
        if (playerMovement != null)
        {
            playerMovement.SetMoveDirection(direction);
        }
    }
}

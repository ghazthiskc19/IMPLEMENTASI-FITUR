using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float delayAttack = 3f;
    public float bulletSpeed = 5f;
    public GameObject bullet;
    public float time;
    private bool isAttacking;
    void Start()
    {

    }
    public void OnAttack(InputValue input)
    {
        if (!isAttacking)
        {
            StartCoroutine(DelayedAttack());
        }
    }
    private IEnumerator DelayedAttack()
    {
        isAttacking = true;
        LaunchAttack();
        yield return new WaitForSeconds(delayAttack);
        isAttacking = false;
    }
    private void LaunchAttack()
    {
        GameObject j = Instantiate(bullet, transform.position, transform.rotation);
        Rigidbody2D rigidbody2D = j.GetComponent<Rigidbody2D>();
        rigidbody2D.linearVelocity = bulletSpeed * transform.up;
    }

}

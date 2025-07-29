using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public float delayAttack = 1f;
    public float bulletSpeed = 3f;
    public GameObject bullet;
    public float time;
    private bool _canAttack = true;
    private bool isAttacking;
    private float defaultBulletSpeed = 5f;
    private float defaultDelaySpeed = 3f;
    private PlayerHealth _playerHealth;
    void Start()
    {
        _playerHealth = GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDied += HandleDeath;

    }
    public void OnAttack(InputValue input)
    {
        if (!_canAttack) return;
        if (!isAttacking)
            StartCoroutine(DelayedAttack());
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
    public void ChangeAttackSpeed(float speedUpMultiplier)
    {
        bulletSpeed *= speedUpMultiplier;
    }
    public void ChangeReloadSpeed(float speedUpMultiplier)
    {
        delayAttack /= speedUpMultiplier;
    }
    public void ResetBulletSpeed()
    {
        bulletSpeed = defaultBulletSpeed;
    }
    public void ResetReloadSpeed()
    {
        delayAttack = defaultDelaySpeed;
    }
    public void HandleDeath()
    {
        _canAttack = false;
    }

}

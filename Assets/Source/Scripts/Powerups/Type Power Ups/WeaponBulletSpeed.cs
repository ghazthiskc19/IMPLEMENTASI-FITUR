using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Weapon/Bullet Speed")]
public class WeaponBulletSpeed : PowerUpEffect
{
    public float speedUpMultiplier = 2f;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerAttack>().ChangeAttackSpeed(speedUpMultiplier);
    }
    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerAttack>().ResetBulletSpeed();
    }
}

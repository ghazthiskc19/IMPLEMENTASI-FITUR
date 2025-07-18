using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Weapon/Bullet Speed")]
public class WeaponBulletSpeed : PowerUpEffect
{
    public float speedUpMultiplier = 1.5f;
    public float duration = 5f;
    public override void Apply(GameObject target)
    {

    }
    public override void Remove(GameObject target)
    {
        
    }
}

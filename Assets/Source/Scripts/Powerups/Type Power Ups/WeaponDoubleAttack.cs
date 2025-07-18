using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Weapon/Double Attack")]
public class WeaponDoubleAttack : PowerUpEffect
{
    public new float duration = 5f;
    public override void Apply(GameObject target)
    {
    }

    public override void Remove(GameObject target)
    {
    }
}

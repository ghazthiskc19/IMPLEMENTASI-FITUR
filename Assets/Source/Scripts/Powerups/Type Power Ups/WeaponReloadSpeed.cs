using UnityEngine;

[CreateAssetMenu(menuName = "Power Ups/Weapon/Reload Speed")]
public class WeaponReloadSpeed : PowerUpEffect
{
    public float speedUpMultiplier = 2.5f;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerAttack>().ChangeReloadSpeed(speedUpMultiplier);
    }

    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerAttack>().ResetReloadSpeed();
    }
}

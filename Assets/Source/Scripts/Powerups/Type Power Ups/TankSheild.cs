using UnityEngine;
[CreateAssetMenu(menuName = "Power Ups/Tank/Sheild")]
public class TankSheild : PowerUpEffect
{
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerHealth>().ApplySheild();
    }
    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerHealth>().RemoveSheild();
    }
}

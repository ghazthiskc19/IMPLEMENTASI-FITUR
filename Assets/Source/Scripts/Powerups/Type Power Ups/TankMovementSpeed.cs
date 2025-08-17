using UnityEngine;
[CreateAssetMenu(menuName = "Power Ups/Tank/Movement Speed")]
public class TankMovementSpeed : PowerUpEffect
{
    public float speedUpMultiplier = 1.5f;
    public override void Apply(GameObject target)
    {
        target.GetComponent<PlayerMovement>().ChangeSpeed(speedUpMultiplier);
    }
    public override void Remove(GameObject target)
    {
        target.GetComponent<PlayerMovement>().OriginalSpeed();
    }
}

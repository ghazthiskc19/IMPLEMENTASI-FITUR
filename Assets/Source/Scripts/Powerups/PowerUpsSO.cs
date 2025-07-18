using UnityEngine;

public abstract class PowerUpEffect : ScriptableObject
{
    [Header("Kategori Power-ups")]
    public PowerUpsCategory powerUpsCategory;
    public float duration;
    public abstract void Apply(GameObject target);
    public abstract void Remove(GameObject target);
}

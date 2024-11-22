using UnityEngine;

public struct DamageEvent
{
    public int damage;
    public Vector3 source;
    public DamageEvent(int damage, Vector3 source)
    {
        this.damage = damage;
        this.source = source;
    }
}

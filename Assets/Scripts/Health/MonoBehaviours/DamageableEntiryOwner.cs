using Leopotam.Ecs;

public abstract class DamageableEntiryOwner : EntityOwner
{
    public void TakeDamage(int damage)
    {
        entity.Replace(new DamageEvent(damage));
    }
}

using Leopotam.Ecs;
using UnityEngine;

public class WeaponCastSystem : IEcsRunSystem
{
    private EcsFilter<ShootCastEvent> shootFilter;
    private EcsFilter<ExplosionCastEvent> explosionFilter;

    public void Run()
    {
        foreach (var sht in shootFilter)
        {
            Debug.Log("WeaponCastSystem. shootFilter");
            ref var shoot = ref shootFilter.Get1(sht);

            if (Physics.Raycast(shoot.origin, shoot.direction, out RaycastHit hit, shoot.distance, shoot.mask))
            {
                Debug.Log("WeaponCastSystem: " + hit.collider.name);
                Vector3 point = hit.point;
                Quaternion rotation = Quaternion.LookRotation(hit.normal);
                var impact = ObjectPoolManager.Instance.GetObject("StoneImpact");
                impact.transform.position = point;
                impact.transform.rotation = rotation;

                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * shoot.force, ForceMode.Impulse);
                }
            }
        }
        foreach (var exp in explosionFilter)
        {
            ref var explosion = ref explosionFilter.Get1(exp);
            Collider[] colliders = Physics.OverlapSphere(explosion.origin, explosion.radius);

            foreach (var collider in colliders)
            {
                Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    Vector3 direction = collider.transform.position - explosion.origin;
                    rb.AddForce(direction.normalized * explosion.force, ForceMode.Impulse);
                }

                var entityOwner = collider.gameObject.GetComponent<EntityOwner>();
                if (entityOwner != null)
                {
                    entityOwner.entity.Replace(new DamageEvent());
                }
            }
        }
    }

}
public struct ShootCastEvent
{
    public Vector3 origin;
    public Vector3 direction;
    public float distance;
    public float force;
    public int damage;
    public LayerMask mask;

    public ShootCastEvent(Vector3 origin, Vector3 direction, float distance, float force, int damage, LayerMask mask)
    {
        this.origin = origin;
        this.direction = direction;
        this.distance = distance;
        this.mask = mask;
        this.force = force;
        this.damage = damage;
    }
}
public struct ExplosionCastEvent
{
    public Vector3 origin;
    public float radius;
    public LayerMask mask;
    public float force;
}

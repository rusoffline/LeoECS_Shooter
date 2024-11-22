using Leopotam.Ecs;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponService
{
    public void ShootCast(Vector3 origin, Vector3 direction, float distance, float force, int damage, LayerMask mask)
    {
        if (Physics.Raycast(origin, direction, out RaycastHit hit, distance, mask))
        {
            Debug.Log("WeaponCastSystem: " + hit.collider.name);
            Vector3 point = hit.point;
            Quaternion rotation = Quaternion.LookRotation(hit.normal);
            var impact = ObjectPoolManager.Instance.GetObject("StoneImpact");
            impact.transform.position = point;
            impact.transform.rotation = rotation;

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * force, ForceMode.Impulse);
            }
            TryApplyDamage(hit.collider.gameObject, new DamageEvent(damage, origin));
        }
    }

    public bool MeleeCast(Vector3 origin, float radius, int damage, LayerMask mask)
    {
        Collider[] colliders = Physics.OverlapSphere(origin, radius, mask);
        foreach (Collider collider in colliders)
        {
            TryApplyDamage(collider.gameObject, new DamageEvent(damage, origin));
            Debug.Log($"WeaponService. MeleeCast. collider hit = {collider.transform.name}");

            Vector3 contactPoint = collider.ClosestPoint(origin);
            var impact = ObjectPoolManager.Instance.GetObject("SparkImpact");
            impact.transform.position = contactPoint;
            impact.transform.rotation = Quaternion.LookRotation(origin - contactPoint);
        }
        return (colliders != null && colliders.Length > 0);
    }

    public void ExplosionCast(Vector3 origin, float radius, float force, int damage, LayerMask mask)
    {
        Collider[] colliders = Physics.OverlapSphere(origin, radius, mask);

        foreach (var collider in colliders)
        {
            Rigidbody rb = collider.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direction = collider.transform.position - origin;
                rb.AddForce(direction.normalized * force, ForceMode.Impulse);
            }

            TryApplyDamage(collider.gameObject, new DamageEvent(damage, origin));
        }
    }

    private void TryApplyDamage(GameObject gameObject, DamageEvent damage)
    {
        var damageable = gameObject.GetComponentInParent<DamageReceiver>();
        if (damageable != null)
        {
            damageable.ApplyDamage(damage);
            Debug.Log("I found Damage reciver and applay damage!");
        }
    }
}

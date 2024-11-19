using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour
{
    private string tagName;
    public virtual void OnSpawn(string tagName)
    {
        this.tagName = tagName;
    }

    public virtual void OnDespawn()
    {
        ReturnToPool();
    }

    public void ReturnToPool()
    {
        ObjectPoolManager.Instance.ReturnObject(tagName, this);
    }
}

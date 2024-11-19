using System.Collections.Generic;
using UnityEngine;

public partial class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance;

    [SerializeField] private List<Pool> pools; 
    private Dictionary<string, Queue<PoolableObject>> poolDictionary; 

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        poolDictionary = new Dictionary<string, Queue<PoolableObject>>();
        InitializePools();
    }

    private void InitializePools()
    {
        foreach (Pool pool in pools)
        {
            Queue<PoolableObject> objectQueue = new Queue<PoolableObject>();

            for (int i = 0; i < pool.initialSize; i++)
            {
                PoolableObject obj = Instantiate(pool.poolablePrefab);
                obj.gameObject.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(pool.tag, objectQueue);
        }
    }

    public PoolableObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist!");
            return null;
        }

        PoolableObject obj;
        if (poolDictionary[tag].Count > 0)
        {
            obj = poolDictionary[tag].Dequeue();
        }
        else
        {
            // Если пул пуст, создаем новый объект
            Pool pool = pools.Find(p => p.tag == tag);
            obj = Instantiate(pool.poolablePrefab);
        }

        obj.gameObject.SetActive(true);
        obj.OnSpawn(tag);
        return obj;
    }

    public void ReturnObject(string tag, PoolableObject obj)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning($"Pool with tag {tag} doesn't exist!");
            Destroy(obj);
            return;
        }

        obj.gameObject.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}

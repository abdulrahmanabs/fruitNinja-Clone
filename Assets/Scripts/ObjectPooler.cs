using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler instance;
    public GameObject objectToPool;
    public int poolSize;

    private List<GameObject> pooledObjects;

    void Awake()
    {
        pooledObjects = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(objectToPool);
            obj.SetActive(false);
            pooledObjects.Add(obj);
        }
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjects.Count; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }
        return null;
    }
    public void DisableAll()
    {
        foreach(var i in pooledObjects)
        {
            i.gameObject.SetActive(false);
        }
    }
    // You can add more functions like ResetPooledObject if needed
}
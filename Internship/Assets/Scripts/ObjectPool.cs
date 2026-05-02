using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    public static ObjectPool instance;

    public static ObjectPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ObjectPool();
            }
            return instance;
        }
    }

    public Dictionary<string, Queue<GameObject>> objectPool = new Dictionary<string, Queue<GameObject>>();

    public GameObject pool = new GameObject("Pool");

    public GameObject GetObject(GameObject prefab)
    {
        GameObject _Object;

        if (!objectPool.ContainsKey(prefab.name) || objectPool[prefab.name].Count == 0)
        {
            _Object = GameObject.Instantiate(prefab);
            PushObject(_Object);

            GameObject childPool = GameObject.Find(prefab.name + "Pool");
            if (childPool == null)
            {
                childPool = new GameObject(prefab.name + "Pool");
                childPool.transform.SetParent(pool.transform);
            }
            _Object.transform.SetParent(childPool.transform);
        }

        _Object = objectPool[prefab.name].Dequeue();
        _Object.SetActive(true);
        return _Object;
    }

    public void PushObject(GameObject _Object)
    {
        string _name = _Object.name.Replace("(Clone)", string.Empty);
        if (!objectPool.ContainsKey(_name))
        {
            objectPool.Add(_name, new Queue<GameObject>());
        }
        _Object.SetActive(false);
        objectPool[_name].Enqueue(_Object);
    }
}


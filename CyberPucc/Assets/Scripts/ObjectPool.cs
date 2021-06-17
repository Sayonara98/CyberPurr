using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField]
    private GameObject ObjectToPool;
    [SerializeField]
    private float poolSize = 8;

    private List<GameObject> poolObjects;

    void Start()
    {
        poolObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < poolSize; i++)
        {
            tmp = Instantiate(ObjectToPool);
            tmp.SetActive(false);
            poolObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        bool expand = true;
        for (int i = 0; i < poolSize; i++)
        {
            if (poolObjects[i].activeInHierarchy == false)
            {
                expand = false;
                return poolObjects[i];
            }
        }

        if (expand)
        {
            GameObject obj = Instantiate(ObjectToPool);
            obj.SetActive(false);
            poolObjects.Add(obj);
            return obj;
        }

        return null;
    }


}

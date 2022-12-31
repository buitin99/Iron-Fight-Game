using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPools : MonoBehaviour
{
    public static ObjectPools instance;

    private List<GameObject> pooledObjets = new List<GameObject>();
    private int amountToPool = 20;
    [SerializeField]
    private GameObject bulletPrefab;

    private void Awake() 
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            GameObject obj = Instantiate(bulletPrefab);
            obj.SetActive(false);
            pooledObjets.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < pooledObjets.Count; i++)
        {
            if (!pooledObjets[i].activeInHierarchy)
            {
                return pooledObjets[i];
            }
        }

        return null;
    }
}

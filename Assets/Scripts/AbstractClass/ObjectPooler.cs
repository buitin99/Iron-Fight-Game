using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : Singleton<ObjectPooler>
{
    [System.Serializable]
    public class ObjectPrefab {
        public int size;    
        public string key;
        public GameObject prefab;
  
        public int active, inactive;
        public Queue<GameObject> objectPool = new Queue<GameObject>();
    }
    public ObjectPrefab[] objectPrefabs;
    private Dictionary<string, ObjectPrefab> dic = new Dictionary<string, ObjectPrefab>();
    protected override void Awake()
    {
        base.Awake();
    }
    private void Start() {
        foreach(ObjectPrefab objectPrefab in objectPrefabs ) {
            dic.Add(objectPrefab.key, objectPrefab);
            for(int i = 0; i< objectPrefab.size; i++) {
                GameObject obj = Instantiate(objectPrefab.prefab);
                obj.transform.SetParent(transform);
                obj.SetActive(false);
                objectPrefab.inactive ++;
                objectPrefab.objectPool.Enqueue(obj);
            }
        }
    }

    public GameObject SpawnObject(string key, Vector3 position, Quaternion rotation) {
        ObjectPrefab objectPrefab = dic[key];
        GameObject obj;
        if(objectPrefab.inactive <= 0) {
            obj = Instantiate(objectPrefab.prefab, position, rotation);
            obj.transform.SetParent(transform);
            obj.SetActive(true);
            objectPrefab.active ++;
            objectPrefab.objectPool.Enqueue(obj);
            objectPrefab.size ++;
        } else {
            obj = objectPrefab.objectPool.Dequeue();
            Transform objTrans = obj.transform;
            objTrans.position = position;
            objTrans.rotation = rotation;
            obj.SetActive(true);
            objectPrefab.active ++;
            objectPrefab.inactive --;
            objectPrefab.objectPool.Enqueue(obj);
        }

        return obj;
    }

    public void InactiveObject(string key, GameObject obj) {
        if(obj.activeSelf) {
            ObjectPrefab objectPrefab = dic[key];
            obj.SetActive(false);
            objectPrefab.inactive ++;
            objectPrefab.active --;
        }
    }

    public void ResetObjectPooler() {

        for(int i = 0; i < transform.childCount; i ++) {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        foreach(ObjectPrefab objectPrefab in objectPrefabs ) {
            for(int i = 0; i< objectPrefab.size; i++) {
                objectPrefab.inactive = objectPrefab.size;
                objectPrefab.active = 0;
            }
        }
    }

}

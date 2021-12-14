using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool<T> {
    private List<GameObject> gameObjectPool = new List<GameObject>();
    private GameObject prefab;
    private bool isGameObject;

    public Pool(GameObject _prefab) {
        prefab = _prefab;
        isGameObject = false;
    }

    public Pool(GameObject _prefab, bool _isGameObject) {
        prefab = _prefab;
        isGameObject = _isGameObject;
    }

    public T GetOrCreate() {
        return GetOrCreateGameObject().GetComponent<T>();
    }

    public GameObject GetOrCreateGameObject() {
        GameObject obj = gameObjectPool.Find(m => !m.activeSelf);
        if (obj == null) {
            obj = GameObject.Instantiate(prefab);
            gameObjectPool.Add(obj);
        }
        return obj;
    }
}

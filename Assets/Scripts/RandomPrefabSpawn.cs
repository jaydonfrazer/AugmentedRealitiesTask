using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPrefabSpawn : MonoBehaviour
{
    public List<GameObject> Prefabs;
    private GameObject ObjectToSpawn;
    private int prefabIndex;

    void Start()
    {
        SpawnObject();
    }

    void SpawnObject()
    {
        prefabIndex = Random.Range(0, Prefabs.Count);
        ObjectToSpawn = Prefabs[prefabIndex];
        GameObject newObject = Instantiate(ObjectToSpawn, gameObject.transform);
    }
}

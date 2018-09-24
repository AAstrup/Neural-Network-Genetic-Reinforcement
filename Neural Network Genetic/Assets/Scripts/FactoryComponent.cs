using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnPointCollection))]
public class FactoryComponent : MonoBehaviour {

    public delegate void GameObjectSpawnedEvent(GameObject spawn);
    public GameObjectSpawnedEvent gameObjectSpawnedEvent;
    public GameObject prefabToSpawn;
    private SpawnPointCollection m_SpawnPointCollection;

    public int spawnAmount;

    private void Start()
    {
        m_SpawnPointCollection = GetComponent<SpawnPointCollection>();
    }

    public void SpawnAll()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            var gmj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            if (gameObjectSpawnedEvent != null)
                gameObjectSpawnedEvent(gmj);
        }
    }

    internal void SpawnSingle()
    {
        var gmj = Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
        if (gameObjectSpawnedEvent != null)
            gameObjectSpawnedEvent(gmj);
    }
}

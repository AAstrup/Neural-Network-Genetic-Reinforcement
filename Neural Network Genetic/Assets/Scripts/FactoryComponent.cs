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
    [SerializeField]
    private Vector3 m_SpawnArea;

    private void Start()
    {
        m_SpawnPointCollection = GetComponent<SpawnPointCollection>();
    }

    public void SpawnAll()
    {
        for (int i = 0; i < spawnAmount; i++)
        {
            SpawnSingle();
        }
    }

    internal void SpawnSingle()
    {
        var gmj = Instantiate(prefabToSpawn, GetSpawnPosition(), Quaternion.identity);
        if (gameObjectSpawnedEvent != null)
            gameObjectSpawnedEvent(gmj);
    }

    private Vector3 GetSpawnPosition()
    {
        return transform.position + new Vector3(m_SpawnArea.x * Random.Range(-1f, 1f), m_SpawnArea.y * Random.Range(-1f, 1f), m_SpawnArea.z * Random.Range(-1f, 1f)) * 0.5f;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, m_SpawnArea);
    }
}

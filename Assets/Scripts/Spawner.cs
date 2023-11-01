using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    [SerializeField] GameObject[] ObjectsToSpawn;
    [SerializeField] GameObject Bomb;
    [SerializeField] float minAngle = -15f, maxAngle = 15f;
    [SerializeField] float minSpawnDelay = 0.25f, maxSpawnDelay = 1f;
    [SerializeField] float minForce = 50, maxForce = 100;
    [SerializeField] float bombSpawnProbability = 0.05f;
    List<GameObject> friutsSpawned = new List<GameObject>();
    public static Spawner Instance { get; private set; }


    void Awake()
    {
        spawnArea = GetComponent<Collider>();
        if(Instance!=null)
        Destroy(gameObject);
        else
        Instance=this;
    }

    void OnEnable()
    {
        StartCoroutine(SpawnObjects());
    }
    void OnDisable()
    {
        StopCoroutine(SpawnObjects());
    }

    public void ClearAllObjects()
    {
        foreach (var i in friutsSpawned)
        {
            Destroy(i);
        }
    }
    IEnumerator SpawnObjects()
    {
        while (GameManager.Instance.gameState == GameManager.GameState.Play)
        {
            Vector3 spawnPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));
            float randomForce = Random.Range(minForce, maxForce);
            if (Random.value <= bombSpawnProbability)
            {
                GameObject bomb = Instantiate(Bomb, spawnPos, rotation);
                bomb?.GetComponent<Rigidbody>().AddForce(bomb.transform.up * 25, ForceMode.Impulse);
                friutsSpawned.Add(bomb);

            }
            else
            {
                GameObject fruit = Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)], spawnPos, rotation);
                fruit?.GetComponent<Rigidbody>().AddForce(fruit.transform.up * randomForce, ForceMode.Impulse);
                friutsSpawned.Add(fruit);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}

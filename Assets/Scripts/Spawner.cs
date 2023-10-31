using System.Collections;
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


    void Awake()
    {
        spawnArea = GetComponent<Collider>();
    }

    void OnEnable()
    {
        StartCoroutine(SpawnObjects());
    }
    void OnDisable()
    {
        StopCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (GameManager.instance.gameState == GameManager.GameState.Play)
        {
            Vector3 spawnPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x), transform.position.y, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));
            float randomForce = Random.Range(minForce, maxForce);
            if (Random.value <= bombSpawnProbability)
            {
                GameObject bomb = Instantiate(Bomb, spawnPos, rotation);
                bomb?.GetComponent<Rigidbody>().AddForce(bomb.transform.up * randomForce, ForceMode.Impulse);

            }
            else
            {
                GameObject fruit = Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)], spawnPos, rotation);
                fruit?.GetComponent<Rigidbody>().AddForce(fruit.transform.up * randomForce, ForceMode.Impulse);
            }

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}

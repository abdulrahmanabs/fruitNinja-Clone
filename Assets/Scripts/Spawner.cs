using System.Collections;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    private Collider spawnArea;
    [SerializeField] GameObject[] ObjectsToSpawn;
    [SerializeField] float minAngle = -15f, maxAngle = 15f;
    [SerializeField] float objectLifeTime = 5f;
    [SerializeField] float minSpawnDelay = 0.25f, maxSpawnDelay = 1f;
    [SerializeField] float minForce = 50, maxForce = 100;


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
        while (enabled)
        {
            Vector3 spawnPos = new Vector3(Random.Range(spawnArea.bounds.min.x, spawnArea.bounds.max.x),transform.position.y, Random.Range(spawnArea.bounds.min.z, spawnArea.bounds.max.z));
            Quaternion rotation = Quaternion.Euler(0, 0, Random.Range(minAngle, maxAngle));

            GameObject fruit = Instantiate(ObjectsToSpawn[Random.Range(0, ObjectsToSpawn.Length)], spawnPos, rotation);
            float randomForce = Random.Range(minForce, maxForce);
            fruit?.GetComponent<Rigidbody>().AddForce(fruit.transform.up * randomForce, ForceMode.Impulse);

            yield return new WaitForSeconds(Random.Range(minSpawnDelay, maxSpawnDelay));
        }
    }
}

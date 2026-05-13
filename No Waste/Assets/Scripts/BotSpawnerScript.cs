using UnityEngine;

public class BotSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab_1;
    [SerializeField] private GameObject enemyPrefab_2;
    [SerializeField] private GameObject enemyPrefab_3;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 5.0f;
    private float nextSpawnTime;
    private int number;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        number = Random.Range(0, 3);
    }

    // Update is called once per frame
    void Update()
    {
        number = Random.Range(0, 3);
        switch (number)
        {
            case 0:
                {
                    enemyPrefab = enemyPrefab_1;
                    break;
                }
            case 1:
                {
                    enemyPrefab = enemyPrefab_2;
                    break;
                }
            default:
                {
                    enemyPrefab = enemyPrefab_3;
                    break;
                }
        }
        if (Time.time >= nextSpawnTime)
        {
            SpawnEnemy();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnEnemy()
    {
        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

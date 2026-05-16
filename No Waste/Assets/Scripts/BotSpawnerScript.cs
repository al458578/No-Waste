using UnityEngine;

public class BotSpawnerScript : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab_1;
    [SerializeField] private GameObject enemyPrefab_2;
    [SerializeField] private GameObject enemyPrefab_3;
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float spawnRate = 2.0f;
    private float nextSpawnTime;
    private int number;

    private float elapsedTime = 0f;
    public int timeCooldown;
    public int duration = 8;
    private bool isRaid = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        number = Random.Range(0, 3);
        timeCooldown = Random.Range(40, 61);
    }

    // Update is called once per frame
    void Update()
    {
        if (isRaid)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f && duration > 0)
            {
                duration--;
                elapsedTime = 0f;

                if (Time.time >= nextSpawnTime)
                {
                    SpawnEnemy();
                    nextSpawnTime = Time.time + spawnRate;
                }

            }
            else if (duration == 0)
            {
                isRaid = false;
                timeCooldown = Random.Range(40, 61);
            }
        }

        else if (isRaid == false)
        {
            elapsedTime += Time.deltaTime;
            if (elapsedTime >= 1f && timeCooldown > 0)
            {
                timeCooldown--;
                elapsedTime = 0f;
            }

            else if (timeCooldown == 0)
            {
                isRaid = true;
                duration = 8;
            }
        }
    }

    void SpawnEnemy()
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

        Vector3 randomOffset = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);
    }
}

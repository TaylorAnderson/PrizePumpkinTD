using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
public class Anthill : MonoBehaviour
{
    public float delay;
    public GameObject enemyPrefab;
    public GameObject redEnemyPrefab;
    public GameObject blueEnemyPrefab;

    public List<GameObject> enemies = new List<GameObject>();

    public float spawnInterval = 1.0f;

    private float durationTimer = 0.0f;
    private float antSpawnTimer = 0.0f;

    private float delayTimer = 0.0f;

    private EnemySpawnBehaviour currentSpawnBehaviour;

    public bool currentlySpawningWave = false;

    public bool logging = false;

    public Grid grid;
    public Tilemap flowmap;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() {
        if (currentlySpawningWave) {
            delayTimer -= Time.deltaTime;
            if (logging) {
                print(delayTimer);
            }
            if (delayTimer < 0) {
                durationTimer += Time.deltaTime;

                antSpawnTimer += Time.deltaTime;

                if (logging) {
                    print(antSpawnTimer);
                }
                if (antSpawnTimer >= 1/currentSpawnBehaviour.antRate) {
                    antSpawnTimer = 0;
                    SpawnEnemy(currentSpawnBehaviour.antType);
                }
    
                if (durationTimer > currentSpawnBehaviour.duration+0.1f) {
                    durationTimer = 0;
                    antSpawnTimer = 0;
                    delayTimer = 0;
                    currentlySpawningWave = false;
                }
            }
        }
    }

    void SpawnEnemy(AntType type) {
        GameObject enemy = null;
        if (type == AntType.BIG) {
            enemy = Instantiate(blueEnemyPrefab);
        }
        if (type == AntType.SMALL) {
            enemy = Instantiate(redEnemyPrefab);
        }
        if (type == AntType.MEDIUM) {
            enemy = Instantiate(enemyPrefab);
        }
        enemies.Add(enemy);
        var enemyComp = enemy.GetComponent<Enemy>();
        enemyComp.flowmap = flowmap;
        enemyComp.grid = grid;
        enemyComp.OnDeath.AddListener(() => {GameManager.instance.OnEnemyKilled(enemyComp);});
        enemyComp.OnDeath.AddListener(() => {OnEnemyDead(enemy);});
        enemy.transform.position = transform.position;
    }

    public void OnEnemyDead(GameObject enemy) {
        enemies.Remove(enemy);
    }

    public void SpawnEnemies(EnemySpawnBehaviour spawnBehaviour) {
        delayTimer = spawnBehaviour.delay;
        currentlySpawningWave = true;
        currentSpawnBehaviour = spawnBehaviour;
    }   
}

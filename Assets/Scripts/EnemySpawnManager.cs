using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemySpawnManager : MonoBehaviour
{
    public List<Anthill> anthills;
    public List<EnemyWave> enemyWaves;
    private int currentWaveIndex = 0;
    public float delayBeforeFirstWave = 5.0f;
    public float delayBetweenWaves = 10.0f;

    private float currentDelay;

    private float delayTimer = 0;

    private bool currentlySpawningWave = false;

    public UnityEvent OnGameDone;
    // Start is called before the first frame update
    void Start()
    {
        currentDelay = delayBeforeFirstWave;
    }

    // Update is called once per frame
    void Update()
    {
        delayTimer += Time.deltaTime;
        if (delayTimer > currentDelay && currentWaveIndex < enemyWaves.Count) {
            SpawnWave(enemyWaves[currentWaveIndex]);
            delayTimer = 0;
        }

        if (currentlySpawningWave) {
            delayTimer = 0;
            var doneWave = true;
            foreach (var hill in anthills) {
                if (hill.currentlySpawningWave || hill.enemies.Count > 0) {
                    doneWave = false;
                }
            }
            if (doneWave) {
                print("done wave");
                currentlySpawningWave = false;
                currentDelay = delayBetweenWaves;
                delayTimer = 0;
                if (currentWaveIndex >= enemyWaves.Count) {
                    OnGameDone.Invoke();
                }
            }
        }
    }

    void SpawnWave(EnemyWave wave) {
        for (int i = 0; i < anthills.Count; i++) {
            if (wave.anthills.Count > i) {
                anthills[i].SpawnEnemies(wave.anthills[i]);
            }
        }
        currentDelay = delayBetweenWaves;
        delayTimer = 0;
        currentWaveIndex += 1;
        currentlySpawningWave = true;
    }
}

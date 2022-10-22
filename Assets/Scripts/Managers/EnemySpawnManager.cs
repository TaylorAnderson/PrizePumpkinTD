using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;

public enum EnemySpawnState {
    BEGINNING_OF_GAME,
    SPAWNING_ENEMIES,
    BETWEEN_WAVES
}
public class EnemySpawnManager : MonoBehaviour
{
    public List<EnemyWave> enemyWaves;
    private int currentWaveIndex = 0;
    public float delayBeforeFirstWave = 5.0f;
    public float delayBetweenWaves = 10f;

    private float currentDelay;

    private float delayTimer = 0;

    private bool currentlySpawningWave = false;
    private bool gameLost = false;

    public UnityEvent OnGameDone;
    public UnityEvent<int> OnWaveDone;
    private bool invokedWaveEvent = false;
    public float timeForWave = 20f;
    public EnemySpawnState state;
    

    public SuperTextMesh nextWaveTimerText;
    public SuperTextMesh waveCountText;
    // Start is called before the first frame update
    void Start()
    {
        delayBeforeFirstWave /= GameManager.instance.gameSpeedMultiplier;
        delayBetweenWaves /= GameManager.instance.gameSpeedMultiplier;
        timeForWave /= GameManager.instance.gameSpeedMultiplier;

        currentDelay = delayBeforeFirstWave;
        //LevelManager.instance.onPumpkinDead.AddListener()
        SetAntPreviews(0);
        LevelManager.instance.onPumpkinDead.AddListener(OnPumpkinDead);
    }

    // Update is called once per frame
    void Update()
    {

        if (state == EnemySpawnState.BEGINNING_OF_GAME) {
            delayTimer += Time.deltaTime;
            if (delayTimer > delayBeforeFirstWave) {
                ChangeState(EnemySpawnState.SPAWNING_ENEMIES);
            }    
            var span = TimeSpan.FromSeconds(delayBeforeFirstWave - delayTimer);
            nextWaveTimerText.text = span.ToString("ss\\:ff");
        }
        if (state == EnemySpawnState.SPAWNING_ENEMIES) {
            bool doneSpawning = true;
            foreach (Anthill a in LevelManager.instance.anthills) {
                if (a.currentlySpawningWave) {
                    doneSpawning = false;
                }
            }
            if (doneSpawning) {
                ChangeState(EnemySpawnState.BETWEEN_WAVES);
            }
            nextWaveTimerText.text = "Soon";
        }
        if (state == EnemySpawnState.BETWEEN_WAVES) {
            delayTimer += Time.deltaTime;
            if (delayTimer > timeForWave) {
                ChangeState(EnemySpawnState.SPAWNING_ENEMIES);
            }
            var span = TimeSpan.FromSeconds(timeForWave - delayTimer);
            nextWaveTimerText.text = span.ToString("ss\\:ff");
        }
    }

    void ChangeState(EnemySpawnState newState) {
        delayTimer = 0;
        state = newState;
        if (newState == EnemySpawnState.BEGINNING_OF_GAME) {

        }
        if (newState == EnemySpawnState.SPAWNING_ENEMIES) {
            waveCountText.text = "Wave " + currentWaveIndex + 1;
            if (currentWaveIndex > enemyWaves.Count - 1) {
                OnGameDone.Invoke();
                return;
            }
            SpawnWave(enemyWaves[currentWaveIndex]);
        }
        if (newState == EnemySpawnState.BETWEEN_WAVES) {
            OnWaveDone.Invoke(currentWaveIndex);
            currentWaveIndex ++;
        }
    }

    void SpawnWave(EnemyWave wave) {
        for (int i = 0; i < LevelManager.instance.anthills.Count; i++) {
            if (wave.anthills.Count > i) {
                LevelManager.instance.anthills[i].SpawnEnemies(wave.anthills[i]);
            }
        }
        currentDelay = delayBetweenWaves;
        delayTimer = 0;
        currentWaveIndex += 1;
        currentlySpawningWave = true;
        invokedWaveEvent = false;
    }

    void SetAntPreviews(int index) {
        var nextWave = enemyWaves[index];
        for (int i = 0; i < nextWave.anthills.Count; i++) {
            if (nextWave.anthills[i].duration > 0) {
                LevelManager.instance.anthills[i].SetNextEnemy(nextWave.anthills[i].antType);
            }
        }
    }

    void OnPumpkinDead() {
        gameLost = true;
    }
}

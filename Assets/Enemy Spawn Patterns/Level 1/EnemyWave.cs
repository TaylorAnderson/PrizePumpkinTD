using System;
using System.Collections.Generic;
using UnityEngine;

public enum AntType {
    BIG,
    MEDIUM,
    SMALL
}
[Serializable]
public class EnemySpawnBehaviour {
    [Tooltip("Wait time before spawning ants")]
    public float delay;

    [Tooltip("The type of ant we're spawning")]
    public AntType antType;

    [Tooltip("How long we're spawning ants for")]
    public float duration;

    [Tooltip("Ants to spawn per second")]
    public float antRate;
}
[CreateAssetMenu]
public class EnemyWave : ScriptableObject
{
    public List<EnemySpawnBehaviour> anthills;
}

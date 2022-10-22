using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantFlower : PlantBehaviour
{
    public GameObject poisonGasPrefab;

    public float gasInterval = 3.0f;

    private float gasTimer = 1.5f;

    public override void Start() {
        base.Start();
        gasInterval /= GameManager.instance.gameSpeedMultiplier;
    }
    // Update is called once per frame
    override public void Update()
    {
        base.Update();
        if (placed) {
            gasTimer += Time.deltaTime;
            if (gasTimer > gasInterval) {
                SfxManager.instance.PlaySound(SoundType.FLOWER_ATTACK);
                var gas = Instantiate(poisonGasPrefab);
                gas.transform.position = transform.position;
                gasTimer = 0;
            }
        }
    }
}

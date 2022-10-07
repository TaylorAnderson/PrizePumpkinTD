using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBehaviour : MonoBehaviour
{
    public float cooldown = 1.0f;

    protected float cooldownTimer = -0.1f;
    protected bool onCooldown = false;

    protected bool placed = false;
    
    protected bool canAttack = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public virtual void Update()
    {
        if (placed) {
            cooldownTimer -= Time.deltaTime;
        }
        canAttack = cooldownTimer <= 0 && placed;
    }

    public virtual void OnPlaced() {
        placed = true;
    }
    public void OnActivated() {
        cooldownTimer = cooldown/1.5f;
    }
}

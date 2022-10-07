using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonGas : MonoBehaviour
{
    public float duration = 5.0f;
    public List<Enemy> enemiesInRange = new List<Enemy>();

    private float timer = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        timer += Time.deltaTime;
        if (timer <= duration) {
            foreach (Enemy e in enemiesInRange) {
                e.poisonTimer += Time.deltaTime;
            }
        }
        else {
            var ps = GetComponent<ParticleSystem>();
            if (ps.time >= ps.main.duration) {
                Destroy(gameObject);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            enemiesInRange.Add(col.gameObject.GetComponent<Enemy>());
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            enemiesInRange.Remove(col.gameObject.GetComponent<Enemy>());
        }
    }
}

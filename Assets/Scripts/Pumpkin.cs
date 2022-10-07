using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Pumpkin : MonoBehaviour
{
    private float health = 10;
    public SuperTextMesh text;
    public UnityEvent OnDeath;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D (Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            SfxManager.instance.PlaySound(SoundType.PUMPKIN_HIT);
            health -= 1;
            text.text = health.ToString() + "/10";
            col.gameObject.GetComponent<Enemy>().DealDamage(1000, Vector3.zero);

            if (health <= 0) {
                OnDeath.Invoke();
            }
        }
    }
}

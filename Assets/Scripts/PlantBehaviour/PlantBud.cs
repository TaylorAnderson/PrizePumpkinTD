using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class PlantBud : PlantBehaviour
{
    private Vector3 basePos;

    public float strikeAnimDist = 0.1f;

    public int damage = 5;

    private List<GameObject> enemiesInRange = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
        if (canAttack) {
            if (enemiesInRange.Count > 0) {
                cooldownTimer = cooldown;
                SfxManager.instance.PlaySound(SoundType.BUD_ATTACK, 0.5f);
                var enemy = enemiesInRange[Random.Range(0, enemiesInRange.Count-1)];
                enemy.GetComponent<Enemy>().DealDamage(damage, transform.position);
                var newPos = transform.position + (enemy.transform.position - transform.position).normalized * strikeAnimDist;
                transform.position = newPos;
                transform.DOMove(basePos, 0.2f);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            enemiesInRange.Add(col.gameObject);
        }
    }

    void OnTriggerExit2D (Collider2D col) {
        if (col.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            enemiesInRange.Remove(col.gameObject);
        }
    }

    public override void OnPlaced() {
        print("geting placed");
        base.OnPlaced();
        GetComponent<CircleCollider2D>().enabled = true;
        basePos = transform.position;
    }
}

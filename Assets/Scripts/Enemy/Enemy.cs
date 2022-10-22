using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.Tilemaps;
public class Enemy : MonoBehaviour
{

    public float speed = 100f;

    private Vector2 dir = Vector2.zero;

    public int health = 5;

    public float hurtSpeed = 2;

    public float poisonTimer = 0;

    private Vector3 vel;

    public float poisonInterval = 0.5f;

    public int gold = 2;

    private Color baseColor;

    public UnityEvent OnDeath;
    [HideInInspector]
    public GridLayout grid;
    [HideInInspector]
    public Tilemap flowmap;
    // Start is called before the first frame update
    void Start()
    {
        speed *= GameManager.instance.gameSpeedMultiplier;
        baseColor = GetComponent<SpriteRenderer>().color;
        var flowTile = flowmap.GetTile(grid.WorldToCell(transform.position));
        if (flowTile != null) {
            var lockedPos = flowmap.CellToWorld(grid.WorldToCell(transform.position)) + Vector3.up * 0.5f + Vector3.right * 0.5f;
            transform.position = lockedPos;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var flowTile = flowmap.GetTile(grid.WorldToCell(transform.position));
        if (flowTile != null) {
            var lockedPos = flowmap.CellToWorld(grid.WorldToCell(transform.position)) + Vector3.up * 0.5f + Vector3.right * 0.5f;
            var dist = Vector3.Distance(lockedPos, transform.position);
            var distVec = lockedPos - transform.position;
            if (dist < 0.05f) {
                if (flowTile.name == "down") {
                    dir = Vector2.down;
                }
                if (flowTile.name == "up") {
                    dir = Vector2.up;
                }
                if (flowTile.name == "left") {
                    dir = Vector2.left;
                }
                if (flowTile.name == "right") {
                    dir = Vector2.right;
                }
            }
        }
        vel *= 0.93f;
        vel = dir * (speed * 10 * Time.fixedDeltaTime);
        transform.position += vel;

        if (poisonTimer > poisonInterval) {
            DealDamage(1, Vector3.zero);
            poisonTimer = 0;
        }
        transform.eulerAngles = Vector3.forward * (Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg);
    }

    public void DealDamage(int damage, Vector3 position) {
        SfxManager.instance.PlaySound(SoundType.ENEMY_HIT);
        health -= damage;
        if (position != Vector3.zero) {
            vel += (transform.position - position).normalized * hurtSpeed;
        }
        StartCoroutine(FlashRed());
        if (health <= 0) {
            SfxManager.instance.PlaySound(SoundType.ENEMY_DEATH);
            OnDeath.Invoke();
            Destroy(this.gameObject);
        }
    }
    public IEnumerator FlashRed() {
        GetComponent<SpriteRenderer>().color = Color.white;
        yield return new WaitForSeconds(0.3f);
        GetComponent<SpriteRenderer>().color = baseColor;
    }
}

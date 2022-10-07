using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantLeaf : PlantBehaviour
{
    public GameObject leafProjectilePrefab;
    private Vector2[] dirs = new Vector2[] {Vector2.left, Vector2.right, Vector2.up, Vector2.down};
    public LayerMask layerMask;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public override void Update() {
        base.Update();
        if (canAttack) {
            var leaf_thrown = TryThrowLeaf();
            if (leaf_thrown) cooldownTimer = cooldown;
        }
    }
    bool TryThrowLeaf() {
        var foundEnemy = false;
        Vector3 closest_dir = Vector3.zero;
        float closest_dist = 1000;
        foreach (var d in dirs) {
            var hit = Physics2D.Raycast(transform.position, d, 100, layerMask);
            if (hit.collider != null && hit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
                var dist = Vector3.Distance(hit.collider.transform.position, transform.position);
                if (dist < closest_dist) {
                    closest_dist = dist;
                    closest_dir = d;
                    foundEnemy = true;
                }
            }
        }
        if (foundEnemy) {
            SfxManager.instance.PlaySound(SoundType.LEAF_ATTACK, 0.5f);
            var leaf = Instantiate(leafProjectilePrefab);
            leaf.transform.position = transform.position;
            leaf.GetComponent<LeafProjectile>().SetDirection(closest_dir);
            return true;
        }
        return false;
    }

}

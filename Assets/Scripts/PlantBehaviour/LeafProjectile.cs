using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeafProjectile : MonoBehaviour
{
    public float speed;
    private Vector3 vel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += Vector3.forward * 1;
        transform.position += vel * Time.deltaTime;
    }

    public void SetDirection(Vector3 dir) {
        vel = dir * speed;
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")) {
            var enemy = col.collider.gameObject.GetComponent<Enemy>();
            enemy.DealDamage(3, Vector3.zero);
        }
    }
}

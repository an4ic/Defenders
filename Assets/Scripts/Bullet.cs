using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    

    private void Update() {
        if(GameObject.FindGameObjectsWithTag("Enemy") != null) {
           GameObject enemyTarget = GameObject.FindGameObjectWithTag("Enemy");
            transform.position = Vector2.MoveTowards(transform.position, enemyTarget.transform.position, 0.2f);
            if(transform.position == enemyTarget.transform.position) {
                
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Enemy") {
            print("coll");
            Destroy(this.gameObject);
        }
    }
}

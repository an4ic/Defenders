using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    private float maxDistance;

    public string TowerType;
    public int TowerPrice;

    private const string ArchTower = "ArchTower";
    private const string CastleTower = "CastleTower";
    private const string StoneTower = "StoneTower";

    private GameObject enemyTarget;


    public string GetTowerType {
        get {return TowerType;}
    }

    public int GetTowerPrice{
        get {return TowerPrice;}
    }

    private void Start() {
        
        maxDistance = FindObjectOfType<GameManager>().AttackDistance;
       // enemyTarget = GameObject.FindGameObjectWithTag("Enemy");
        
    }


    private void Update() {
        StartAttack();
    
    }

    private void StartAttack () {
        
        if(GameObject.FindGameObjectsWithTag("Enemy") != null) {
            enemyTarget = GameObject.FindGameObjectWithTag("Enemy");
            if(enemyTarget != null) {
                if( Mathf.Abs(Vector2.Distance(transform.position, enemyTarget.transform.position)) < maxDistance) {
                    
                    BulletStart ();
    //                print("distance");
                }
            }
        }
    }


    private void BulletStart () {
        if(GameObject.FindGameObjectWithTag("Bullet1") == null && TowerType == ArchTower) {
            GameObject temp =  Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;
        }
        if(GameObject.FindGameObjectWithTag("Bullet2") == null  && TowerType == CastleTower) {
            GameObject temp =  Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

        }
        if(GameObject.FindGameObjectWithTag("Bullet3") == null && TowerType == StoneTower) {
            GameObject temp =  Instantiate(bullet, transform.position, Quaternion.identity) as GameObject;

        }
       
        
    }


}

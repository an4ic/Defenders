using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{   

    private GameObject checkPoints;
    public float health = 0;
    public string EnemyType;
    private string dead = "Dead";

    private Animator anim;

    private int i = 0;
    private bool IsKilled = false;

    private void Start() {    
        anim = GetComponent<Animator>();
        checkPoints = GameObject.Find("CheckPoints");
    }

    private void Update() {        
        
        if(!IsKilled)
            EnemyMove();
       
    }


     private void EnemyMove () {

            if(transform.position != checkPoints.transform.GetChild(i).transform.position) {
                transform.position = Vector2.MoveTowards(transform.position, checkPoints.transform.GetChild(i).transform.position,  Time.deltaTime);           
            }
            if(transform.position == checkPoints.transform.GetChild(i).transform.position && i < checkPoints.transform.childCount - 1) {
                i++;
            }
        
    }


    private void OnTriggerEnter2D(Collider2D col) {
        if(col.tag == "Bullet2" || col.tag == "Bullet2" || col.tag == "Bullet3") {
            health--;
            if(health == 0){
                FindObjectOfType<GameManager>().DeleteWaveList();
                EnemyKill();
            }
        }

        if(col.tag == "Exit") {
            FindObjectOfType<GameManager>().DeleteWaveList();
            Destroy(this.gameObject);
        }
    }

    private void EnemyKill () {
        IsKilled = true;
        this.tag = dead;
        
        if(EnemyType == "enemy1")
            FindObjectOfType<GameManager>().SetPlayerScore += 1;
        if(EnemyType == "enemy2")
            FindObjectOfType<GameManager>().SetPlayerScore += 2;
        if(EnemyType == "enemy3")
            FindObjectOfType<GameManager>().SetPlayerScore += 3;
        
        print("msg");

        anim.Play("Dying");
        Destroy(this.gameObject, 2.0f);
          
    }


    
}

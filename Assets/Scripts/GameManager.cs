using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Check Point for enemies")]
    public GameObject CheckPoints;
    [Header("enemies")]
    [SerializeField] GameObject enemy, enemy1, enemy2;
    [SerializeField] Transform SpawnPoint;

    [Header("TOWER PREFABS")]
    [SerializeField] GameObject castle, stone, arch;


    [Header("ENEMY HEALTH BAR")]
    public float Enemy1Heath = 5f;
    public float Enemy2Health = 10f;
    public float Enemy3Health = 20f;

    private GameObject selectedTower;
    [Header("ATTACT DISTANCE BETWEEN ENEMY AND TOWER")]
    public float AttackDistance = 5.4f;   

    public Text ScoreText;

    private Vector2 mousePosition;
    private Vector3 pressedButton;

    private List<GameObject> waveList = new List<GameObject>();
    public float waveCount = 3; // default
    private bool IsRespawn = false;

    private int PLAYER_SCORE = 10;


    public void DeleteWaveList () {
        waveList.RemoveAt(waveList.Count - 1);
    }
  

    private void Start() {

       

        mousePosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        IsRespawn = true;
        StartCoroutine(GenerateWave());
    }



    private void Update() {

        print("waveList: " + waveList.Count);

        // Spawn Wave and Increase on Level
        if(waveList.Count == 0) {
            IsRespawn = true;
            StartCoroutine(GenerateWave());  
        }

        // Control Tower placement on touch 
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            Vector3 temp = Camera.main.ScreenToWorldPoint(touch.position);
            switch(touch.phase) {
                case TouchPhase.Began:
                    print("began");
                    for(int i = 0; i < Input.touchCount; i++) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        RaycastHit hit;

                        if(Physics.Raycast(ray.origin, ray.direction, out hit)) {
                            if(hit.transform.name == "CastleTower" || hit.transform.name == "StoneTower" || hit.transform.name == "ArchTower") {
                                pressedButton = hit.transform.position;
                                GenerateTower(hit.transform.name);
                                selectedTower.GetComponent<TowerController>().enabled = false;
                                selectedTower.transform.position = new Vector2(temp.x, temp.y);
                            }
                        }
                    }
                break;

                case TouchPhase.Moved:
                    print(Camera.main.ScreenToWorldPoint(touch.position));
                    if(selectedTower != null) {
                        selectedTower.transform.position = new Vector2(temp.x, temp.y);       
                    }
                    break;

                case TouchPhase.Ended:
                    print("ended");
                    for(int i = 0; i < Input.touchCount; i++) {
                        Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
                        RaycastHit hit;
                        
                        if(Physics.Raycast(ray.origin, ray.direction, out hit)) {
                            if(hit.transform.tag == "BuildSpot") {
                                selectedTower.transform.position = hit.transform.position;
                                selectedTower.GetComponent<TowerController>().enabled = true;
                                hit.transform.gameObject.GetComponent<BoxCollider>().enabled = false;
                                selectedTower = null;
                            }
                        }
                        else  {
                            Destroy(selectedTower);
                            selectedTower = null;
                        }

                        
                     }
                    break;
            }
        }

    }

 

    public void GenerateTower (string name) {
        if(name == "CastleTower")
            selectedTower = Instantiate(castle,  mousePosition,  Quaternion.identity);
        else if(name == "StoneTower") 
            selectedTower = Instantiate(stone, mousePosition, Quaternion.identity);
        else if(name == "ArchTower")
            selectedTower = Instantiate(arch, mousePosition, Quaternion.identity);
    }

    
    private void SpawnEnemy() {
        Instantiate(enemy, SpawnPoint.position, Quaternion.identity);
    }
    



    IEnumerator GenerateWave () {
        
        if(IsRespawn) {
            for (int i = 0; i < waveCount; i++) {
                GameObject instance = Instantiate(enemy, SpawnPoint.position, Quaternion.identity); 
                waveList.Add(instance);
                yield return new WaitForSeconds(1f);
            }
            waveCount += 2;
            IsRespawn = false;
        }    
    }

   


}

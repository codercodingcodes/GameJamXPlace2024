using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public GameEvent startEvent;
    public GameEvent placementComplete;
    private GameEventListener placementCompleteListener;
    public GameEvent SceneLoaded;
    public bool rangeVisible;
    public GameObject enemy;
    public GameObject fastEnemy;
    public GameObject tankyEnemy;
    public float[] enemySpawnRate = {1,0,0};
    public int tankyCost = 50;
    public int fastCost = 5;
    private int lvlCost;
    public int level;


    // Start is called before the first frame update
    void Start()
    {
        placementCompleteListener = this.gameObject.GetComponent<GameEventListener>();
        placementComplete.AddListener(placementCompleteListener);
        // SceneLoaded.TriggerEvent();
    }

    public void StartLevel(){
        SpawnWave(level);
    }
    public int CalculateLvlPts(int x){
        return Mathf.FloorToInt(Mathf.Sqrt(x*500f));
    }
    public void CalculateSpawnRate(int x){
        float c1;
        float c2;
        float c3;
        c1 = (1.3f*Mathf.Log(x-0.3f))/(1+1.3f*Mathf.Log(x));
        if (c1>1){
            c1 = 1;
        }else if(c1<0){
            c1=0;
        }
        c2 = (10*Mathf.Log(x))/(1+10*Mathf.Log(x))-c1;
        if (c2>1){
            c2 = 1;
        }else if(c2<0){
            c2=0;
        }
        c3 = 1-c1-c2;
        if (c3>1){
            c3 = 1;
        }else if(c3<0){
            c3=0;
        }
        enemySpawnRate[0] = c1;
        enemySpawnRate[1] = c2;
        enemySpawnRate[2] = c3;
    }
    public void SpawnWave(int lvl){
        CalculateSpawnRate(lvl);
        lvlCost = CalculateLvlPts(lvl);
        StartCoroutine(SampleLevel());
    }
    IEnumerator SampleLevel()
    {
        
        while (lvlCost>0){
            yield return new WaitForSeconds(UnityEngine.Random.Range(3f,5f));
            float temp = UnityEngine.Random.Range(0f,1f);
            Debug.Log(lvlCost);
            Debug.Log(temp);
            foreach  (float e in enemySpawnRate){
                Debug.Log(e);
            }
            
            if (temp<enemySpawnRate[0] && lvlCost>= 50){
                Instantiate(tankyEnemy, new Vector3(UnityEngine.Random.Range(-13f,9f), 11, 0), Quaternion.identity);
                lvlCost -= 50;
            }else if(temp < enemySpawnRate[1] && lvlCost >= 5){
                Instantiate(fastEnemy, new Vector3(UnityEngine.Random.Range(-13f,9f), 11, 0), Quaternion.identity);
                lvlCost -= 5;
            }else{
                Instantiate(enemy, new Vector3(UnityEngine.Random.Range(-13f,9f), 11, 0), Quaternion.identity);
                lvlCost -= 1;
            }
            
        }
    }

}
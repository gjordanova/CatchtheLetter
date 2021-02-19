using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnerObject : MonoBehaviour
{
    public GameControllAirplane gameControll;
    Collectable_Letter coll_lett;
    public Spawner spawner_enemy;
    public Spawner spawner_collectable;
    public Spawner[] spawner_tree;
    GameObject Enemy_Obj;
    GameObject Collectable_Obj;
    GameObject Tree_Obj;
    float timeEnemy = 0.2f;
    float repeatEnemy=6.0f;
    float timeCollectable = 0.15f;
    float timeEnemyIncrese = 0.05f;
    float timeCollectableIncrese = 0.05f;
    float timeTree = 0.5f;
    float timeTreeIncrese = 0.02f;

    private void Awake()
    {

    }
    private void Start()
    {

    }
    void Update()
    {
     
        if (gameControll.gameOver == true)
        {
            Time.timeScale = 0;
            gameControll.retryGame = true;
            CancelInvoke("Launch_Enemy");
            CancelInvoke("LaunchHeart");
            CancelInvoke("LaunchTree");
            //DestroyImmediate(Enemy_Obj);
            //DestroyImmediate(Tree_Obj);
            //DestroyImmediate(Collectable_Obj);
            //DestroyObject(Enemy_Obj);
            spawner_enemy.Object.SetActive(false);
            spawner_collectable.Object.SetActive(false);
        }

        else if (gameControll.secunde==17)
        {
            Debug.Log("retray");
            CancelInvoke();
            CancelInvoke("LaunchHeart");
            CancelInvoke("LaunchTree");
        }
        else if (gameControll.pause == true)
        {
            Time.timeScale = 0;
            gameControll.resume = false;
        }
        else if (gameControll.spawnObj == true)
        {
            Debug.Log("spawnObj");
            InvokeRepeating("Launch_Enemy", timeEnemy, repeatEnemy);
            //InvokeRepeating("Launch_Enemy", 0, 0);
            Debug.Log(timeEnemy);
            InvokeRepeating("Launch_Collectable", timeCollectable, 7.0f);
            InvokeRepeating("LaunchTree", timeTree, 8.0f);
            spawner_enemy.Object.SetActive(true);
            spawner_collectable.Object.SetActive(true);
            gameControll.spawnObj = false;
            int kolkupati = 1;
            kolkupati++;
            Debug.Log(kolkupati);
        }
        else if (gameControll.continue_game == true)
        {
            Debug.Log("continue");
            timeEnemy += timeEnemyIncrese;
            timeCollectable += timeCollectableIncrese;
            timeTree += timeTreeIncrese;
            Debug.Log(timeEnemy);
            Debug.Log(timeCollectable);
            gameControll.continue_game = false;
            //InvokeRepeating("Launch_Enemy", 0.2f, 1.9f);
        }
        else if(gameControll.start == true)
        {

        }
    }
    public void Launch_Enemy()
    {
        //Enemy_Obj = Instantiate(spawner_enemy.Object);
        //Enemy_Obj.transform.position = transform.position + new Vector3(0, Random.Range(-spawner_enemy.height_min, spawner_enemy.height_max));
        Enemy_Obj = (GameObject)Instantiate(spawner_enemy.Object,new Vector3(15, Random.Range(-spawner_enemy.height_min, spawner_enemy.height_max),0),Quaternion.identity);
        Debug.Log(Enemy_Obj.name);
      
    }
    public void Launch_Collectable()
    {
       
        //Collectable_Obj = Instantiate(spawner_collectable.Object);
        //Collectable_Obj.transform.position = transform.position + new Vector3(0, Random.Range(-spawner_collectable.height_min, spawner_collectable.height_max));
        Collectable_Obj = (GameObject)Instantiate(spawner_collectable.Object, new Vector3(15, Random.Range(-spawner_enemy.height_min, spawner_enemy.height_max), 0), Quaternion.identity);

    }
    public void CalculateSpawning(float timeEnemy,float increseEnemy)
    {
        if ( gameControll.continue_game == true)
        {

        }
        else if(gameControll.tryAgainLevel == true)
        {

        }
    }
 
    public void LaunchTree()
    {
        for (int i = 0; i < spawner_tree.Length; i++)
        {
            StartCoroutine("WaitToStart");
            Tree_Obj = Instantiate(spawner_tree[i].Object);
            Tree_Obj.transform.position = transform.position + new Vector3(Random.Range(-spawner_tree[i].height_min, spawner_tree[i].height_max), -4.8f);
        }

    }
    IEnumerator WaitToStart()
    {
        yield return new WaitUntil(() => gameControll.start == true);
    }
}






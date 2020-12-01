using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerObject : MonoBehaviour
{
    public GameControllAirplane gameControll;
    public Spawner spawner_enemy;
    public Spawner spawner_collectable;
    public Spawner[] spawner_tree;
    GameObject Enemy_Obj;
    GameObject Collectable_Obj;
    GameObject Tree_Obj;

    void Update()
    {
        if (gameControll.gameOver == true)
        {
            Time.timeScale = 0;
            gameControll.retry = true;
            CancelInvoke("Launch_Enemy");
            CancelInvoke("LaunchHeart");
            CancelInvoke("LaunchTree");
            Collectable_Obj.SetActive(false);
            Enemy_Obj.SetActive(false);
            Tree_Obj.SetActive(false);
        }
        if (gameControll.pause == true)
        {
            Time.timeScale = 0;
            Debug.Log(Time.timeScale);
            gameControll.resume = false;
        }
        if (gameControll.spawnObj == true)
        {
            InvokeRepeating("Launch_Enemy", 1.0f, 1.9f);
            InvokeRepeating("Launch_Collectable", 0.7f, 5.0f);
            InvokeRepeating("LaunchTree", 0.5f, 8.0f);
            gameControll.spawnObj = false;
        }
    }

    public void Launch_Enemy()
    {
        Enemy_Obj = Instantiate(spawner_enemy.Object);
        Enemy_Obj.transform.position = transform.position + new Vector3(0, Random.Range(-spawner_enemy.height_min, spawner_enemy.height_max));
    }
    public void Launch_Collectable()
    {
        Collectable_Obj = Instantiate(spawner_collectable.Object);
        Collectable_Obj.transform.position = transform.position + new Vector3(0, Random.Range(-spawner_collectable.height_min, spawner_collectable.height_max));
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






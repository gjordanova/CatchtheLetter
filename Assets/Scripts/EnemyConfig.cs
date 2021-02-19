using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Enemy Config")]
public class EnemyConfig : ScriptableObject
{
    [SerializeField] GameObject EnemyObj;
    [SerializeField] GameObject pathObj;
    [SerializeField] float timeBetweenSpawned = 0.5f;
    [SerializeField] float randomFactor = 0.5f;
    [SerializeField] int numbersOfEnemy = 10;
    [SerializeField] float moveSpeed = 0.5f;

    public GameObject GetEnemyPrefab() { return EnemyObj; }
    public GameObject GetPathObj() { return pathObj; }
    public float getTimeBetweenSpawned() { return timeBetweenSpawned; }
    public float getTimerandomFactor() { return randomFactor; }
    public int getNumbersOfEnemy() { return numbersOfEnemy; }
    public float getMoveSpeed() { return moveSpeed; }
}

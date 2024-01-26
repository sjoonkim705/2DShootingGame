using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // 목표 : 일정 시간마다 적을 프리팹으로부터 생성해서 내 위치에 갖다 놓고 싶다.
    // 필요 속성:
    // - 적 프리팹
    // - 일정시간
    // - 현재시간
    // 구현 순서:
    // 적 생성 시간을 랜덤하게 하고 싶다.
    // 필요 속성
    // - 최소 시간
    // - 최대 시간

    private float spawnTimer = 0f;
    public int EnemyNumber = 10;
    private int enemyCount = 0;
    public float MinTime = 0.5f;
    public float MaxTime = 3f;

    private const float MinX = -3.0f;
    private const float MaxX = 3.0f;

    private float _spawnInterval;

    
    [Header("적 프리팹")]
    public GameObject Enemy_Base_Prefab;
    public GameObject Enemy_Target_Prefab;
    public GameObject Enemy_Follow_Prefab;


    //List<GameObject> Enemies = new List<GameObject>();
    GameObject[] Enemies;


    void Start()
    {
        Enemies = new GameObject[EnemyNumber];
        spawnTimer = 0f;
        SetRandomSpawnTime();

    }
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer > _spawnInterval)
        {
            Vector2 spawnPos = transform.position;
            spawnPos.x = Random.Range(MinX, MaxX);
            transform.position = spawnPos;

            if (EnemyNumber > enemyCount)
            {
                int enemyRandomIndex = Random.Range(0, 10);
                if (enemyRandomIndex < 4 )
                {

                    Enemies[enemyCount] = GameObject.Instantiate(Enemy_Target_Prefab, this.transform.position,transform.rotation);
                }
                else if (enemyRandomIndex < 5)
                {
                    Enemies[enemyCount] = GameObject.Instantiate(Enemy_Follow_Prefab, this.transform.position, transform.rotation);
                }
                else
                {
                    Enemies[enemyCount] = GameObject.Instantiate(Enemy_Base_Prefab, this.transform.position, transform.rotation);
                }
                //Enemies[enemyCount].transform.position = this.transform.position;
                enemyCount++;

            }
            SetRandomSpawnTime();
            spawnTimer = 0;
        }
    }
    private void SetRandomSpawnTime()
    {
        _spawnInterval = Random.Range(MinTime, MaxTime);
    }
}
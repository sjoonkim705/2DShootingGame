using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerFire : MonoBehaviour
{
    // [총알:발사 제작]
    // 목표 : 총알을 만들어서 발사하고 싶다.
    // 속성:
    // - 총알
    // - 총구 위치

    // 구현 순서

    // 2. 프리팹으로부터 총알을 동적으로 만들고,
    // 3. 만든 총알의 위치를 총구의 위치로 바꾼다.


    [Header("총알 프리팹")]
    public GameObject BulletPrefab; // 총알 프리팹
    public GameObject SideBulletPrefab;
    public GameObject BoomPrefab;


    [Header("총구들")]
    public GameObject[] Muzzles;
    public GameObject[] SideMuzzles;

    [Header("타이머")]
    private float _timer;
    private const float COOL_TIME = 0.6f;
    private float _boomTimer = 0;
    private const float BOOM_COOL_TIME = 5.0f;

    // public const Vector3 SHOT_INTERVAL = { 0.6f, 0f, 0f };
    [Header("자동 모드")]
    public bool AutoMode = false;
    public AudioSource FireSource;


    void Start()
    {
        _timer = 0f;
        _boomTimer = 0;
        AutoMode = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("자동 공격 모드");
            AutoMode = true;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("수동 공격 모드");
            AutoMode = false;
        }

        // 타이머 계산

            _timer -= Time.deltaTime;
            _boomTimer -= Time.deltaTime;
     
        // 1. 발사 버튼을 누르면

        bool ready = (AutoMode || Input.GetKeyDown(KeyCode.Space));
        if (_timer <= 0 && ready)
        {
            Fire();
        }
        if (_boomTimer <= 0 && Input.GetKeyDown(KeyCode.Alpha3)) // boom
        {
            BoomFire();
            float boomFiredTime = _boomTimer;
        }

    }
    public void Fire()
    {
        _timer = COOL_TIME;
        // 2. 프리팹으로부터 총알을 만든다.
        // GameObject[] bullet = new GameObject[Muzzles.Length];
        // bullet[0] = GameObject.Instantiate(BulletPrefab);
        // bullet[1] = GameObject.Instantiate(BulletPrefab);
        FireSource.Play();

        //목표 : 총구 개수 만큼 총알을 만들고, 만든 총알의 위치를 가 총구의 위치로 만든다.
        for (int i = 0; i < Muzzles.Length; i++)
        {
            // 1. 총알을 만들고
            // 2. 위치를 설정한다.
            GameObject bullet = GameObject.Instantiate(BulletPrefab);
            bullet.transform.position = Muzzles[i].transform.position;
            //Vector3 sideMuzzle = Muzzles[i].transform.position;
 
        }
        for (int i =0; i< SideMuzzles.Length; i++)
        {
            GameObject sideBullet = GameObject.Instantiate(SideBulletPrefab);
            sideBullet.transform.position = SideMuzzles[i].transform.position;
        }
    }
    public void BoomFire()
    {
        _boomTimer = BOOM_COOL_TIME;

        GameObject boom = GameObject.Instantiate(BoomPrefab);
        boom.transform.position = new Vector2(0, 1.6f);

    }
}


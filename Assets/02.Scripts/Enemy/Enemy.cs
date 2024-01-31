using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;
using static UnityEngine.GraphicsBuffer;
using UnityEngine.UIElements;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public enum EnemyType
{ 
    Basic,
    Target,
    Follow,
}


public class Enemy : MonoBehaviour
{
    public float Speed = 0.5f;
    private UnityEngine.Vector2 _dir;
    public int EnemyHP = 10;
    public EnemyType EType;
    private GameObject _target;
    Player playerInfo;
    private float _radianTarget;
    private float _degreeTarget;
    
    public GameObject HealthItemPrefab;
    public GameObject SpeedItemPrefab;
    public GameObject EnemyDieVFXPrefab;

    public Animator EnemyAnimator;




    // 목표:
    // etype.basic 타입 -> 아래로 이동
    // etype.target 타입 : 처음 태어났을대 플레이어가 있는 방향으로 이동
    // 속성
    // - enemyType
    // 구현 순서
    // 1. 시작할 때 방향을 구한다. (플레이어가 있는 방향)
    // 2. 방향을 향해 이동한다.
    private void Awake()
    {

    }

    void Start()
    {
        // 캐싱 : 자주 쓰는 데이터를 더 가까운 장소에 저장해두고 필요할때 가져다 씀
        _target = GameObject.Find("Player");
        playerInfo = _target.GetComponent<Player>();
        EnemyAnimator = this.gameObject.GetComponent<Animator>();

        if (EType == EnemyType.Basic )
        {
            _dir = UnityEngine.Vector2.down;
        }
        else if (EType == EnemyType.Target || EType == EnemyType.Follow)
        {
            // 1. 시작할 때 방향을 구한다. (플레이어 있는방향)
            // 1-1. 플레이어를 찾는다.
            // GameObject.FindGameObjectsWithTag("Player");
            // 1-2. 방향을 구한다. (target - me)
            //float angle = Mathf.Atan2(y2 - y1, x2 - x1) * Mathf.Rad2Deg;
            //this.transform.localRotation()
            //float angle = UnityEngine.Vector3.Angle(_target.transform.position, this.transform.position);

            _dir = _target.transform.position - this.transform.position;
            //transform.Rotate(_dir, angle * Time.deltaTime);
            // tan@ = y/x  -> @ = arctan(y/x)
            _radianTarget = Mathf.Atan2(_dir.y, _dir.x);

            _degreeTarget = _radianTarget * Mathf.Rad2Deg;
            transform.rotation = UnityEngine.Quaternion.Euler(new Vector3(0, 0, _degreeTarget+90));

        }

        _dir.Normalize();

    }

    void Update()
    {
        // 목표: 적을 아래로 이동시키고 싶다.
        if (EType == EnemyType.Follow)
        {
            _dir = _target.transform.position - this.transform.position;
        }
        _dir.Normalize();

        _radianTarget = Mathf.Atan2(_dir.y, _dir.x);

        _degreeTarget = _radianTarget * Mathf.Rad2Deg;
        transform.rotation = UnityEngine.Quaternion.Euler(new Vector3(0, 0, _degreeTarget + 90));

        UnityEngine.Vector2 enemyPosition = transform.position + (UnityEngine.Vector3)_dir * Speed * Time.deltaTime;
        transform.position = enemyPosition;

    }

    // 목표 : 충돌하면 적과 플레이어를 삭제하고 싶다.
    // 순서
    // 1. 만약에 충돌이 일어나면


    // 충돌이 일어나면 호출되는 이벤트 함수
    private void OnCollisionEnter2D(Collision2D collision)
    {

        // 2. 적과 플레이어를 삭제한다.
        // 너죽고

        // 나죽자
        //Destroy(this.gameObject);
        // 충돌한 게임오브젝트의 태그를 확인
        // EnemyAnimator.Play();

       
        if (collision.collider.CompareTag("Bullet"))
        {
            Bullet bullet = collision.collider.GetComponent<Bullet>();
            EnemyHP -= bullet.BDamage;
            collision.gameObject.SetActive(false);
            // Destroy(collision.collider.gameObject);     // bullet disappears when hits enemy
        }

        else if (collision.collider.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            EnemyDie();
            player.DecreaseHealth(1);

        }

        if (EnemyHP <= 0)
        {
            EnemyDie();


            if (collision.collider.tag == "Bullet")
            {
                MakeItem();
            }
        }
        else
        {
            if (EType == EnemyType.Basic)
            {
                EnemyAnimator.Play("Hit");
            }
            else if (EType == EnemyType.Target)
            {
                EnemyAnimator.Play("Hit");
            }
            else if (EType == EnemyType.Follow)
            {
                EnemyAnimator.Play("Hit");
            }

        }

    }
    public void MakeItem()
    {
        int itemDropPerc = Random.Range(0, 10);
        if (itemDropPerc == 0)  // item Spawn rate
        {
            if (Random.Range(0, 2) == 0)
            {
                ItemDrop(ItemType.Health);
            }
            else
            {
                ItemDrop(ItemType.SpeedUp);
            }
        }
    }
    private void ItemDrop(ItemType itemType)
    {
        if (itemType == ItemType.Health)
        {
            GameObject Item = GameObject.Instantiate(HealthItemPrefab);
            Item.transform.position = this.transform.position;
        }
        else if (itemType == ItemType.SpeedUp)
        {
            GameObject Item = GameObject.Instantiate(SpeedItemPrefab);
            Item.transform.position = this.transform.position;
        }
    }
    public void EnemyDie()
    {

        GameObject DieVFX = GameObject.Instantiate(EnemyDieVFXPrefab);
        DieVFX.transform.position = this.transform.position;

        this.gameObject.SetActive(false);
        //Destroy(this.gameObject);
        //playerInfo.KillCount++;
        //GameObject smGameObject = GameObject.Find("ScoreManager");
        //ScoreManager scoreManager = smGameObject.GetComponent<ScoreManager>();
        //int score = scoreManager.GetScore();
        //scoreManager.AddScore();
        ScoreManager.Instance.Score += 1;
        Debug.Log(ScoreManager.Instance.Score);


    }


}



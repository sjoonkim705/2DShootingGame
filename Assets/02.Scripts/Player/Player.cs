using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Player : MonoBehaviour
{
    //public static Player instance;
    private int _playerHP = 3;
    private float _floatTimer = 0;
    public int AliveTime = 0;
    public int KillCount = 0;
    public AudioSource EnemyHitSound;

    public int GetPlayerHP()
    {
        return _playerHP;
    }
    public void SetPlayerHP(int healthPoint)
    {
        _playerHP = healthPoint;
    }
    public void DecreaseHealth(int amount)
    {
        if (amount <= 0)
        {
            return;

        }
        _playerHP -= amount;
        if (_playerHP <=0)
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {

        /*        //GetComponent<컴포넌트 타입> (); ->GameObject의 컴포넌트를 가져오는 메소드
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                sr.color = Color.white;

                //Transform tr = GetComponent<Transform>();
                //tr.position = new Vector2(0, -2.7f);
                transform.position = new Vector2(0f, -2.7f);

                PlayerMove playerMove = GetComponent<PlayerMove>();
                playerMove.Speed = 5f;
                //*/



    }
    private void Update()
    {
        _floatTimer += Time.deltaTime;
        if (_floatTimer > 1.0f)
        {
            AliveTime++;
            _floatTimer = 0;
            // Debug.Log(AliveTime);
        }

        if (_playerHP <= 0)
            Destroy(this.gameObject);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            EnemyHitSound.Play();
        }
        Debug.Log($"PlayerHP: {_playerHP}");
    }
    public void AddHealth(int amount)
    {
        if (amount <= 0)
        {
            return;
        }    
        _playerHP += amount;
    }

}

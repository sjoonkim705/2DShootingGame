using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    private float _boomLastTimer = 0;
    void Start()
    {
        _boomLastTimer = 0;
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i].GetComponent<Enemy>();
            enemy.EnemyDie();
            enemy.MakeItem();

        }
    }

    // Update is called once per frame
    void Update()
    {
        _boomLastTimer += Time.deltaTime;
        if ( _boomLastTimer >= 3.0f)
        {
            Destroy(this.gameObject);

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            Enemy touchedEnemy = collision.gameObject.GetComponent<Enemy>();
            if (touchedEnemy != null )
            {
                touchedEnemy.EnemyDie();
                touchedEnemy.MakeItem();
            }
        }
    }


}

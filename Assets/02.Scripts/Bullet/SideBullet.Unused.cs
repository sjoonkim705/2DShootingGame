using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class SideBullet : MonoBehaviour
{
    public float bSpeed = 10f;
    Vector3 bDir;
    float timer = 0; // 0.3초간 대기후 발사
    const float IDLE_TIME = 0.3f;


    void Start()
    {
        timer = 0;
        bDir = Vector3.up;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (timer > IDLE_TIME)
        {
            transform.position += (Vector3)bDir * Time.deltaTime * bSpeed;
        }
    }
}

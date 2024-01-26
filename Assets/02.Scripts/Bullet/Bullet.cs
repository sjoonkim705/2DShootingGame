using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public enum BulletType
{
    Main = 0,
    Sub = 1,
    Pet = 2
} // 열거형(상수를 기억하기 좋게 그룹화하는것)

public class Bullet : MonoBehaviour
{
    // 목표 : 총알이 위로 계속 이동하고 싶다.
    // 속성:
    // - 속력
    // 구현 순서
    // 1. 이동할 방향을 구한다.
    // 2. 이동한다.

    public float BSpeed;
    public BulletType BType = BulletType.Main; // 0이면 주총알, 1이면 보조총알, 2면 펫이쏘는 총알
    public int BDamage;
    private const float MAX_DETACT_RANGE_Y = 8f;

    Vector2 bDir;

    void Start()
    {
        if (BType == BulletType.Main)
        {
            BDamage = 10;
            BSpeed = 20;
        }
        else if (BType == BulletType.Sub)
        {
            BDamage = 5;
            BSpeed = 10;
        }

        bDir = Vector2.up;

    }

    // Update is called once per frame
    void Update()
    {
        // 1. 이동할 방향을 구한다.
        // 이동한다.
        transform.position += (Vector3)bDir * Time.deltaTime * BSpeed;


    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyZone : MonoBehaviour
{
    // 목표: 다른 물체와 충돌하면 충돌한 물체를 삭제한다.
    // 구현 순서

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            collision.gameObject.SetActive(false);
        }
        else if (collision.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    public float Speed = 1f;
    private Vector2 _mapMovingdir = Vector2.down;

    private void Awake()
    {
    }
    private void Update()
    {
        // 배경 스크롤
        // -> 게임 화면에서 배경 이미지를일정한 속도로
        // 움직여 캐릭터나 몬스터 등의 움직임을 더 동적으로 만들어주느 기술
        // 캐릭터는 그대로 두고 배경만 움직여서 캐릭터가 움직이는 것처럼 눈속임을 한다.
        // 목표 : 아래로 이동하고 싶다.
        // 순서:
        // 1. 방향을
        // 2. 이동한다.

/*        Vector2 newPos = transform.position + (Vector3)_mapMovingdir*Speed*Time.deltaTime;
        if (newPos.y < -12.6f)
        {
            newPos.y = 12.6f;
        }
        transform.position = newPos;*/





    }
}

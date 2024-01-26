using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMove : MonoBehaviour
{
    /**
         목표 : 플레이어를 이동하고 싶다.
         필요 속성 : 
            - 이동 속도
            순서:
           1. 키보드 입력을 받는다.
           2. 키보드 입력에 따라 이동할 방향을 계산한다.
           3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킨다.
    
     **/

    private float _speed = 3f; // 이동 속도: 초당 3만큼 이동하겠다.
    public const float MinX = -3f;
    public const float MaxX = 3f;
    public const float MinY = -6f;
    public const float MaxY = 0f;

    public Animator MyAnimator;
    public float GetSpeed()
    {
 
        return _speed;
    }
    public void SetSpeed(float speed)
    {
        _speed = speed;

    }
    public void IncreaseSpeed()
    {
        SetSpeed(_speed + 1);
    }
    private void Awake()
    {
        MyAnimator = this.gameObject.GetComponent<Animator>();
    }

    void Start()
    {

    }

    void Update()
    {
        //transform.Translate(Vector2.up * Speed * Time.deltaTime);
        // (0.1) * 3 -> (0.3) * Time.deltaTime
        // deltaTime은 프레임 간 시간 간격을 반환한다.
        // 30fps: d-> 0.03초
        // 60fps: d-> 0.016초
        Move();
        SpeedUpDown();

    }

    private void Move()
    {
        
        // 1. 키보드 입력을 받는다.
        // float h = Input.GetAxis("Horizontal");     // -1.0f ~ 0f ~ +1.0f 좌우키 입력에 따라서
        // float v = Input.GetAxis("Vertical");       // -1.0f ~ 0f ~ +1.0f 좌우키 입력에 따라서 

        float h = Input.GetAxisRaw("Horizontal");     // -1 or 0 or 1
        float v = Input.GetAxisRaw("Vertical");
        MyAnimator.SetInteger("h", (int)h);


    

        // Debug.Log($"h: {h},  v: {v}");


        // 2. 키보드 입력에 따라 이동할 방향을 계산한다
        // Vector2 dir = Vector2.right * h + Vector2.up * v;
        Vector2 dir = new Vector2(h, v);

        // 이동 방향을 정규화 ( 방향은 같지만 길이를 1로 만들어줌)
        dir = dir.normalized;
        

        // 3. 이동할 방향과 이동 속도에 따라 플레이어를 이동시킴.
        // transform.Translate( dir * Speed * Time.deltaTime );
        // 공식을 이용한 이동
        // 새로운 위치 = 현재 위치 + 속도 * 시간
        Vector2 newPosition = (Vector2)transform.position + (dir * _speed) * Time.deltaTime;



        if (newPosition.x < MinX)
        {
            newPosition.x = MaxX;
        }
        else if (newPosition.x > MaxX)
        {
            newPosition.x = MinX;
        }

        //newPosition.y = Mathf.Max(MinY, newPosition.y);
        //newPosition.y = Mathf.Min(MaxY, newPosition.y);

        newPosition.y = Mathf.Clamp(newPosition.y, MinY, MaxY);

        /*        if (newPosition.y > MaxY)
                {
                    newPosition.y = MaxY;
                }
                else if (newPosition.y < MinY)
                {
                    newPosition.y = MinY;
                }*/


        // 현재 위치 출력
        transform.position = newPosition;
        //Debug.Log(transform.position);
        // 


    }
    private void SpeedUpDown()
    {
        bool speedUp = Input.GetKeyDown(KeyCode.E);
        bool speedDown = Input.GetKeyDown(KeyCode.Q);
        if (speedUp)
            _speed += 1f;
        if (speedDown)
            _speed -= 1f;
    }

}

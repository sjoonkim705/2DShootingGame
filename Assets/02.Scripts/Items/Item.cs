using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;


public enum ItemType
{
    Health = 0,
    SpeedUp = 1,
}
public class Item : MonoBehaviour
{
    public ItemType iType;
    private float _itemTimer = 0;
    private float _itemAppearTimer = 0;
    const float EAT_TIME = 1.0f;
    private Rigidbody2D _itemRigid;
    private GameObject _target;
    public const float DRAW_SPEED = 10f;
    public const float FOLLOW_TIME = 3.0f;
    
    public GameObject ItemVFXPrefab;


    public Animator MyAnimator;
    Vector2 _dir;

    private void Awake()
    {
        _itemRigid = GetComponent<Rigidbody2D>();
        MyAnimator = GetComponent<Animator>();
        MyAnimator.SetInteger("itemType", (int)iType);
       
    }
    private void Start()
    {
        _target = GameObject.Find("Player");
        Player player = GetComponent<Player>();


      //  _itemRigid.AddTorque(5f * Time.deltaTime);
  
    }
    private void Update()
    {
        FollowItem(FOLLOW_TIME, DRAW_SPEED);

    }
    private void FollowItem(float followTime, float drawSpeed)
    {
        _itemAppearTimer += Time.deltaTime;
        if (_itemAppearTimer >= followTime)
        {
            _dir = _target.transform.position - this.transform.position;
        }
        else
        {
            _dir = Vector2.zero;
        }
        _dir.Normalize();
        Vector2 newPos = transform.position + (Vector3)_dir * Time.deltaTime * drawSpeed;
        transform.position = newPos;
    }

    private void OnTriggerEnter2D(Collider2D otherCollision)
    {


    }
    private void OnTriggerStay2D(Collider2D otherCollision)
    {
       _itemTimer += Time.deltaTime;
        
        if (_itemTimer > EAT_TIME)
        {
            PlayerMove playerMove = otherCollision.gameObject.GetComponent<PlayerMove>();
            Player playerInfo = otherCollision.gameObject.GetComponent<Player>();
            if (iType == ItemType.Health)
            {
                playerInfo.AddHealth(1);
                GameObject HealthVFX = GameObject.Instantiate(ItemVFXPrefab);
                HealthVFX.transform.position = playerMove.transform.position;
                
            }
            else if (iType == ItemType.SpeedUp)
            {
                playerMove.AddSpeed(1);
                GameObject SpeedUpVFX = GameObject.Instantiate(ItemVFXPrefab);
                SpeedUpVFX.transform.position = playerMove.transform.position;

                Debug.Log($"Speed : {playerMove.GetSpeed()}");

            }
            Destroy(this.gameObject);

        }
    }


    private void OnTriggerExit2D(Collider2D otherCollision)
    {
        _itemTimer = 0;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RingingItem : MonoBehaviour
{
    public Rigidbody2D myRigid;

    void Start()
    {
        myRigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {

        myRigid.AddTorque(1f*Time.deltaTime);
        transform.Translate(Vector2.down * 0.5f* Time.deltaTime);
        //transform.position = newPos;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Player player = collision.collider.GetComponent<Player>();
            player.PlayerHP++;
            Destroy(this.gameObject);
        }
    }
}

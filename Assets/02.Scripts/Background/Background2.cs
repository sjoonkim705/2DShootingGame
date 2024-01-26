using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background2 : MonoBehaviour
{
    public Material MyMaterial;
    public float ScrollSpeed = 0.2f;
    Vector2 _dir = Vector2.down;

    // Start is called before the first frame update
    private void Awake()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        MyMaterial = sr.sharedMaterial;
    }
    void Start()
    {
        //MyMaterial = this.GetComponent<Material>();
        _dir = Vector2.up;

    }

    // Update is called once per frame
    void Update()
    {
        MyMaterial.mainTextureOffset += _dir * ScrollSpeed * Time.deltaTime;
        

    }
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monster : MonoBehaviour
{
    public Sprite healthBar;
    public float hp = 100.0f;
    private float fullsize;
    // Start is called before the first frame update
    void Start()
    {
        //fullsize = healthBar.texture.set

    }


    void FixedUpdate()
    {
       // print("Fixed Update");
       // healthBar.localScale = new Vector2(100.0f * fullsize / hp, healthBar.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    { 
        hp -= 1.0f;
    }

    //public static explicit operator Monster(GameObject v)
    //{
    //    throw new NotImplementedException();
    //}


    // Update is called once per frame
    void Update()
    {
      //  hp -= 1;
    }

}

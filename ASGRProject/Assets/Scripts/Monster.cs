using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monster : PhysicalObject
{
    public Sprite healthBar;
    private float fullsize;
    public float dmg = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        hp = 100.0f;
        //fullsize = healthBar.texture.set

    }



    void FixedUpdate()
    {
       // print("Fixed Update");
       // healthBar.localScale = new Vector2(100.0f * fullsize / hp, healthBar.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhysicalObject obj = (PhysicalObject)collision.gameObject;
        obj.Damage(dmg);
        obj.SendMessageUpwards("FixedUpdate");
        //hp -= 1.0f;
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

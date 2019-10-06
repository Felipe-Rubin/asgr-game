using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Monster : PhysicalObject
{
    public GameObject healthBar;
    private float fullsize;
   
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Monster>().healthBar;
        //hpSprite = healthBar.GetComponent<SpriteRenderer>();
        hp = 100.0f;
        //fullsize = healthBar.texture.set
        //healthBar.GetComponent<RectTransform>().localScale;

    }

    

    void FixedUpdate()
    {
       // print("Fixed Update");
       // healthBar.localScale = new Vector2(100.0f * fullsize / hp, healthBar.localScale.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Vector3 currentHp = healthBar.GetComponent<RectTransform>().localScale;
        PhysicalObject obj = (PhysicalObject)collision.gameObject;
        currentHp.x = obj.Damage(dmg) / 400.0f; 
        //healthBar.flipX = true;
        obj.SendMessageUpwards("FixedUpdate");
        healthBar.GetComponent<RectTransform>().localScale = currentHp;
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

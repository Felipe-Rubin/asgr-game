using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : PhysicalObject
{
    public GameObject healthBar;
    private float fullsize;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        healthBar = GetComponent<Monster>().healthBar;
        //hpSprite = healthBar.GetComponent<SpriteRenderer>();
        hp = 100.0f;
        //fullsize = healthBar.texture.set
        //healthBar.GetComponent<RectTransform>().localScale;

    }
    void FixedUpdate()
    {

        if (Math.Abs(hp) <= 0.0f)
        {
            FindObjectOfType<GameLogic>().monster_killed(this);
        }

        Vector3 currentHp = healthBar.GetComponent<RectTransform>().localScale;
        currentHp.x =  hp / 400.0f;
        healthBar.GetComponent<RectTransform>().localScale = currentHp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PhysicalObject obj = (PhysicalObject)collision.gameObject;
            obj.Damage(dmg);
        }
        //healthBar.flipX = true;
        //obj.SendMessageUpwards("FixedUpdate");
        //hp -= 1.0f;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PhysicalObject obj = (PhysicalObject)collision.gameObject;
            obj.Damage(dmg);
        }
    }


    // Update is called once per frame
    void Update()
    {
      //  hp -= 1;
    }

}

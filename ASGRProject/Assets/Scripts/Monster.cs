using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Monster : PhysicalObject
{
    public Image healthBar;
    //public GameObject healthBar;
    //private float fullsize;
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        
        //healthBar = GetComponent<Monster>().healthBar;


        //hpSprite = healthBar.GetComponent<SpriteRenderer>();
        
        //fullsize = healthBar.texture.set
        //healthBar.GetComponent<RectTransform>().localScale;
    }


    public void ReplaceHealthBar(Image hpBar)
    {
        //Destroy(GetComponent<Monster>().healthBar);
        //GetComponent<Monster>().healthBar = hpBar;
        Destroy(healthBar);
        healthBar = hpBar;
    }
    void FixedUpdate()
    {
        healthBar.fillAmount = GetRemainingHP();
        //Vector3 currentHp = healthBar.GetComponent<RectTransform>().localScale;
        //currentHp.x =  hp / 400.0f;
        //healthBar.GetComponent<RectTransform>().localScale = currentHp;
    }

    private void collisionResolution(Collider2D collision)
    {
        
        //if (collision.gameObject.tag == "Player")
        if(collision.gameObject.tag == "Player")
        {
            PhysicalObject obj = (PhysicalObject)collision.gameObject;
            obj.Damage(atk);
        }
    }
    public override float Damage(float value)
    {
        if(base.Damage(value) <= 0f && destructible){
            FindObjectOfType<GameLogic>().monster_killed(this);
        }
        return hp;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collisionResolution(collision);        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        collisionResolution(collision);
    }

    // Update is called once per frame
    void Update()
    {
      //  hp -= 1;
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : PhysicalObject
{
    //public float hp = 100.0f;

    public Rigidbody2D rb;


    //public List<GameObject>

    //public GameObject player;

    // Start is called before the first frame update


    private Vector2 direction;
    //private Vector2 movement;
    private Vector2 mousePos;
    private bool lr = true;

    public int look_right = 1;


    public override void Start()
    {
        base.Start();
        //xprev = Input.mousePosition.x;
        //yprev = Input.mousePosition.y;
        //hp = 50.0f; // Physical Object
        //rb = GetComponent<Rigidbody2D>();
        //Console.Write("update");
    }

    public void Move(Vector2 movePos)
    {
        rb.position += movePos;
    }

    public void flip_sprites(bool x,bool y)
    {
        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i].flipX = x;
            sprites[i].flipY = y;
        }

    }

    void FixedUpdate()
    {
        /* Start bars at 100% */

        //transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //cam.transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);


        //Vector2 lookDir = mousePos - rb.position;
        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //print("Looking At Angle,dir: " + angle+","+lookDir);
        
        //if (Mathf.Abs(angle) < 90.0f && !lr)
        //{
        //    lr = true;
        //    flip_sprites(!lr, false);
        //}
        //else if (Mathf.Abs(angle) < 180.0f && Mathf.Abs(angle) > 90.0f && lr)
        //{
        //    lr = false;
        //    flip_sprites(!lr, false);
        //}
        //rb.rotation = angle; // Rotates Camera


        //if (Input.GetKey("x"))
        //{

        //    hp -= 10.0f;
        //}
    }

    public override float Damage(float value)
    {
       float after_dmg = base.Damage(value);
        if (isDestructible())
        {
            SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();
            //StartCoroutine(FlashSprites(sprites, 5, 0.0f));
            StartCoroutine(flash_effect(sprites, 6));
            //flash_effect(sprites);
        }
        return after_dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PhysicalObject obj = (PhysicalObject)collision.gameObject;
        //obj.Damage(dmg);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //PhysicalObject obj = (PhysicalObject)collision.gameObject;
        //obj.Damage(dmg);
    }
    // Update is called once per frame
    void Update()
    {
        mousePos = new Vector2(Input.mousePosition.x,Input.mousePosition.y);
    }



    /* Based on the answers on
     * https://forum.unity.com/threads/make-a-sprite-flash.224086/
     *
     * This is a visual effect to be used when the character is attacked.
     * During which it becomes immune (not destructible).
    */
    
    public IEnumerator flash_effect(SpriteRenderer[] sprites,int time)
    {
        setDestructible(false);
        for (int i = 0; i < time; i++) {
            yield return new WaitForSeconds(0.05f);
            for (int j = 0; j < sprites.Length; j++){
                sprites[j].color = new Color(sprites[j].color.r, sprites[j].color.g, sprites[j].color.b, 0.0f);
            }
            yield return new WaitForSeconds(0.05f);
            for (int j = 0; j < sprites.Length; j++)
            {
                sprites[j].color = new Color(sprites[j].color.r, sprites[j].color.g, sprites[j].color.b, 1.0f);
            }
        }
        setDestructible(true);
    }


}





























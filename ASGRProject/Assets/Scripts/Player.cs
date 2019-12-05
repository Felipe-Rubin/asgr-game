using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : PhysicalObject
{
    //public float hp = 100.0f;

    //public Rigidbody2D rb;
    //public List<string> skills = new List<string>();
    private SkillSystem skillSystem;
    // Start is called before the first frame update

    private Vector2 direction;
    //private Vector2 movement;
    private Vector2 mousePos;

    public int look_right = 1;

    private Vector2 prevPos;
    public override void Start()
    {
        base.Start();
        prevPos = rb.position;
        skillSystem = FindObjectOfType<SkillSystem>();

        foreach (string s in skills)
        {
            GameObject icon = skillSystem.GetSkillIcon(s);
            FindObjectOfType<UIOverlay>().AddToSkillPanel(s, icon);
        }

        setDestructible(true);
    }

    public List<string> GetSkills()
    {
        return skills;
    }

    public void AddSkill(Skill s)
    {
        skills.Add(s.name);
        GameObject icon = skillSystem.GetSkillIcon(s.name);
        FindObjectOfType<UIOverlay>().AddToSkillPanel(s.name,icon);
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
    }

    public override float Damage(float value)
    {
       float after_dmg = base.Damage(value);
        //
        if(hp <= 0f)
        {
            FindObjectOfType<GameLogic>().OnPlayerDeath();
            return 0f;
        }
        //
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





























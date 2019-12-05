using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhysicalObject : MonoBehaviour
{
    public List<string> skills = new List<string>();
    public Rigidbody2D rb;
    public float hp = 100.0f;/* Health Points */
    public float atk = 1.0f; /* Default Damage */
    public float sp = 100.0f; /* Default Speed */
    public float def = 0.0f; /* Defense Points */
    public bool hpRecoveryEnabled = false;
    public bool spRecoveryEnabled = false;
    public float hpRecovery = 0f;
    public float hpRecoveryTime = 1f;
    public float spRecovery = 0f;
    public float spRecoveryTime = 1f;
    public List<SkillAttribute> attributes = new List<SkillAttribute> { SkillAttribute.normal };

    private float maxHP, maxSP;
    /* Movement Speed */
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    //public Rigidbody2D rb;
    public bool destructible = true; /* is destructible or not */
    int selectedSkill = 0; // Current Selected Skill

    public virtual void Use(Vector3 from, Vector3 to)
    {
        FindObjectOfType<SkillSystem>().Use(skills[selectedSkill], this, from, to);
    }

    public virtual void SetSelectedSkill(int i)
    {
        selectedSkill = i;
    }

    public virtual void upgradeHP(float newHP)
    {
        maxHP = newHP;
    }
    public virtual void upgradeSP(float newSP)
    {
        maxSP = sp;
    }
    public float getMaxHP()
    {
        return maxHP;
    }
    public float getMaxSP()
    {
        return maxSP;
    }

    public virtual void Start()
    {
        maxHP = hp;
        maxSP = sp;
        EnableHPRecovery(hpRecoveryEnabled);
        EnableSPRecovery(spRecoveryEnabled);
    }

    /*Enable or disable invincible */
    public virtual void setDestructible(bool destructible) {
        this.destructible = destructible;
    }

    public virtual bool isDestructible() {
        return this.destructible;
    }


    /* Get hp in % */
    public float GetRemainingHP()
    {
        return hp/maxHP;
    }
    /* Get sp in % */
    public float GetRemainingSP()
    {
        return sp/maxSP;
    }


    /* Casting from GameComponent to Physical Object */
    public static explicit operator PhysicalObject(GameObject v)
    {
        return v.GetComponent<PhysicalObject>();
    }


    /* Heal this value, return healed health*/
    public virtual float Heal(float value) {
        if (destructible)
        {
            hp += value;
            if (hp > maxHP)
            {
                hp = maxHP;
            }
        }
        return hp;
    }
    /* Damage this value, return damaged health*/
    public virtual float Damage(float value) {
        if (destructible)
        {
            hp -= value;
            if (hp < 0.0f)
            {
                hp = 0.0f;
            }
        }
        return hp;
    }

    /* Spend SP*/
    public virtual float Spend(float value)
    {
        sp -= value;
        if (sp < 0.0f)
        {
            sp = 0.0f;
        }
        return sp;
    }

    /* Recharge SP */
    public virtual float Recharge(float value)
    {
        sp += value;
        if (sp > maxSP)
        {
            sp = maxSP;
        }
        return sp;
    }

    public void EnableHPRecovery(bool on)
    {
        if(!hpRecoveryEnabled && on)
        {
            hpRecoveryEnabled = true;
            StartCoroutine(HPRecovery());
        }
        else
        {
            hpRecoveryEnabled = on;
        }
    }
    public void EnableSPRecovery(bool on)
    {
        if(!spRecoveryEnabled && on)
        {
            spRecoveryEnabled = true;
            StartCoroutine(SPRecovery());
        }
        else
        {
            spRecoveryEnabled = on;
        }
    }
    /* Natural HP and SP Recovery */

    private IEnumerator HPRecovery()
    {
        while (hpRecoveryEnabled)
        {
            Heal(hpRecovery);
            yield return new WaitForSeconds(hpRecoveryTime);            
        }
    }

    private IEnumerator SPRecovery()
    {
        while (spRecoveryEnabled)
        {
            Recharge(spRecovery);
            yield return new WaitForSeconds(spRecoveryTime);
        }
    }


    //public void PhysicalCollision()
    //{

    //}


    // Start is called before the first frame update
    //void Start()
    //{
    //    base.Start();
    //    healthBar = GetComponent<Monster>().healthBar;
    //    //hpSprite = healthBar.GetComponent<SpriteRenderer>();
    //    hp = 100.0f;
    //    //fullsize = healthBar.texture.set
    //    //healthBar.GetComponent<RectTransform>().localScale;

    //}
}

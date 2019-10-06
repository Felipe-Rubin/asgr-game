using UnityEngine;

public abstract class PhysicalObject : MonoBehaviour
{
    public float hp = 100.0f;/* Health Points */
    public float dmg = 1.0f; /* Default Damage */
    public float sp = 100.0f; /* Default Speed */
    private float maxHP, maxSP;
    /* Movement Speed */
    protected float moveSpeed = 5f;
    protected float turnSpeed = 90f;
    //public Rigidbody2D rb;
    protected bool destructible; /* is destructible or not */
    public virtual void Start()
    {
        maxHP = hp;
        maxSP = sp;
        destructible = true;
    }

    public float getMaxHP()
    {
        return maxHP;
    }
    public float getMaxSP()
    {
        return maxSP;
    }

    

    /*Enable or disable invincible */
    public virtual void setDestructible(bool destructible) {
        this.destructible = destructible;
    }
    public virtual bool isDestructible() {
        return this.destructible;
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
    /* Casting from GameComponent to Physical Object */
    public static explicit operator PhysicalObject(GameObject v)
    {
        return v.GetComponent<PhysicalObject>();
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
    /* Spend SP */
    public virtual float Spend(float value)
    {
        sp -= value;
        if (sp < 0.0f)
        {
            sp = 0.0f;
        }
        return sp;
    }
    /* Recharges SP */
    public virtual float Recharge(float value)
    {
        sp += value;
        if (sp > maxSP)
        {
            sp = maxSP;
        }
        return sp;
    }
   
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

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalObject: MonoBehaviour
{
    public float maxHP = 100.0f;
    public float hp;/* Health Points */
    
    private bool destructible; /* is destructible or not */

    /*Enable or disable invincible */
    public virtual void setDestructible(bool destructible) {
           this.destructible = destructible;
    }
    public virtual bool isDestructible() {
        return this.destructible;
    }

    /* Heal this value, return healed health*/
    public virtual float Heal(float value) {
        hp += value;
        if (hp > maxHP)
        {
            hp = maxHP;
        }
        return hp;
    }

    public static explicit operator PhysicalObject(GameObject v)
    {
        return v.GetComponent<PhysicalObject>();

        //return physicalObject;
        //throw new NotImplementedException();
    }

    /* Damage this value, return damaged health*/
    public virtual float Damage(float value) {
        hp -= value;
        if (hp < 0.0f) {
            hp = 0.0f;
        }
        return hp;
    }
}



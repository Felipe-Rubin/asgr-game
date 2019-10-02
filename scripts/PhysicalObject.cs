using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalObject: MonoBehaviour
{
    public PhysicalObject(float hp)
    {
        this.hp = hp;
        this.destructible = true;
    }

    public float hp;/* Health Points */

    private bool destructible; /* is destructible or not */

    /*Enable or disable invincible */
    public void setDestructible(bool destructible) {
           this.destructible = destructible;
    }
    public bool isDestructible() {
        return this.destructible;
    }

    /* Heal this value, return healed health*/
    public float Heal(float value) {
        hp += value;
        return hp;
    }
    /* Damage this value, return damaged health*/
    public float Damage(float value) {
        hp -= value;
        return hp;
    }

    // Start is called before the first frame update
    //    void Start()
    //  {
    //     
    //}

    // Update is called once per frame
    void Update()
    {
        if (this.hp <= 0.0) {
            Destroy(gameObject);
        }
    }
}



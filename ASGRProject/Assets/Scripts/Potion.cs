﻿using UnityEngine;
using System.Collections;

public class Potion : PhysicalObject
{
    /* Recovery Effect */
    public float hpEffect = 0.0f;
    public float spEffect = 0.0f;
    /* Damage Effect */
    public float dmgEffect = 0.0f;
    /* Speed Effect */
    public float spdEffect = 0.0f;
    private object g;
    // Use this for initialization
    public override void Start()
    {
        base.Start();
        setDestructible(false);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhysicalObject obj = (PhysicalObject)collision.gameObject;
        if (collision.gameObject.tag == "Player")
        {
            obj.Heal(hpEffect);
            obj.Recharge(spEffect);
            Destroy(gameObject);
        }
    }
    
    void FixedUpdate()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }
}

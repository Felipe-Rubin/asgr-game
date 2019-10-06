using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PhysicalObject
{
    // Start is called before the first frame update
    
    public override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PhysicalObject obj = (PhysicalObject)collision.gameObject;
        //obj.Damage(dmg);
        //Destroy(gameObject);
    }

}

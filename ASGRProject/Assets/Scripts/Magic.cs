using UnityEngine;
using System.Collections;
using System;

public class Magic : PhysicalObject
{
    // Use this for initialization
    private bool player_casted = false;
    public override void Start()
    {
        base.Start();
        setDestructible(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setPlayerOwnership(bool owned_by_player)
    {
        player_casted = owned_by_player;
    }

    public bool getPlayerOwnership()
    {
        return player_casted;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Collision!");
        bool ownership = getPlayerOwnership();
        print("Player Owned: " + ownership);
        PhysicalObject obj = (PhysicalObject)collision.gameObject;
        if (collision.gameObject.tag != "projectile")
        {
            if (ownership)
            {
                if (collision.gameObject.tag != "Player")
                {
                    obj.Damage(dmg);
                    Destroy(gameObject);
                }
            }
            else
            {
                if (collision.gameObject.tag == "Player")
                {
                    obj.Damage(dmg);
                    Destroy(gameObject);
                }
            }
        }

    }
}

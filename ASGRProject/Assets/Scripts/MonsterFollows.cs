using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterFollows : MonoBehaviour
{
    public PhysicalObject me;
    private Player player;
    private bool followPlayer = false;
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            followPlayer = true;
        }
    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            followPlayer = false;
        }

    }
    // Update is called once per frame
    void Update()
    {
        if (followPlayer)
        {
            me.transform.position = Vector3.MoveTowards(me.transform.position,player.transform.position,me.moveSpeed*Time.deltaTime);
        }        
    }
}

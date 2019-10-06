using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : PhysicalObject
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public float sp = 100.0f;
    //public float hp = 100.0f;

    public Rigidbody2D rb;
    public Image spBar;
    public Image hpBar;
    public Camera cam;


    //public List<GameObject>

    //public GameObject player;

    // Start is called before the first frame update


    /*
    void Camera::computeforward()
    {
        *xPos+= vx * *speed;
        *zPos+= vz * *speed;
    }
    void Camera::computebackwards()
    {
        *xPos+= vx * -*speed;
        *zPos+= vz * -*speed;

    }
    void Camera::computeleft()
    {
        *xPos+= *speed * sin(vz);
        *zPos+= *speed * -sin(vx);

    }
    void Camera::computeright()
    {
        *xPos-= *speed * sin(vz);
        *zPos-= *speed * -sin(vx);
    }
    */
    private Vector2 direction;
    //private Vector2 movement;
    private Vector2 mousePos;
    private bool lr = true;

    public int look_right = 1;


    void Start()
    {
        //xprev = Input.mousePosition.x;
        //yprev = Input.mousePosition.y;
        //hp = 50.0f; // Physical Object
        //rb = GetComponent<Rigidbody2D>();
        //Console.Write("update");
    }


    void FixedUpdate()
    {
        /* Start bars at 100% */
        hpBar.fillAmount = hp / maxHP;
        spBar.fillAmount = sp / 100.0f;

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        rb.position += movement;
        

        //transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        //cam.transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
            
        //Vector2 lookDir = mousePos - rb.position;

        //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        ////print("Angle: " + angle);
        //if (Mathf.Abs(angle) < 90.0f && !lr)
        //{
        //    //transform.Rotate(180, 0, 0);
        //    lr = true;
        //    //print("Looking Right");
        //}
        //else if (Mathf.Abs(angle) < 180.0f && Mathf.Abs(angle) > 90.0f && lr )
        //{
        //    //transform.Rotate(180, 0, 0);
        //    lr = false;
        //    //print("Looking Left");
        //}



        //rb.rotation = angle; // Rotates Camera


        //if (Input.GetKey("x"))
        //{

        //    hp -= 10.0f;
        //}


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PhysicalObject obj = (PhysicalObject)collision.gameObject;
        obj.Damage(dmg);
        //currentHp.x = obj.Damage(dmg) / 400.0f;
        //print("Colliding");
    }
    // Update is called once per frame
    void Update()
    {
    }
}





























using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public float expPoints = 30.0f;
    public float hp = 100.0f;
    public float sp = 100.0f;
    public Rigidbody2D rb;
    public Image spBar;
    public Image hpBar;
    

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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        direction = Vector2.zero;
        //Console.Write("update");
    }
    void FixedUpdate()
    {
        /* Start bars at 100% */
        hpBar.fillAmount = hp / 100.0f;
        spBar.fillAmount = sp / 100.0f;

        transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        //if (Input.GetKey("x"))
        //{

        //    hp -= 10.0f;
        //}



    }
    private void OnCollisionEnter(Collision collision)
    {
        print("Colliding");
    }
    // Update is called once per frame
    void Update()
    {

    }
}

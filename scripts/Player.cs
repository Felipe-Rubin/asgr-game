using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float turnSpeed = 90f;
    public Rigidbody2D rb;
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

    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
    }
}

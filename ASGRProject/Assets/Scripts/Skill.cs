using UnityEngine;
using System.Collections;
using System;

public class Skill : MonoBehaviour
{
    public GameObject projectile;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public static explicit operator Skill(GameObject v)
    {
        return v.GetComponent<Skill>();
    }
}

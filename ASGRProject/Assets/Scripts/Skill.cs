using UnityEngine;
using System.Collections;
using System;

// Skill.cast -> Magic.execute
public class Skill : MonoBehaviour
{
    public Magic effect;
    public float cooldown; // Cool Down to cast again
    private float cost; // Skill Cost
    // Use this for initialization
    private float lastcast = 0.0f; // Current Cooldown, last time it was casted.
    private GameObject caster = null; // Who casts this skill
    void Start()
    {
        lastcast = 0.0f;
    }
    public GameObject getCaster()
    {
        return caster;
    }
    public void setCaster(GameObject newcaster)
    {
        caster = newcaster;
    }

    private void FixedUpdate()
    {
        SpriteRenderer s = GetComponentInChildren<SpriteRenderer>();
        if (on_cooldown())
        {
            
            s.color = new Color(s.color.r, s.color.g, s.color.b, 0.5f);
        }
        else
        {
            s.color = new Color(s.color.r, s.color.g, s.color.b, 1.0f);
        }
    }


    public bool on_cooldown()
    {
        float nowtime = Time.realtimeSinceStartup;
        if (lastcast <= 0.0f ){
            return false;
        } else
        {
            if ((nowtime - lastcast) > cooldown)
            {
                return false;
            }
        }
        return true;
    }
    

    // Update is called once per frame
    void Update()
    {
    }
    /* Cast this skill and start the cooldown */
    public void cast()
    {
        if (!on_cooldown())
        {
            //effect.execute();
            
            if(caster == null)
            {
                Magic mg = Instantiate(effect, new Vector3(0, 0, 0), Quaternion.identity);
            }
            else
            {
                Magic mg = Instantiate(effect, caster.transform.position, Quaternion.identity);
                mg.setPlayerOwnership(true);
            }
            
            // DO EFFECT HERE
            lastcast = Time.realtimeSinceStartup;
        }
        
    }

    public static explicit operator Skill(GameObject v)
    {
        return v.GetComponent<Skill>();
    }
}

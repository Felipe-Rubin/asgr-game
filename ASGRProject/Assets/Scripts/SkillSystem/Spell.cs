using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[System.Serializable]
[System.Serializable]
public class Spell : Skill
{
    public ParticleSystem effect;
    public SkillAoE aoe = SkillAoE.Target;
    public float velocity;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
    }

    public override void cast()
    {
        base.cast();
        if (state == SkillState.activated)
        {
            if(aoe == SkillAoE.Target)
            {
                Vector3 direction = to - from;
                rb.AddForce(direction*velocity,ForceMode2D.Impulse);
            }
            else
            {
                rb.transform.position = to;
            }
            effect.Play();
        }

        // TO DO 
    }

    public override void activated()
    {
        if (!effect.isEmitting || hitStop)
        {
            //print("Spell Stopping");
            effect.Stop(); // maybe not necessary
            state = SkillState.completed;
        }
        else
        {
            //print("Skill "+name+" isAlive: "+effect.IsAlive() +" hitStop: "+hitStop);
        }
    }



    //// Update is called once per frame
    public override void Update()
    {
        base.Update();
        //Debug.Log("Spell " + name + " state: " + state);
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;



//public class SkillEditor: Editor
//{
//    public override void OnInspectorGUI()
//    {
//        DrawDefaultInspector();


//    }
//}

//[System.Serializable]
// public class Skill : Item {  
//     public int weaponDamage;
// }


//[CustomEditor()]
//[CustomEditor(typeof(Skill), true)]
public class Skill : PhysicalObject
{
    private AudioSource audioSource;
    public AudioClip castingAudio;
    public AudioClip activeAudio;
    public AudioClip hitAudio;

    //public float hpCost = 0f;
    //public float spCost = 0f;
    public float baseDMG = 0f;
    public float castTime = 0f;
    public float coolDown = 0f;
    public SkillType type = SkillType.active;
    public SkillState state = SkillState.ready;
    public SkillModel model = SkillModel.discrete;
    public SkillSchool school = SkillSchool.Spell;
    protected PhysicalObject user = null;
    private float ctEnd;
    private string skillName;
    public Vector3 from;
    public Vector3 to;
    protected bool hitStop = false;



    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();

        //audioSource = new AudioSource
        //{
        //    spread = 360,
        //    volume = 0.5f,
        //    clip = activeAudio,
        //    playOnAwake = false,
        //    loop = true
        //};

        //setDestructible(false);
    }

    private void collide(Collider2D collision)
    {
        PhysicalObject collidedAt = (PhysicalObject)collision.gameObject;
        if(collidedAt != user)
        {
            //float attrmod = AttributeMultiplier(attributes,collidedAt.attributes);
            float attrmod = 1;
            collidedAt.Damage((atk-collidedAt.def) * attrmod);
            if (model == SkillModel.discrete) // Discrete: Stop After 1 hit
            {
                hitStop = true;
            }
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        collide(collision);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        collide(collision);
    }


    public float AttributeMultiplier(List<SkillAttribute> src,List<SkillAttribute> dst)
    {
        // Here put according to monster type
        return 1.0f;
    }

    public PhysicalObject GetUser()
    {
        return user;
    }

    public string GetSkillName()
    {
        return skillName;
    }
    public void setSkillName(string s)
    {
        skillName = s;
    }

    public void Use(PhysicalObject user,Vector3 from,Vector3 to)
    {
        this.user = user;
        this.from = from;
        this.to = to;
        atk = user.atk + baseDMG;
        //Calculate Damage Here

        if(castTime > 0f)
        {
            // Add Here Casting Buffer Above Player
        }
        state = SkillState.cast;
        ctEnd = Time.realtimeSinceStartup+castTime;
    }

    // This is overrided on Spell and Technique
    public virtual void activated()
    {
        if(activeAudio != null)
        {
            audioSource.Play();
        }
        state = SkillState.completed;
    }

    public virtual void completed()
    {
        state = SkillState.cooldown;
    }


    public virtual void cast()
    {

        if(( Time.realtimeSinceStartup/ctEnd ) >= 1f)
        {
            // Remove Casting Buffer
            // Execute it
            state = SkillState.activated;
        }
        // TO DO 
    }

    // Update is called once per frame
    public virtual void Update()
    {
        switch (state) {
            case SkillState.cast: cast();break;
            case SkillState.activated: activated();break;
            case SkillState.completed: completed(); break;
            case SkillState.cooldown: FindObjectOfType<SkillSystem>().cooldown(this);break;
        }
    }
}

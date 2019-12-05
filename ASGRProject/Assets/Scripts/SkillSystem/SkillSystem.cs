using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;


[Serializable]
[CanEditMultipleObjects]
[CustomPropertyDrawer(typeof(SpriteRenderer))]
public class SkillSystem : MonoBehaviour
{

[Serializable]
[CanEditMultipleObjects]
[AddComponentMenu("Transform/Follow Transform")]
public struct SkillEntry
{
    public GameObject skill; // Prefab
    public Sprite icon;
    public string name;
    public float spCost;
    public float hpCost;
}

    public struct CDEntry
    {
        public string name;
        public PhysicalObject user;
        public float end; // Time it should end
        public CDEntry(string name, PhysicalObject user, float end)
        {
            this.name = name;
            this.user = user;
            this.end = end;
        }
    }
    
    public List<SkillEntry> availableSkills = new List<SkillEntry>();

    private Dictionary<string,SkillEntry> skills; // Dict formatted Skills
    private Dictionary<PhysicalObject,List<string>> backlog; // Waiting for skill end
    private List<CDEntry> onCD; // On Cooldown List
    private UIOverlay uiOverlay;
    private Player player;
    public bool userLimitation = true;

    // Start Skill Execution process
    public void Use(string name, PhysicalObject user,Vector3 from, Vector3 to)
    {
        if (!backlog.ContainsKey(user))
        {
            backlog.Add(user, new List<string>());
        }

        SkillEntry sk = skills[name];

        //MAYBE TO DO: test skill requirements

        if ( (userLimitation && backlog[user].Contains(name)) || sk.spCost > user.sp || sk.hpCost >= user.hp)
        {
            print("Not enough Resources");
            return;
        }

        // Pay Casting Cost in Advance
        user.sp -= sk.spCost;
        user.hp -= sk.hpCost;
        Skill s = (Skill)Instantiate(sk.skill, user.transform.position, Quaternion.identity);
        backlog[user].Add(name);
        s.setSkillName(name);
        s.Use(user,from,to);
        //create skill instance
        //instance.cast
        //backlog.add(user,instance)
        //
    }


    public GameObject GetSkillIcon(string name)
    {
        if (skills.ContainsKey(name))
        {
            GameObject go = new GameObject();
            SpriteRenderer r = go.AddComponent<SpriteRenderer>();
            r.sprite = skills[name].icon;
            r.drawMode = SpriteDrawMode.Sliced;
            return go;
        }
        return null;
    }


    public void cooldown(Skill skill)
    {
        if (skill.coolDown > 0f)
        {
            
            if(skill.GetUser() == player)
            {
                uiOverlay.SetSkillCooldown(skill.GetSkillName());
            }
            CDEntry e = new CDEntry(skill.GetSkillName(), skill.GetUser(), skill.coolDown + Time.realtimeSinceStartup);
            for (int i = 0; i < onCD.Capacity; i++)
            {
                if(onCD[i].end > e.end){
                    onCD.Insert(i,e);
                }
            }
        }
        else
        {
            backlog[skill.GetUser()].Remove(skill.GetSkillName());
        }

        Destroy(skill);
    }
    public bool Ready(CDEntry e)
    {
        if(Time.realtimeSinceStartup > e.end)
        {
            backlog[e.user].Remove(name);
            return true;
        }
        return false;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        uiOverlay = FindObjectOfType<UIOverlay>();
        player = FindObjectOfType<Player>();
        skills = new Dictionary<string, SkillEntry>();
        backlog = new Dictionary<PhysicalObject, List<string>>();
        onCD = new List<CDEntry>();

        foreach (SkillEntry sk in availableSkills)
        {
            skills.Add(sk.name,sk);
		}
    }

    // Update is called once per frame
    void Update()
    {
        
        while(onCD.Capacity > 0 && Ready(onCD[0]))
        {
            onCD.RemoveAt(0);
        }
    }
}

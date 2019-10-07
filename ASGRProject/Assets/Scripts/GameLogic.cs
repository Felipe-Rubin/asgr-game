using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameLogic : MonoBehaviour
{

    public Image spBar;
    public Image hpBar;
    public Text fps;
    public Text stageText;
    //public Monster mvp;
    public int nmonsters; // How many
    /* Prefabs */
    public GameObject[] minionPrefab; // Monster Prefab
    public GameObject[] potionPrefab; // Potion Prefab
    public GameObject[] skillPrefab; // Skills Prefab
    private List<Skill> skill_list;
    public float dropRate = 0.5f;

    /* Tile Maps */
    public Tilemap map1;

    /* Game Cameras */
    //public Camera minicam;
    //public Camera maincam;

    /* Player */
    public Player player;
    
    /*Spawn initial monster enemies*/
    private void spawn_monsters()
    {
        /* Create Minion Monsters */
        float x, y = 0.0f;
        for (int i = 0; i < nmonsters; i++)
        {
            int rng = Random.Range(0, minionPrefab.Length);
            x = Random.Range(map1.cellBounds.xMin+0.0f, map1.cellBounds.xMax);
            y = Random.Range(map1.cellBounds.yMin+0.0f, map1.cellBounds.yMax);
            GameObject minion = Instantiate(minionPrefab[rng], new Vector3(x, y, 0), Quaternion.identity);
            minion.tag = "minion";
        }

        /*Create Random Items*/
    }
    // Note: Random.Range returns int if args are int,if args are float
    // then it returns float.

    public void monster_killed(Monster m)
    {
        bool rng = Random.Range(0.0f, 1.0f) < dropRate ? true : false;
        Vector3 pos = m.transform.position;
        Destroy(m.gameObject, 0);
        if (rng)
        {
            int rng2 = Random.Range(0, potionPrefab.Length);

            Instantiate(potionPrefab[rng2], pos, Quaternion.identity);
        }
        
    }

    public void initialize_skills()
    {
        for(int i = 0; i < skillPrefab.Length; i++)
        {
            Skill sk = (Skill)Instantiate(skillPrefab[i], new Vector3(0,0, 0), Quaternion.identity);
            sk.setCaster(player.gameObject);
            skill_list.Add(sk);
        }
    }

    /* FPS Calculation
       From Unity Docs: Time.realtimeSinceStartup
    */
    public float updateInterval = 0.5F;
    private double lastInterval;
    private int frames = 0;
    private float fps_value;

    void Start()
    {
        skill_list = new List<Skill>();
        initialize_skills();
        spawn_monsters();

        // Related to fps
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;
    }
    

    
    void FixedUpdate()
    {
        hpBar.fillAmount = player.hp / player.getMaxHP();
        spBar.fillAmount = player.sp / player.getMaxSP();

        stageText.text = "Remaining Enemies: " + GameObject.FindGameObjectsWithTag("minion").Length;
        fps.text = "FPS: " + fps_value;
        Vector2 mv = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.Move(mv);

        //print("MousePos: "+Input.mousePosition);
        if (Input.GetButton("Fire1"))
        {

            int selected_skill = 0;
            skill_list[selected_skill].cast();

            //Projectile pj = skillPrefab[selected_skill].GetComponent<Skill>().;
            //Vector3 pjPos = player.transform.position;
            //pjPos.x += 5;
            //GameObject pj = Instantiate(, pjPos, Quaternion.identity);

            //.setPlayerOwnership(true);
            //print(pj.GetComponent<Projectile>().GetPlayerOwnership());

            //pj.GetComponent<Rigidbody2D>().AddForce(player.transform.forward);
            //Vector3 lookDir = Input.mousePosition - player.transform.position;
            //float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            //print("Player"+player.transform.position+" MousePos: " + angle);
            ////Instantiate(skillPrefab[0].GetComponent<Skill>().projectile,player.transform.position, Quaternion.identity);
            //Instantiate(skillPrefab[0].GetComponent<Skill>().projectile, Input.mousePosition, Quaternion.identity);
        }


    }

    private void fps_increment()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + updateInterval)
        {
            fps_value = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }

    }
    // Update is called once per frame
    void Update()
    {
        fps_increment();
        
    }
}


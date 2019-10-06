using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameLogic : MonoBehaviour
{

    public Image spBar;
    public Image hpBar;

    public Text stageText;
    //public Monster mvp;
    public int nmonsters; // How many
    /* Prefabs */
    public GameObject[] minionPrefab; // Monster Prefab
    public GameObject[] potionPrefab; // Potion Prefab
    public GameObject[] skillPrefab; // Skills Prefab
    public float dropRate = 0.5f;

    /* Tile Maps */
    public Tilemap map1;

    /* Game Cameras */
    //public Camera minicam;
    //public Camera maincam;

    /* Player */
    public Player player;
    private int remaining_minions;
    
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
        print("Called");
        bool rng = Random.Range(0.0f, 1.0f) < dropRate ? true : false;
        Vector3 pos = m.transform.position;
        Destroy(m.gameObject, 0);
        if (rng)
        {
            int rng2 = Random.Range(0, potionPrefab.Length);

            Instantiate(potionPrefab[rng2], pos, Quaternion.identity);
        }
        
    }

    void Start()
    {
        spawn_monsters();
    }
    void FixedUpdate()
    {
        hpBar.fillAmount = player.hp / player.getMaxHP();
        spBar.fillAmount = player.sp / player.getMaxSP();

        stageText.text = "Remaining Enemies: " + remaining_minions;
        Vector2 mv = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.Move(mv);


        if (Input.GetButton("Fire1"))
        {
            Instantiate(skillPrefab[0].GetComponent<Skill>().projectile,player.transform.position, Quaternion.identity);
        }

    }


    // Update is called once per frame
    void Update()
    {

        remaining_minions = GameObject.FindGameObjectsWithTag("minion").Length;
    }
}


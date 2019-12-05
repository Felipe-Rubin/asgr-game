using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using System;

public class GameLogic : MonoBehaviour
{
    public UIOverlay uIOverlay;
    public Texture2D cursorTexture;

    //public Monster mvp;
    public int nmonsters; // How many
    
    public GameObject mvpPrefab;
    private bool mvpSpawned = false;

    public AudioClip levelAudioClip;
    public AudioClip mvpAudioClip;
    public AudioSource levelAudioSource;


    /* Prefabs */
    public GameObject[] minionPrefab; // Monster Prefab
    public GameObject[] potionPrefab; // Potion Prefab
    public GameObject[] skillPrefab; // Skills Prefab
    public float dropRate = 0.5f;
    private SkillSystem skillSystem;

    /* Tile Maps */
    //public Tilemap activeMap;
    private GameMap activeMap;

    public GameMap[] Maps;

    /* Game Cameras */
    //public Camera minicam;

    public Camera maincam;
    /* Player */
    public Player player;

    //public int startLevel = 0;
    
    void Start()
    {
        // Add game map here
        int rng = UnityEngine.Random.Range(0, Maps.Length);
        activeMap = Instantiate(Maps[rng], new Vector3(0, 0, 0), Quaternion.identity);

        minionPrefab = activeMap.minionPrefab;

        Debug.Log("RNG GOT "+rng);
        
        //activeMap = FindObjectOfType<GameMap>();
        skillSystem = FindObjectOfType<SkillSystem>();

        spawn_monsters();
        configure_cursor();

        Vector3 cam_start_pos = maincam.transform.position;
        cam_start_pos.x = player.transform.position.x;
        cam_start_pos.y = player.transform.position.y;
        maincam.transform.position = cam_start_pos;

        levelAudioSource = GetComponent<AudioSource>();
        levelAudioSource.clip = levelAudioClip;
    }

    private T FindObjectsOfTypeIncludingAssets<T>()
    {
        throw new NotImplementedException();
    }

    public void OnPlayerDeath()
    {
        SceneManager.LoadScene("Lose");
    }

    public void OnBossDefeat()
    {
        SceneManager.LoadScene("Win");
    }
    
    /*Spawn initial monster enemies*/
    private void spawn_monsters()
    {
        /* Create Minion Monsters */
        int x, y;
        for (int i = 0; i < nmonsters; i++)
        {
            int rng = UnityEngine.Random.Range(0, minionPrefab.Length);
            //x = Random.Range(activeMap.GetBounds().xMin+0.0f, activeMap.GetBounds().xMax);
            //y = Random.Range(activeMap.GetBounds().yMin+0.0f, activeMap.GetBounds().yMax);
            x = UnityEngine.Random.Range(activeMap.GetBounds().xMin+5, activeMap.GetBounds().xMax-5);
            y = UnityEngine.Random.Range(activeMap.GetBounds().yMin+5, activeMap.GetBounds().yMax-5);
            GameObject minion = Instantiate(minionPrefab[rng], new Vector3(x, y, 0), Quaternion.identity);
            minion.tag = "minion";
        }
        /*Create Random Items*/
    }
    // Note: Random.Range returns int if args are int,if args are float
    // then it returns float.

    public void monster_killed(Monster m)
    {

        bool rng = UnityEngine.Random.Range(0.0f, 1.0f) < dropRate ? true : false;
        bool shouldSpawnMVP = false;

        if((GameObject.FindGameObjectsWithTag("mvp").Length == 0) && (GameObject.FindGameObjectsWithTag("minion").Length + GameObject.FindGameObjectsWithTag("miniboss").Length) == 1){
            shouldSpawnMVP = true;
        }

        Vector3 pos = m.transform.position;
        string mtag = m.gameObject.tag;

        Destroy(m.gameObject, 0);

        if (rng)
        {
            int rng2 = UnityEngine.Random.Range(0, potionPrefab.Length);

            Instantiate(potionPrefab[rng2], pos, Quaternion.identity);
        }

        if(shouldSpawnMVP)
        {
            uIOverlay.StartBossAnimation();
            levelAudioSource.clip = mvpAudioClip;
            levelAudioSource.Play();

        }

        if (mtag == "mvp")
        {
            OnBossDefeat();
        }
    }

    public void spawn_mvp()
    {
        
        float x = UnityEngine.Random.Range(activeMap.GetBounds().xMin+0.0f, activeMap.GetBounds().xMax);
        float y = UnityEngine.Random.Range(activeMap.GetBounds().yMin+0.0f, activeMap.GetBounds().yMax);
        Monster mvp = (Monster)Instantiate(mvpPrefab, new Vector3(x, y, 0), Quaternion.identity);

        //mvp.upgradeSP(mvp.sp*1.5f);
        //mvp.upgradeHP(mvp.hp*10f);
        //mvp.atk += 5;
        //mvp.def = 1;
        mvp.ReplaceHealthBar(uIOverlay.mvpBar);
        //mvp
        mvp.tag = "mvp";
    }


    public void configure_cursor()
    {
        //Vector2 cursorHotspot = new Vector2 (cursorTexture.width / 2, cursorTexture.height / 2);

        //Cursor.SetCursor(cursorTexture,cursorHotspot, CursorMode.Auto);
        //Cursor.visible = true;
        //Cursor.lockState = CursorLockMode.Confined;

    }



    void CameraMouse() {

        Vector3 mp = maincam.ScreenToWorldPoint(Input.mousePosition);
        //player.transform.forward * Vector3()
        Debug.DrawLine(player.transform.position, mp, Color.yellow);

        player.transform.rotation = Quaternion.LookRotation(transform.forward, player.transform.position-mp);

    }

    //public Vector3 lt = new Vector3(0.0f, 0.0f, 0.0f);
    void FixedUpdate()
    {
        /* Scene Administration */
        if(SceneManager.GetActiveScene().name == "Lose")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Level1");
            }
        }else if(SceneManager.GetActiveScene().name == "Win")
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Level1");
            }
            
        }
        else /* On Level 1*/
        {
            Vector2 mv = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            player.Move(mv);
            Vector3 from = player.transform.position;
            Vector3 to = maincam.ScreenToWorldPoint(Input.mousePosition);


            CameraMouse();

            if(Input.anyKey)
            {
                string inputStr = Input.inputString;
                if(inputStr.Length > 0)
                {
                    char input = inputStr.ToCharArray()[0];
                    if(input >= '0' && input <= '9')
                    {
                        if(input == '0')
                        {
                            player.SetSelectedSkill(10);
                            
                        }
                        else
                        {
                            player.SetSelectedSkill((int)char.GetNumericValue(input)-1);
                        }
                        player.Use(from, to);

                    }
                }
            }

            if(Input.GetMouseButtonDown(0))
            {
                //
            }

        }



    }

    
    // Update is called once per frame
    void Update()
    {

    }
}


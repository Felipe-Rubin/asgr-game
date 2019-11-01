using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameLogic : MonoBehaviour
{
    public Texture2D cursorTexture;
    public Image spBar;
    public Image hpBar;
    public Text fps;
    public Text stageText;
    public Text coordPos;
    //public Monster mvp;
    public int nmonsters; // How many
    /* Prefabs */
    public GameObject[] minionPrefab; // Monster Prefab
    public GameObject[] potionPrefab; // Potion Prefab
    public GameObject[] skillPrefab; // Skills Prefab
    private List<Skill> skill_list;
    public float dropRate = 0.5f;

    public GameObject skillPanel;

    /* Tile Maps */
    public Tilemap map1;

    /* Game Cameras */
    //public Camera minicam;

    public Camera maincam;

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
            //Skill sk = (Skill)Instantiate(skillPrefab[i], new Vector3(0,0, 0), Quaternion.identity);

            //Ok but on Game Scene.
            //Skill sk = (Skill)Instantiate(skillPrefab[i]);
            //sk.setCaster(player.gameObject);
            //skill_list.Add(sk);


            // On UI:
            GameObject sk = Instantiate(skillPrefab[i]);
            sk.GetComponent<Skill>().setCaster(player.gameObject);
            skill_list.Add(sk.GetComponent<Skill>());
            sk.transform.SetParent(skillPanel.transform, false);
            //To set the position and function
            //sk.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, (i * -289.5f));// <-- This positioning does not position with screen the size


        }
    }

    public void configure_cursor()
    {
        Cursor.visible = true;
        //Cursor.SetCursor()
        Cursor.lockState = CursorLockMode.Confined;

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
        configure_cursor();

        // Related to fps
        lastInterval = Time.realtimeSinceStartup;
        frames = 0;

        Vector3 aux = Input.mousePosition;
        Vector3 realmp = maincam.ScreenToWorldPoint(new Vector3(aux.x, aux.y, maincam.nearClipPlane));
        oldpos = realmp;
    }



    /*
        WAYS TO ROTATE:
        transform.eulerAngles = Vector3.forward * degrees;
    // or
    transform.rotation = Quaternion.Euler(Vector3.forward * degrees);
    // or
    transform.rotation = Quaternion.LookRotation(Vector3.forward, yAxisDirection);
    // or
    transform.LookAt(Vector3.forward, yAxisDirection);
    // or
    transform.right = xAxisDirection;
    // or
    transform.up = yAxisDirection;

    */
    public float currentV = 0.0f;
    public Vector3 oldpos;
    void CameraMouse() {

        Vector3 mp = maincam.ScreenToWorldPoint(Input.mousePosition);
        //player.transform.forward * Vector3()
        Debug.DrawLine(player.transform.position, mp, Color.yellow);

        player.transform.rotation = Quaternion.LookRotation(transform.right, player.transform.position-mp);

        Debug.Log("Rotation: " + player.transform.rotation+" Player Forward:"+player.transform.right + " Player Right:" + player.transform.right);

        // player.transform.rotation = Quaternion.LookRotation(Vector3.forward, mpw, DELTA TIME);

        
        //player.transform.Rotate()
        //Debug.Log("MP[S:"+mp+ ",W:" + maincam.ScreenToWorldPoint(mp) + ",R:" +maincam.ViewportPointToRay(mp)+"]   P[p:"+player.transform.position+",r:" + player.transform.rotation + ",u:" + player.transform.up+",f:"+ player.transform.forward+",e:"+ player.transform.eulerAngles+",S:"+maincam.WorldToScreenPoint(player.transform.position) +"]");

        //player.transform.rotation = new Quaternion()
        //float angle = 3.0f;

        //Quaternion.FromToRotation

        //float angle = Vector3.Angle(player.transform.eulerAngles)
        //float angle = Vector3.Angle(player.transform.position, maincam.ScreenToWorldPoint(mp));

        //print("Angle: " + angle);
        //Vector3.rad

        //player.transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, 1));
        //player.transform.rotation = Quaternion.AngleAxis(angle, player.transform.eulerAngles);

        //Screenspace is defined in pixels.
        //The bottom-left of the screen is (0,0);
        //the right-top is (pixelWidth,pixelHeight).
        //The z position is in world units from the camera

        //Vector3 mp = Input.mousePosition;

        //Vector3 realmp = maincam.ScreenToWorldPoint(new Vector3(mp.x, mp.y, maincam.nearClipPlane));
        //Ray castPoint = Camera.main.ScreenPointToRay(realmp);
        //print("Screen2World: " + realmp + " Screen2Ray: " + castPoint);


        //Vector3 cp = maincam.WorldToScreenPoint(player.transform.position);
        //Vector3 mp = Input.mousePosition - cp;
        //float angleoff = (Mathf.Atan2(player.transform.right.y,player.transform.right.x) - Mathf.Atan2(mp.y, mp.x)) * Mathf.Rad2Deg;
        //print("Angle Offset is:" + angleoff);        

        //player.transform.Rotate(new Vector3(0,0,currentAngle-angleoff)*0.1f);

        //currentAngle = angleoff;



        //player.transform.position


        //Vector3 aux = Input.mousePosition;
        //Vector3 realmp = maincam.ScreenToWorldPoint(new Vector3(aux.x, aux.y, maincam.nearClipPlane));

        ////print("Aux: "+ aux);
        //Vector2 mp = new Vector2(realmp.x, realmp.y);

        //print(mp+ " > " + oldpos);
        //float angle = VMath.Instance.Angle(oldpos, mp);
        //oldpos = mp;

        //player.transform.eulerAngles = Vector3.forward * angle;
        //player.transform.eulerAngles = Vector3.forward * -Vector3.Angle(oldpos, realmp);
        //player.transform.LookAt(new Vector3(00, realmp.x));
        //float speed = 5f;
        //Vector2 direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        //print("Such Angle: " + angle);
        //Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //player.transform.rotation = Quaternion.Slerp (transform.rotation, rotation, speed * Time.deltaTime);
    }

    //public Vector3 lt = new Vector3(0.0f, 0.0f, 0.0f);
    void FixedUpdate()
    {
        hpBar.fillAmount = player.hp / player.getMaxHP();
        spBar.fillAmount = player.sp / player.getMaxSP();

        coordPos.text = "World: (" +
           player.transform.position.x + "," +
           player.transform.position.y + "," +
           player.transform.position.z + ")";


        stageText.text = "Remaining Enemies: " + GameObject.FindGameObjectsWithTag("minion").Length;
        fps.text = "FPS: " + Mathf.RoundToInt(fps_value);
        
        Vector2 mv = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.Move(mv);


        CameraMouse();
        //print("Mouse Position at:"+Input.mousePosition);
        //Vector3 ltnow = Input.mousePosition;

        //player.GetComponent<Rigidbody2D>().MoveRotation(Vector3.Angle(lt, ltnow));

        //print("Mouse Angle: " + Vector3.Angle(lt, ltnow));
        //lt = ltnow;
        //print("Mouse Position" + Input.mousePosition.normalized);


        //print("MousePos: "+Input.mousePosition);
        if (Input.GetButton("Fire1"))
        {
            int selected_skill = 0;
            skill_list[selected_skill].cast();

            
            // Testing 
            //player.gameObject.GetComponent<Rigidbody2D>().AddForce(mv*3.0f);


        }

        //Input.GetAxisRaw(Input.mousePosition.)


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


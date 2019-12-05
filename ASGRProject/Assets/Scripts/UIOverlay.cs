using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIOverlay : MonoBehaviour
{
    private Player player;
    public Image spBar;
    public Image hpBar;
    public Text fps;
    public Text stageText;
    public Text coordPos;
    public GameObject skillPanel;
    private Dictionary<string, int> skills = new Dictionary<string, int>();
    public Image mvpBar;
    public Text bossWarning;

    private SkillSystem skillSystem;

    /* FPS Calculation
       From Unity Docs: Time.realtimeSinceStartup
    */
    private float updateInterval = 0.5f;
    private double lastInterval;
    private int frames = 0;
    private float fps_value;

    // Start is called before the first frame update
    void Start()
    {
        mvpBar.fillAmount = 0f;
        Color c = bossWarning.color;
        c.a = 0f;
        bossWarning.color = c;

        player = FindObjectOfType<Player>();
        //player = (Player)GameObject.FindGameObjectWithTag("Player");
        skillSystem = FindObjectOfType<SkillSystem>();
        //GetComponent<SkillSystem>();
        // Related to fps

        lastInterval = Time.realtimeSinceStartup;
        frames = 0;



    }
    //private void OnGUI()
    //{
    //    m_Fading = GUI.Toggle(new Rect(0, 0, 100, 30), m_Fading, "Fade In/Out");
    //}

    private IEnumerator fillBarAnimation()
    {
        //Fade In
        for (int j = 1; j <= 10; j++){
            bossWarning.color = new Color(bossWarning.color.r, bossWarning.color.g,bossWarning.color.b,j*0.1f);
            //Debug.Log("Animation Color Up: "+bossWarning.color.a);
            yield return new WaitForSeconds(0.2f);
        }

        //Wait a bit
        yield return new WaitForSeconds(0.5f);

        //Fade Out
        for (int j = 9; j >= 0; j--){
            bossWarning.color = new Color(bossWarning.color.r, bossWarning.color.g, bossWarning.color.b,j*0.1f);
            yield return new WaitForSeconds(0.2f);
        }

        // Fill HP Bar
        for (int i = 0; i < 100; i++) {
            mvpBar.fillAmount = mvpBar.fillAmount + 0.01f;
            yield return new WaitForSeconds(0.005f);
        }

        // Change Song

        //Spawn MVP
        FindObjectOfType<GameLogic>().spawn_mvp();

    }

    public void StartBossAnimation()
    {
        //fillBarAnimation();
        StartCoroutine(fillBarAnimation());
    }

    public void AddToSkillPanel(string s, GameObject icon)
    {
        //skill.gameObject.transform.SetParent..

        //GameObject
        icon.transform.SetParent(skillPanel.transform, false);
        int idx = icon.gameObject.transform.GetSiblingIndex();
        //print("UIOverlay: Adding " + s + " on index " + idx + " with icon " + icon);
        skills.Add(s, idx);
        //sk.transform.SetParent(skillPanel.transform, false);
    }

    public void SetSkillCooldown(string s)
    {
        int idx = skills[s]; // Skill index on panel;
        Transform icon = skillPanel.transform.GetChild(idx);
        cooldownAnimation(icon);
    }

    private void cooldownAnimation(Transform icon)
    {
        // Animation Here
        //Animation Steps: https://answers.unity.com/questions/635414/cooldown-effect-in-a-button.html
        //1.Create Image as a child for an icon, stretch it so it covers your icon.
        //2.Add sprite with alpha.
        //3.Set image type to filled.
        //4.Set fill method to Radial 360.
        //5.Play with fill origin, fill amount and clockwise.
        //6.If you change fill method, then you can differend cooldown types (vertical, horizontal etc.).
        // Completed Animation
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

    private void FixedUpdate()
    {
        hpBar.fillAmount = player.GetRemainingHP();
        spBar.fillAmount = player.GetRemainingSP();

        coordPos.text = player.transform.position.x + "," +
           player.transform.position.y + "," +
           player.transform.position.z;


        stageText.text = "Remaining Enemies: "+GameObject.FindGameObjectsWithTag("minion").Length;
        fps.text = ""+Mathf.RoundToInt(fps_value);

    }
    // Update is called once per frame
    void Update()
    {
        fps_increment();
    }
}



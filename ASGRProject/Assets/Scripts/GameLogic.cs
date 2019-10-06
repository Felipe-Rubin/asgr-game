using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;

public class GameLogic : MonoBehaviour
{
    public Text stageText;
    public List<GameObject> enemies;
    //public Monster mvp;
    public int nmonsters; // How many
    /* Prefabs */
    public GameObject mp; // Monster Prefab
    

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
        //(Pref)
        float x, y = 0.0f;
        for (int i = 0; i < nmonsters; i++)
        {
            x = Random.Range(map1.cellBounds.xMin, map1.cellBounds.xMax);
            y = Random.Range(map1.cellBounds.yMin, map1.cellBounds.yMax);
            GameObject minion = Instantiate(mp, new Vector3(x, y, 0), Quaternion.identity);
            enemies.Add(minion);
        }

    }


    void Start()
    {
        spawn_monsters();
    }
    void FixedUpdate()
    {
        stageText.text = "Remaining Enemies: "+enemies.Count;
    }


    // Update is called once per frame
    void Update()
    {
        //foreach (GameObject mi in enemies)
        //    if (((Monster)mi).hp <= 0.0f) {
        //        Destroy(mi, 0.5f); // delay de 0,5 segundos
        //    }
    }
}


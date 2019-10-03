using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text stageText;
    public List<GameObject> enemies;
    //public Monster mvp;
    public int nmonsters; // How many
    public GameObject mp; // Monster Prefab

    // Start is called before the first frame update
    void Start()
    {
    

        for (int i = 0; i < nmonsters; i++)
        {
            float x = Random.Range(0.0f, 5.0f);
            float y = Random.Range(0.0f, 5.0f);
            GameObject minion = Instantiate(mp, new Vector3(x, y, 0), Quaternion.identity);
            enemies.Add(minion);
        }
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


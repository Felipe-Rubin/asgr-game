using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    public Text stageText;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    void FixedUpdate()
    {
        stageText.text = "Remaining Enemies: ";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}


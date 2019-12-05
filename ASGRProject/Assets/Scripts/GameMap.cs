using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class GameMap : MonoBehaviour
{
    public Tilemap dmap; // Design Map
    public Tilemap cmap; // Collision Map
    public GameObject mvpPrefab;
    public GameObject[] minionPrefab; // Monster Prefab

    // Use this for initialization
    private Vector3 dimension;
    void Start()
    {

    }
    public BoundsInt GetBounds()
    {
        return dmap.cellBounds;
    }

    // Update is called once per frame
    void Update()
    {

    }
}

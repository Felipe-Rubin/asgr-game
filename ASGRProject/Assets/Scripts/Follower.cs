using UnityEngine;
using System.Collections;

public class Follower : MonoBehaviour
{
	public Transform target; // The object to follow
    // Use this for initialization
    private Vector3 nextPos;
    public float followSpeed = 2f;

    void Start()
    {
    }
    
    private void FixedUpdate()
    {
        this.transform.position = nextPos;


    }
    

    // Update is called once per frame
    void Update()
    {
        nextPos = target.position;
        nextPos.z = this.transform.position.z;
    }
}

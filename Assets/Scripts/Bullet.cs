using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public float speed;

    private bool destroyed=false;
    private int direction;

    
	// Use this for initialization
	void Start () {
        direction = GameObject.FindGameObjectWithTag("Player").GetComponent<Player1>().direction;
    }
	
	// Update is called once per frame
	void Update () {
        if (destroyed==false)
        {
            
            transform.position += new Vector3(1,0,0)*speed*direction*Time.deltaTime;
        }
	}
}

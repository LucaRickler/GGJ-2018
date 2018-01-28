using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEnergySphere : MonoBehaviour {

    public Transform boss;
    public float speed;

	// Use this for initialization
	void Start () {
        //boss = GameObject.Find("Enemy").transform;
	}
	
	// Update is called once per frame
	void Update () {
        Movement();		
	}

    void Movement() {
        transform.position = boss.position;
        transform.RotateAround(boss.position, Vector3.up, speed * Time.deltaTime);
    }
}

﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public Transform player;
    public float speed;
    public float speedBack;

	private Animator animator;
	public MeshRenderer nucleusRender;
	[SerializeField]
	private Material _positive_material;
	[SerializeField]
	private Material _negative_material;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player != null)
            Following();

	}

	private Definitions.Polarity polarity;

	void ChangeColor() {
		if (polarity == Definitions.Polarity.Negative) {
			polarity = Definitions.Polarity.Positive;
			nucleusRender.material = _positive_material;
		} else {
			polarity = Definitions.Polarity.Negative;
			nucleusRender.material = _negative_material;
		}
	}

    void Following() {
        Vector3 direction = player.localPosition - transform.localPosition;
        bool isOBstacles = false;
        float angle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        if (!gameObject.name.Equals("EnemyFollow"))
        {
            if (CheckObstacles(transform.forward))            
                isOBstacles = true;           
            else
				transform.rotation = Quaternion.Euler(-90, angle, 0);
        }
        if (!isOBstacles)
        {
            if (Mathf.Abs(player.localPosition.x - transform.localPosition.x) > 5f || Mathf.Abs(player.localPosition.z - transform.localPosition.z) > 5f)
                transform.position = Vector3.MoveTowards(transform.localPosition, player.localPosition, speed * Time.deltaTime);
            else if (Mathf.Abs(player.localPosition.x - transform.localPosition.x) < 5f || Mathf.Abs(player.localPosition.z - transform.localPosition.z) < 5f)
                transform.position -= Vector3.back * Time.deltaTime * speedBack;
        }
        else 
            transform.position += Vector3.right * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Link"))
        {
			animator.SetInteger ("Phase", 2);
            speed = 4f;
        }        
    }

    private bool CheckObstacles(Vector3 direction)
    {
       /* if (Physics.Raycast(transform.position, direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, 2f))
            return true;
        direction.z = -direction.z;
        if (Physics.Raycast(transform.position, direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, 2f))
            return true;
        direction = transform.right;
        if (Physics.Raycast(transform.position, direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, 2f))
            return true;
        direction.x = -direction.x;
        if (Physics.Raycast(transform.position, direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, 2f))
            return true;
        else if (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, 2f))
            return true;*/
        return false;
    }
}

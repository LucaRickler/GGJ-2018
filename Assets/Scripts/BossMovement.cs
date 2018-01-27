using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public Transform player;
    public float speed;
    public float speedBack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player != null)
            Following();

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
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.up);
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

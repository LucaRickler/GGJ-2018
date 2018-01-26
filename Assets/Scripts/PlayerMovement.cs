using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingStraight;
    private bool isMovingBack;

    public float speed;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        InputPlayer();
        UpdatePosition();
        UpdateDirection();
        Shoot();
	}

    void InputPlayer() {
        isMovingLeft = Input.GetKey(KeyCode.A);
        isMovingRight = Input.GetKey(KeyCode.D);
        isMovingStraight = Input.GetKey(KeyCode.W);
        isMovingBack = Input.GetKey(KeyCode.S);
    }

    void UpdatePosition(){

        Vector3 position = transform.localPosition;
        if (isMovingLeft)
            position.x -= speed * Time.deltaTime;
        else if (isMovingRight)
            position.x += speed * Time.deltaTime;
        else if (isMovingStraight)
            position.z += speed * Time.deltaTime;
        else if (isMovingBack)
            position.z -= speed * Time.deltaTime;

        transform.localPosition = position;
    }

    void UpdateDirection() {
        Vector3 objectPos = Camera.main.WorldToScreenPoint(transform.position);
        
        Vector3 dir = Input.mousePosition - objectPos;
      
        transform.rotation = Quaternion.Euler(new Vector3(0, Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg, 0));
    }

    void Shoot() {

        string prefab_name = "";
        GameObject bullet;
        bool mouse_down = false;
        if (Input.GetMouseButtonDown(0)) {
            prefab_name = "BulletPlus";
            mouse_down = true;
        }
        else if (Input.GetMouseButtonDown(1)) {
            prefab_name = "BulletMinus";
            mouse_down = true;
        }
        if(mouse_down)
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/"+prefab_name), transform.localPosition, transform.localRotation);      
    }
}

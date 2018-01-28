using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    private bool isMovingLeft;
    private bool isMovingRight;
    private bool isMovingStraight;
    private bool isMovingBack;

    public float speed;
    private float delayBetweenShoot = 0.2f;

    private bool shooting = false;

    [SerializeField]
    private float shotDelay = 0.2f;

	private bool using_controller = true;
	
	// Update is called once per frame
	void FixedUpdate () {
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1))
            shooting = true;

		/*if (Mathf.Abs (Input.GetAxis ("RightH")) > axis_tolerance)
			using_controller = true;*/
		//else
		//	using_controller = false;
        InputPlayer();
        UpdatePosition();
        UpdateDirection();

        if (delayBetweenShoot <= 0)
        {
            Shoot();
            delayBetweenShoot = shotDelay;
        }
        else
            delayBetweenShoot -= Time.deltaTime;
        if (shooting)
            GutlingShoot();
    }

	private float axis_tolerance = 0.3f;

    void InputPlayer() {
		isMovingLeft = Input.GetKey(KeyCode.A); //|| Input.GetAxis("Horizontal") < -axis_tolerance;
		isMovingRight = Input.GetKey(KeyCode.D);//|| Input.GetAxis("Horizontal") > axis_tolerance;
		isMovingStraight = Input.GetKey(KeyCode.W);// || Input.GetAxis("Vertical") > axis_tolerance;
		isMovingBack = Input.GetKey(KeyCode.S);// || Input.GetAxis("Vertical") < -axis_tolerance;
		/*if (Mathf.Abs (Input.GetAxis ("Horizontal")) < axis_tolerance) {
			isMovingLeft = false;
			isMovingRight = false;
		}
		if (Mathf.Abs (Input.GetAxis ("Vertical")) < axis_tolerance) {
			isMovingBack = false;
			isMovingStraight = false;
		}*/
    }

    void UpdatePosition(){

        Vector3 position = transform.localPosition;
        if (isMovingLeft)
            position.x -= speed * Time.deltaTime;
        else if (isMovingRight)
            position.x += speed * Time.deltaTime;
        if (isMovingStraight)
            position.z += speed * Time.deltaTime;
        else if (isMovingBack)
            position.z -= speed * Time.deltaTime;

        //position = CheckWalls(position);
		transform.localPosition = position;
    }

	//[SerializeField]
	private float rotation_speed = 3.0f;

    void UpdateDirection() {
		//if (using_controller) {
			//float x = Input.GetAxis ("RightH");
			//float y = Input.GetAxis ("RightV");
		//	float dir = Input.GetAxisRaw ("RightH");
		//Vector3 newDir = new Vector3 (transform.position.x + dir, transform.position.y, transform.position.z - Input.GetAxisRaw("RightV"));
		//transform.LookAt (newDir);
			//transform.Rotate (new Vector3 (0, dir*rotation_speed, 0));// = Quaternion.Euler (new Vector3 (0, Mathf.Atan2 (y, x)  * Mathf.Rad2Deg, 0));
			//transform.rotation = Quaternion.Euler (new Vector3 (0, Mathf.Atan2 (y,x) * Mathf.Rad2Deg, 0));
		//} else {
			Vector3 objectPos = Camera.main.WorldToScreenPoint (transform.position);
        
			Vector3 dir = Input.mousePosition - objectPos;
      
			transform.rotation = Quaternion.Euler (new Vector3 (0, Mathf.Atan2 (dir.x, dir.y) * Mathf.Rad2Deg, 0));
		//}
	}

    void Shoot(){
        shooting = false;
        string prefab_name = "";
        GameObject bullet = null;
        if (Input.GetMouseButton(0))
        {
            prefab_name = "BulletPlus";
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/" + prefab_name), transform.localPosition, transform.localRotation);
        }
        else if (Input.GetMouseButton(1))
        {
            prefab_name = "BulletMinus";
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/" + prefab_name), transform.localPosition, transform.localRotation);
        }
        if (bullet != null)
        {
            bullet.transform.rotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, transform.localRotation.eulerAngles.y - 90, transform.localRotation.eulerAngles.z);
        }
    }

    void GutlingShoot() {
        shooting = false;
        string prefab_name = "";
        GameObject bullet;
        bool mouse_down = false;
        if (Input.GetMouseButtonDown(0))
        {
            prefab_name = "BulletPlus";
            mouse_down = true;
        }
        else if (Input.GetMouseButtonDown(1))
        {
            prefab_name = "BulletMinus";
            mouse_down = true;
        }
        if (mouse_down)
        {
            bullet = (GameObject)Instantiate(Resources.Load("Prefabs/" + prefab_name), transform.localPosition, transform.localRotation);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnergySphere") || other.tag.Equals("Boss"))
        {
            Destroy(this.gameObject);
        }
    }

    Vector3 CheckWalls(Vector3 position) {
        RaycastHit hit;
        Vector3 direction = transform.forward;
        if (Physics.Raycast(transform.position, direction, out hit, 1f) || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, out hit, 1f) ||
            (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, out hit, 1))) {
            if (hit.collider.name.Equals("TopBorder"))
                position += -Vector3.forward * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("LeftBorder"))
                position += Vector3.right * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("RightBorder"))
                position += Vector3.left * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("BottomBorder"))
                position += Vector3.forward * speed * Time.deltaTime;
            return position;
        }
        direction.z = -direction.z;

        if (Physics.Raycast(transform.position, direction, out hit, 1f) || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, out hit, 1f) ||
            (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, out hit, 1f))) {
            if (hit.collider.name.Equals("TopBorder"))
                position += -Vector3.forward * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("LeftBorder"))
                position += Vector3.right * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("RightBorder"))
                position += Vector3.left * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("BottomBorder"))
                position += Vector3.forward * speed * Time.deltaTime;
            return position;
        }
        direction = transform.right;
        if (Physics.Raycast(transform.position, direction, out hit, 1f) || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, out hit, 1f) ||
            (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, out hit, 1f)))
        {
            if (hit.collider.name.Equals("TopBorder"))
                position += -Vector3.forward * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("LeftBorder"))
                position += Vector3.right * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("RightBorder"))
                position += Vector3.left * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("BottomBorder"))
                position += Vector3.forward * speed * Time.deltaTime;
            return position;
        }
        direction.x = -direction.x;
        if (Physics.Raycast(transform.position, direction, out hit, 1f) || Physics.Raycast(transform.position + new Vector3(0.5f, 0, 0), direction, out hit, 1f) ||
            (Physics.Raycast(transform.position + new Vector3(-0.5f, 0, 0), direction, out hit, 1f)))
        {
            if (hit.collider.name.Equals("TopBorder"))
                position += -Vector3.forward * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("LeftBorder"))
                position += Vector3.right * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("RightBorder"))
                position += Vector3.left * speed * Time.deltaTime;
            else if (hit.collider.name.Equals("BottomBorder"))
                position += Vector3.forward * speed * Time.deltaTime;
            return position;
        }
        return position;
    }
    
}

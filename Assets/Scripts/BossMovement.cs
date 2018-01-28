using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public Transform player;
    public float speed;
    public float speedBack;
    public float phase2Speed;

	private Animator animator;
	public MeshRenderer nucleusRender;
	public MeshRenderer[] shellRender;
	[SerializeField]
	private Material _positive_shell_material;
	[SerializeField]
	private Material _negative_shell_material;
	[SerializeField]
	private Material _positive_core_material;
	[SerializeField]
	private Material _negative_core_material;

	[SerializeField]
	private float _color_change_time = 20.0f;
	private float _current_color_timer;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		if (Random.value < 0.5f) {
			Debug.Log ("Boss Positive");
			polarity = Definitions.Polarity.Positive;
			nucleusRender.material = _positive_core_material;
			var mats1 = shellRender [0].materials;
			var mats2 = shellRender [2].materials;
			mats1 [0] = _positive_shell_material;
			mats2 [1] = _positive_shell_material;
			for (int i = 0; i < 6; i++)
				shellRender [i].materials = (i != 2 ? mats1 : mats2);
		} else {
			Debug.Log ("Boss Negative");
			nucleusRender.material = _negative_core_material;
			var mats1 = shellRender [0].materials;
			var mats2 = shellRender [2].materials;
			mats1 [0] = _negative_shell_material;
			mats2 [1] = _negative_shell_material;
			for (int i = 0; i < 6; i++)
				shellRender [i].materials = (i != 2 ? mats1 : mats2);
		}

		_current_color_timer = _color_change_time;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player != null)
            Following();
		_current_color_timer -= Time.deltaTime;
		if (_current_color_timer < 0) {
			_current_color_timer = _color_change_time;
			ChangeColor ();
		}

	}
		
	private Definitions.Polarity polarity;

	void ChangeColor() {
		if (polarity == Definitions.Polarity.Negative) {
			polarity = Definitions.Polarity.Positive;
			nucleusRender.material = _positive_core_material;
			var mats1 = shellRender [0].materials;
			var mats2 = shellRender [2].materials;
			mats1 [0] = _positive_shell_material;
			mats2 [1] = _positive_shell_material;
			for (int i = 0; i < 6; i++)
				shellRender [i].materials = (i != 2 ? mats1 : mats2);
		} else {
			polarity = Definitions.Polarity.Negative;
			nucleusRender.material = _negative_core_material;
			var mats1 = shellRender [0].materials;
			var mats2 = shellRender [2].materials;
			mats1 [0] = _negative_shell_material;
			mats2 [1] = _negative_shell_material;
			for (int i = 0; i < 6; i++)
				shellRender [i].materials = (i != 2 ? mats1 : mats2);
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
            if (Mathf.Abs(player.localPosition.x - transform.localPosition.x) > 8f || Mathf.Abs(player.localPosition.z - transform.localPosition.z) > 8f)
                transform.position = Vector3.MoveTowards(transform.localPosition, player.localPosition, speed * Time.deltaTime);
            else if (Mathf.Abs(player.localPosition.x - transform.localPosition.x) < 8f || Mathf.Abs(player.localPosition.z - transform.localPosition.z) < 8f)
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
            speed = phase2Speed;
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

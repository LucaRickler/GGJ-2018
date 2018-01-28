using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	[SerializeField]
	private float _time_out = 10.0f;
	private float _current_time = 10.0f;

	private Transform _target1;
	private Transform _target2;

	private Tower _tower1;
	public Tower Tower1 {
		get {
			return _tower1;
		}
	}

	private Tower _tower2;
	public Tower Tower2 {
		get {
			return _tower2;
		}
	}

	public void Init (Tower tower1, Tower tower2) {
		this._target1 = tower1.targetCenter.transform;
		this._target2 = tower2.targetCenter.transform;
		this._tower1 = tower1;
		this._tower2 = tower2;
		this.gameObject.SetActive (false);
        _current_time = _time_out;
		Stretch ();
	}

	void Update () {
        if (gameObject.activeSelf)
        {
            _current_time -= Time.deltaTime;
            if (_current_time <= 0.0f)
            {
                _current_time = _time_out;
                Deactivate();
            }
        }
		Stretch ();
	}

	void Stretch () {
		Vector3 center = 0.5f * (_target1.position + _target2.position);
		transform.position = center;

		float ax = Relativeposition () * Mathf.Atan2 (Mathf.Abs(_target2.position.z - _target1.position.z), Mathf.Abs(_target2.position.y - _target1.position.y));
		float ay = Mathf.Atan2 (Mathf.Abs(_target2.position.x - _target1.position.x), Mathf.Abs(_target2.position.z - _target1.position.z));
		float az = Mathf.Atan2 (Mathf.Abs(_target2.position.y - _target1.position.y), Mathf.Abs(_target2.position.x - _target1.position.x));
		transform.localRotation = Quaternion.Euler (Mathf.Rad2Deg * ax, Mathf.Rad2Deg * az, Mathf.Rad2Deg * ay);

		var distance = Vector3.Distance (_target1.position, _target2.position);

		transform.localScale = new Vector3 (0.5f, 0.5f* distance, 0.5f);
		GetComponent<CapsuleCollider> ().height = 0.5f * distance;
	}

    public void Deactivate()
    {
        if (gameObject.activeSelf)
        {
            if (_tower1.Polarity != Definitions.Polarity.Off)
                _tower1.Polarity = Definitions.Polarity.Off;
            if (_tower2.Polarity != Definitions.Polarity.Off)
                _tower2.Polarity = Definitions.Polarity.Off;
            this.gameObject.SetActive(false);
        }
    }

	public void Activate () {
		_current_time = _time_out;
		this.gameObject.SetActive (true);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("EnergySphere"))        
            Destroy(this.gameObject);
    }

	int Relativeposition () {
		if (_target2.position.x >= _target1.position.x) {
			if (_target2.position.z >= _target1.position.z) {
				return -1;
			} else {
				return 1;
			}
		} else {
			if (_target2.position.z >= _target1.position.z) {
				return 1;
			} else {
				return -1;
			}
		}
	}

}

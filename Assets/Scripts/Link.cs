using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	[SerializeField]
	private float _time_out = 10.0f;

	private Transform _target1;
	private Transform _target2;
	private Tower _tower1;
	private Tower _tower2;

	public void Init (Tower tower1, Tower tower2) {
		this._target1 = tower1.transform;
		this._target2 = tower2.transform;
		this._tower1 = tower1;
		this._tower2 = tower2;
		Stretch ();
	}

	void Update () {
		_time_out -= Time.deltaTime;
		if (_time_out <= 0.0f)
			Destroy (this.gameObject);
		Stretch ();
	}

	void Stretch () {
		var center = 0.5f * (_target1.localPosition + _target2.localPosition);
		transform.localPosition = center;

		float ax = RelativePosition () * Mathf.Atan2 (Mathf.Abs(_target2.localPosition.z - _target1.localPosition.z), Mathf.Abs(_target2.localPosition.y - _target1.localPosition.y));
		float ay = Mathf.Atan2 (Mathf.Abs(_target2.localPosition.x - _target1.localPosition.x), Mathf.Abs(_target2.localPosition.z - _target1.localPosition.z));
		float az = Mathf.Atan2 (Mathf.Abs(_target2.localPosition.y - _target1.localPosition.y), Mathf.Abs(_target2.localPosition.x - _target1.localPosition.x));
		transform.localRotation = Quaternion.Euler (Mathf.Rad2Deg * ax, Mathf.Rad2Deg * az, Mathf.Rad2Deg * ay);

		var distance = Vector3.Distance (_target1.localPosition, _target2.localPosition);

		transform.localScale = new Vector3 (0.5f, 0.5f* distance, 0.5f);
		GetComponent<CapsuleCollider> ().height = 0.5f * distance;
	}

	public void Deactivate (bool towersOff = true) {
		_tower1.Polarity = Definitions.Polarity.Off;
		_tower2.Polarity = Definitions.Polarity.Off;
		Destroy (this.gameObject);
	}

	int RelativePosition () {
		if (_target2.localPosition.x >= _target1.localPosition.x) {
			if (_target2.localPosition.z >= _target1.localPosition.z) {
				return -1;
			} else {
				return 1;
			}
		} else {
			if (_target2.localPosition.z >= _target1.localPosition.z) {
				return 1;
			} else {
				return -1;
			}
		}
	}
}

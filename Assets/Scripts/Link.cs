using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour {

	[SerializeField]
	private float _time_out = 10.0f;

	private Transform _target1;
	private Transform _target2;

	public void Init (Transform target1, Transform target2) {
		this._target1 = target1;
		this._target2 = target2;
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

		float ax = Mathf.Atan2 (_target2.localPosition.z - _target1.localPosition.z, _target2.localPosition.y - _target1.localPosition.y);
		float ay = Mathf.Atan2 (_target2.localPosition.z - _target1.localPosition.z, _target2.localPosition.x - _target1.localPosition.x);
		float az = Mathf.Atan2 (_target2.localPosition.y - _target1.localPosition.y, _target2.localPosition.x - _target1.localPosition.x);
		transform.localRotation = Quaternion.Euler (ax, ay, az);
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {
	public enum RayResult {
		Wall,
		Charachter,
		Tower,
		None
	} 

	public RayResult CheckView (Transform other) {
		RaycastHit hit = null;
		Physics.Raycast (this.transform.position, other.position, out hit);
		if (hit != null) {
			if (hit.collider.tag == "wall")
				return RayResult.Wall;
			if (hit.collider.tag == "charachter")
				return RayResult.Charachter;
			if (hit.collider.tag == "tower")
				return RayResult.Tower;
		}
		return RayResult.None;
	}
}

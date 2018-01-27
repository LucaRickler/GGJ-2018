using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayViewer : MonoBehaviour {

    
	public Definitions.HitType CheckView (Transform other) {
		RaycastHit hit;
        if (Physics.Raycast(transform.position, other.position - transform.position, out hit, 10000000000))
        {

            if (hit.collider.tag == "Wall")
                return Definitions.HitType.Wall;
            if (hit.collider.tag == "harachter")
                return Definitions.HitType.Charachter;
            if (hit.collider.tag == "Tower")
                return Definitions.HitType.Tower;
        }
		return Definitions.HitType.None;

	}
}

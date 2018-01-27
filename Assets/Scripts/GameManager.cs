using System;
using UnityEngine;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager> {
	public List<Tower> towers = new List<Tower>();

	[SerializeField]
	private GameObject _link_prefab;

	void Awake () {
		foreach (Tower t1 in towers) {
			foreach (Tower t2 in towers) {
				if (t2 != t1 && !(t1.HasLink(t2))) {
					GameObject link = Instantiate (_link_prefab) as GameObject;
					link.GetComponent<Link> ().Init (t1, t2);
					t1.AddLink (link.GetComponent<Link> ());
					t2.AddLink (link.GetComponent<Link> ());
				}
			}	
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	private RayViewer _ray_viewer;

	[SerializeField]
	private GameObject _link_prefab; 

	private Polarity _polarity = Polarity.Off;
	public Polarity Polarity {
		get {
			return this._polarity;
		} 
		set {
			this._polarity = value;
			this.CheckLink ();
		}
	}

	void Start () {
		this._ray_viewer = this.GetComponent<RayViewer> ();
	}

	public void CheckLink () {
		List<Tower> all_towers = GameManager.Instance.towers;
		foreach (Tower t in all_towers) {
			if (this._ray_viewer.CheckView (t.transform)) {
				this.CreateLink (t);
				return;
			}
		}
	}

	private void CreateLink (Tower other) {
		
	}



}

enum Polarity {
	Negative,
	Off,
	Positive
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	private RayViewer _ray_viewer;

	[SerializeField]
	private GameObject _link_prefab;

	private Definitions.Polarity _polarity = Definitions.Polarity.Off;

	public Definitions.Polarity Polarity {
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
            if (t.gameObject != this.gameObject)
            {
                Definitions.HitType res = this._ray_viewer.CheckView(t.transform);
                if (t.Polarity != Definitions.Polarity.Off && t.Polarity != this.Polarity)
                {
                    if (res == Definitions.HitType.Tower || res == Definitions.HitType.Charachter)
                    {
                        this.CreateLink(t);
                        break;
                    }
                }
            }
		}
	}

	private void CreateLink (Tower other) {
        Debug.Log("link on");
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet")) {
            Polarity = other.gameObject.GetComponent<BulletBehaviour>().Polarity;
            Destroy(other.gameObject);
        }
    }
    
}

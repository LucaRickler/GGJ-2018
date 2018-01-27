using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour {
	private RayViewer _ray_viewer;


	private List<Link> _links = new List<Link>();
	private bool _link_on = false;

	[SerializeField]
	private Definitions.Polarity _polarity = Definitions.Polarity.Off;
	public Definitions.Polarity Polarity {
		get {
			return this._polarity;
		} 
		set {
			if (value != this._polarity && _link_on) {
				_link_on = false;
				foreach (Link l in _links) {
					l.Deactivate ();
				}
			}
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

	public void AddLink(Link l) {
		this._links.Add (l);
	}

	private void CreateLink (Tower other) {
		FindLink (other).Activate ();
		_link_on = true;
	}

	public bool HasLink (Tower other) {
		return this.FindLink (other) != null;
	}

	public Link FindLink(Tower other) {
		foreach (Link l in _links) {
			if ((l.Tower1 == this && l.Tower2 == other) || (l.Tower2 == this && l.Tower1 == other))
				return l;
		}
		return null;
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Bullet")) {
            Polarity = other.gameObject.GetComponent<BulletBehaviour>().Polarity;
            Destroy(other.gameObject);
        }
    }
    
}

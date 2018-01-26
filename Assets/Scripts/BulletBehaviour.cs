using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour {

    public float lifeTime;
    public float speed;

    [SerializeField]
    private Polarity _polarity;

    public Polarity Polarity
    {
        get
        {
            return this._polarity;
        }
        set
        {
            this._polarity = value;
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Movement();
	}

    void Movement() {
        if (lifeTime <= 0)
            Death();
        else
        {
            lifeTime -= Time.deltaTime;
            transform.position += transform.forward * Time.deltaTime * speed;
        }
    }

    void Death() {
        Destroy(this.gameObject);
    }
}

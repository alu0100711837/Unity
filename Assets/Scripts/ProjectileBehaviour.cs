using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

	// Use this for initialization
	public float lifeTime = 3.0f;
	public float damage = 5.0f;
	public Transform explosion;
	void Start () {
		Destroy(gameObject, lifeTime);
	}
	
		void OnTriggerEnter(Collider other) {
		Explode();
	}

	void Explode() {
		Transform e = Instantiate(explosion, this.transform.position, this.transform.rotation);
		this.gameObject.SetActive(false);
		Destroy(e.gameObject, 3.0f);
		Destroy(this.gameObject, 3.5f);
	}

	
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {

	// Use this for initialization
	private float speed = 2.0f;
	private Rigidbody rigidbody;
	void Start () {
		rigidbody = this.GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		FixMovement();
	}

	void Move() {
		transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
		transform.Rotate(Input.GetAxis("Mouse Y") * speed,  0f, 0, Space.Self);
	}

	void FixMovement() {
		this.rigidbody.AddForce(this.transform.forward * -10.0f);
	}
}

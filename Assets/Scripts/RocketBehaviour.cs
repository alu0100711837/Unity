using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehaviour : MonoBehaviour {

	float speed = 2.0f;
	Rigidbody rigidbody;

	
	void Start () {
		rigidbody = this.GetComponent<Rigidbody>();
	}
	
	void Update () {
		Move();
		FixMovement();
	}

	void Move() {
		transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
		transform.Rotate(Input.GetAxis("Mouse Y") * speed,  0f, 0, Space.Self);
	}

	//Método que hace que el cohete tienda a avanzar en todo momento hacia donde está apuntando
	void FixMovement() {
		this.rigidbody.AddForce(this.transform.forward * -10.0f);
	}
}

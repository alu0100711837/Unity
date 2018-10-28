using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour {

	// Use this for initialization
	float speed = 2.0f;		//Sensibilidad
	public float error = 0.3f;
	public Transform projectile;
	public GameObject projectileSpawn;
	public float timeBetweenFire = 0.3f;	//Cadencia de disparo
	private float timeUntilNextFire;
	private Vector3 bombPosition;
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;	//Ocultamos el cursor
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Shoot();
	}

//Muy importante tener en cuenta el sistema de referencia de cada eje.
	void Move() {
		transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
		transform.Rotate(Input.GetAxis("Mouse Y") * speed,  0f, 0, Space.Self);
	}

	void Shoot() {
		if (Input.GetMouseButton(0) && timeUntilNextFire <= 0) {
			Transform b = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
			Vector3 forceDirection = projectileSpawn.transform.position - this.transform.position;
			Vector3 desviation = new Vector3(Random.Range(-error, error), Random.Range(-error, error), Random.Range(-error, error));
			b.GetComponent<Rigidbody>().AddForce((forceDirection.normalized + desviation) * 400.0f);
			timeUntilNextFire = timeBetweenFire;
		}
		timeUntilNextFire -= Time.deltaTime;
	}
}

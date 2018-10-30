using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortairBehaviour : MonoBehaviour {

	float speed = 2.0f;		//Sensibilidad
	public float error = 0.3f;
	public Transform projectile;
	public GameObject projectileSpawn;
	public float timeBetweenFire = 0.3f;	//Cadencia de disparo
	private float timeUntilNextFire;
	private Vector3 bombPosition;

	public List<GameObject> fireSpawners;
	public Transform fireEffect;

	private AudioSource shotSound;
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;	//Ocultamos el cursor
		shotSound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Shoot();
	}

//Muy importante tener en cuenta el sistema de referencia de cada eje.
	void Move() {
		if (timeUntilNextFire <= 0) {
			transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
			projectileSpawn.transform.Translate(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
		}
	}
  
	void Shoot() {
		if (Input.GetMouseButton(0) && timeUntilNextFire <= 0) {
			StartCoroutine(FakeShoot());
			StartCoroutine(dropBombs());
			timeUntilNextFire = timeBetweenFire;
		}
		timeUntilNextFire -= Time.deltaTime;
	}

	IEnumerator dropBombs() {
		yield return new WaitForSeconds(3.0f);
		for (int i = 0; i < 5; i++) {
			Vector3 desviation = new Vector3(Random.Range(-error, error), 0, Random.Range(-error, error));
			Transform b = Instantiate(projectile, projectileSpawn.transform.position + desviation, projectileSpawn.transform.rotation);
			b.GetComponent<Rigidbody>().AddForce(new Vector3(0, -200000.0f, 0));
			yield return new WaitForSeconds(0.5f);
		}
	}

	IEnumerator FakeShoot() {
		for (int i = 0; i < 5; i++) {
			Instantiate(fireEffect, fireSpawners[i].transform.position, fireSpawners[i].transform.rotation);
			shotSound.Play();
			yield return new WaitForSeconds(0.5f);
		}
	}
}

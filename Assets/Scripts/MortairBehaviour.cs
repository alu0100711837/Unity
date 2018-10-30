using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MortairBehaviour : MonoBehaviour {

	//Sensibilidad de movimiento
	float speed = 2.0f;
	float timeUntilNextFire;
	Vector3 bombPosition;
	//Precisión del arma. Si es 0 todas las balas impactarán exactamente en el mismo sitio
	public float error = 0.3f;
	public Transform projectile;
	public GameObject projectileSpawn;
	//Cadencia de tiro. Cuanto más bajo sea el número más rápido disparará el arma
	public float timeBetweenFire = 0.3f;
	public List<GameObject> fireSpawners;
	public Transform fireEffect;

	private AudioSource shotSound;

	//Ocultamos el cursor e inicializamos y cargamos el componente de sonido para solo hacerlo una vez
	void Start () {
		Cursor.lockState = CursorLockMode.Locked;
		shotSound = this.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		Move();
		Shoot();
	}

	//Muy importante tener en cuenta el sistema de referencia de cada eje. Explicado con más detalle en el blog
	void Move() {
		if (timeUntilNextFire <= 0) {
			transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
			projectileSpawn.transform.Translate(Input.GetAxis("Vertical"), 0, -Input.GetAxis("Horizontal"));
		}
	}
  
	/* Ejecutamos la animación de disparo pero realmente no disparamos. Luego instanciamos las bombas en el punto seleccionado.
	Es importante que sean corrutinas. Explicado con más detalle en el blog */
	void Shoot() {
		if (Input.GetMouseButton(0) && timeUntilNextFire <= 0) {
			StartCoroutine(FakeShoot());
			StartCoroutine(dropBombs());
			timeUntilNextFire = timeBetweenFire;
		}
		timeUntilNextFire -= Time.deltaTime;
	}

	/* Instanciamos las bombas con un cierto retardo entre una y otra. Aplicamos algo de fuerza vertical para que parezca
	que ya venían cayendo desde más alto. Aplicamos también algo de desviación para que caigan en un área dispersa */
	IEnumerator dropBombs() {
		yield return new WaitForSeconds(3.0f);
		for (int i = 0; i < 5; i++) {
			Vector3 desviation = new Vector3(Random.Range(-error, error), 0, Random.Range(-error, error));
			Transform b = Instantiate(projectile, projectileSpawn.transform.position + desviation, projectileSpawn.transform.rotation);
			b.GetComponent<Rigidbody>().AddForce(new Vector3(0, -200000.0f, 0));
			yield return new WaitForSeconds(0.5f);
		}
	}

	//Instanciamos la bola de fuego en cada cañón y el sonido para crear la sensación de disparo
	IEnumerator FakeShoot() {
		for (int i = 0; i < 5; i++) {
			Instantiate(fireEffect, fireSpawners[i].transform.position, fireSpawners[i].transform.rotation);
			shotSound.Play();
			yield return new WaitForSeconds(0.5f);
		}
	}
}

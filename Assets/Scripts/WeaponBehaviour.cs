using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviour : MonoBehaviour {

	//Sensibilidad de movimiento
	float speed = 2.0f;
	float timeUntilNextFire;
	Vector3 bombPosition;
	//Precisión del arma. Si es 0 todas las balas impactan exactamente en el mismo sitio
	public float error = 0.3f;
	public Transform projectile;
	public GameObject projectileSpawn;
	//Cadencia de tiro. Cuanto más bajo sea el número más rápido dispara el arma
	public float timeBetweenFire = 0.3f;

	private AudioSource shotSound;


	void Start () {
		//Ocultamos el cursor e inicializamos el componente de audio solo una vez
		Cursor.lockState = CursorLockMode.Locked;
		shotSound = this.GetComponent<AudioSource>();
	}
	
	void Update () {
		Move();
		Shoot();
	}

//Muy importante tener en cuenta el sistema de referencia de cada eje. Explicado con detalle en el blog
	void Move() {
		transform.Rotate(0f, Input.GetAxis("Mouse X") * speed, 0f, Space.World);
		transform.Rotate(Input.GetAxis("Mouse Y") * speed,  0f, 0, Space.Self);
	}
  
	void Shoot() {
		if (Input.GetMouseButton(0) && timeUntilNextFire <= 0) {
			//Instanciamos el proyectil. Calculamos y le aplicamos fuerza y desviación.
			Transform b = Instantiate(projectile, projectileSpawn.transform.position, projectileSpawn.transform.rotation);
			Vector3 forceDirection = projectileSpawn.transform.position - this.transform.position;
			Vector3 desviation = new Vector3(Random.Range(-error, error), Random.Range(-error, error), Random.Range(-error, error));
			b.GetComponent<Rigidbody>().AddForce((forceDirection.normalized + desviation) * 400.0f);
			shotSound.Play();
			timeUntilNextFire = timeBetweenFire;
		}
		timeUntilNextFire -= Time.deltaTime;
	}
}

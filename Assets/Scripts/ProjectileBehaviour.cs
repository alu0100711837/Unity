using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour {

	public float lifeTime = 3.0f;
	public float damage = 5.0f;
	public Transform explosion;


	void Start () {
		//Cuando el proyectil se instancia comienza la cuenta atrás para su destrucción
		Destroy(gameObject, lifeTime);
	}
	
	void OnTriggerEnter(Collider other) {
		Explode();
	}

		/*Instanciamos la explosión. Tenemos que eliminar la bala cuando ha chocado, pero la bala es quien ha instanciado la explosión.
		Si eliminamos un objeto se eliminan automáticamente todos los objetos instanciados por el primero.
		Si eliminamos la bala, la explosión automáticamente se cortará y desaparecerá.
		Solución: desactivamos la bala (que no es lo mismo que eliminar) y una vez pasado un tiempo prudencial para que
		la animación de la explosión termine, destruímos la explosión y luego la bala */
	void Explode() {
		Transform e = Instantiate(explosion, this.transform.position, this.transform.rotation);
		this.gameObject.SetActive(false);
		Destroy(e.gameObject, 3.0f);
		Destroy(this.gameObject, 3.5f);
	}

	
}

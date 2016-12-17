using UnityEngine;
using System.Collections;



public class ItemRespawn : MonoBehaviour {
	
	public GameObject power1;
	public GameObject power2;
	public GameObject power3;
	public GameObject power4;


	public float respawnRate = 3.0F;
	private float time;
	private Object powerChild;

	// Use this for initialization
	void Start () {
		Respawn();
		time = respawnRate;
	}
	
	// Update is called once per frame
	void Update () {
		if(powerChild == null) time -= Time.deltaTime;
		if (0.0F >= time) {
			time = respawnRate;
			Respawn ();
		}
	}

	void Respawn () {
		float p = Random.Range (0.0F, 100.0F);
		if (p > 75.0F) {
			powerChild = Object.Instantiate (power1, transform.position, transform.rotation);
		} else if (p > 50.0F && 75.0F >= p) {
			powerChild = Object.Instantiate (power2, transform.position, transform.rotation);
		}
		else if (p > 25.0F && 50.0F >= p) {
			powerChild = Object.Instantiate (power3, transform.position, transform.rotation);
		}
		else {
			powerChild = Object.Instantiate (power4, transform.position, transform.rotation);
		}

	}
}

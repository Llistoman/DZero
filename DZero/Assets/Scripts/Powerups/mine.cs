using UnityEngine;
using System.Collections;

public class mine : MonoBehaviour {
	
	bool death = false;
	public float speed = 8;
	public float sizeChange = 0.001F;
	private AudioSource audio;
	private int numColisions;
	private ParticleSystem expl;

	// Use this for initialization
	void Start () {
		numColisions = 0;
		audio = gameObject.GetComponent<AudioSource>();
		expl = gameObject.GetComponent<ParticleSystem> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!death) {
			//Escalado para remarcar el objeto
			float scale = sizeChange * Mathf.Sin (Time.time * speed);
			transform.localScale += new Vector3 (scale, scale, scale);
		} else {
			transform.localScale -= new Vector3 (sizeChange, sizeChange, sizeChange);
			if (transform.localScale.x < 0)
				Destroy(gameObject);
		}
	}

	void OnTriggerEnter(Collider other) {
		numColisions++;
		if ( 1 < numColisions && (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))) {
			other.SendMessage ("dmg", 20.0f);
			other.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (5000, transform.position - new Vector3(0,0.5f,0), 10f);
			audio.Play();
			expl.Play ();
			death = true;
		}
	}
}

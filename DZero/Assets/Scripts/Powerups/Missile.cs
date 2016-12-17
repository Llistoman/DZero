using UnityEngine;
using System.Collections;

public class Missile : MonoBehaviour {

	public float speed = 3.0F;
	public float height = 0.2f;
	private AudioSource audio;
	private ParticleSystem part;
	private bool death = false;
	public Vector3 ActiveWaypoint;
	public GameObject Target;
	private float dist1;
	private float dist2;
	private bool lockOn = false;

	// Use this for initialization
	void Start () {
		transform.RotateAround (transform.position, transform.up, 180f);
		audio = GetComponent<AudioSource> ();
		part = GetComponent<ParticleSystem> ();
	}

	// Update is called once per frame
	void Update () {
		if (!death) {
			dist1 = Vector3.Distance(Target.transform.position,transform.position);
			dist2 = Vector3.Distance(ActiveWaypoint,transform.position);
			RaycastHit hit;
			if (Physics.Raycast (transform.position, -transform.up, out hit, height * 5)) {
				transform.Translate (0, (height - hit.distance), 0);
			}
			if (dist1 < dist2)
				lockOn = true;
			if (lockOn) {
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (Target.transform.position - transform.position), 10 * Time.deltaTime);
			} else {
				
				transform.rotation = Quaternion.Slerp (transform.rotation, Quaternion.LookRotation (ActiveWaypoint - transform.position), 10 * Time.deltaTime);
			}
			transform.position += transform.forward * speed; 
		}
		else 
			Destroy(gameObject,0.5f);
	}

	void OnTriggerEnter(Collider other) {
		if ((other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI"))) {
			other.SendMessage ("dmg", 20.0f);
			other.gameObject.GetComponent<Rigidbody> ().AddExplosionForce (5000, transform.position, 5f);
			audio.Play();
			part.Play ();
			death = true;
		}
	}
}

using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {
	public GameObject Door1;
	public GameObject Door2;
	public GameObject TopeDoor1;
	public GameObject TopeDoor2;

	public float speed = 2.5F;
	public float speedClose = 15.0F;
	private Vector3 trans;
	private Vector3 transClose;
	private float initialx;

	private AudioSource audio;

	int open = 1;


	// Use this for initialization
	void Start () {
		trans = new Vector3 (speed, 0.0F, 0.0F);
		transClose = new Vector3 (speedClose, 0.0F, 0.0F);
		initialx = Door1.transform.position.x;
		audio = gameObject.GetComponent<AudioSource>();
		Open ();
	}
	
	// Update is called once per frame
	void Update () {
		if (open == 1) {
			Door1.transform.position -= trans;
			Door2.transform.position += trans;
			if (Door1.transform.position.x <= TopeDoor1.transform.position.x)
				Stop ();
		}
		if (open == 2) {
			Door1.transform.position += transClose;
			Door2.transform.position -= transClose;
			if (Door1.transform.position.x >= initialx)
				Stop ();
		}
		if (open == 3) {
			Door1.transform.position += transClose;
			Door2.transform.position -= transClose;
			if (Door1.transform.position.x >= initialx) {
				Stop ();
				StartCoroutine (PostOpen ());
			}
		}
	
	}

	IEnumerator PostOpen(){
		yield return new WaitForSeconds (0.4F);
		open = 1;
	}

	void Open(){
		audio.Play();
		open = 1;
	}

	void Close(){
		audio.Play();
		open = 2;
	}

	void CloseAndOpen(){
		audio.Play();
		open = 3;
	}

	void Stop(){
		open = 0;
	}
}

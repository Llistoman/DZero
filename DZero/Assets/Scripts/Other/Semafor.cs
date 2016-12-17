using UnityEngine;
using System.Collections;

public class Semafor : MonoBehaviour {
	private Light red;
	private Light yellow;
	private Light green;

	private AudioSource asem1;
	private AudioSource asem2;
	private AudioSource asem3;

	public float speedMovement = 2.0F;
	public float transMovement = 0.0005F;

	public float timeCadence = 2.0F;
	private float time;
	int fase = 0;

	// Use this for initialization
	void Start () {
		red = GameObject.Find("RL").GetComponent(typeof(Light)) as Light;
		yellow = GameObject.Find("YL").GetComponent(typeof(Light)) as Light;
		green = GameObject.Find("GL").GetComponent(typeof(Light)) as Light;

		asem1 = GameObject.Find("AS1").GetComponent(typeof(AudioSource)) as AudioSource;
		asem2 = GameObject.Find("AS2").GetComponent(typeof(AudioSource)) as AudioSource;
		asem3 = GameObject.Find("AS3").GetComponent(typeof(AudioSource)) as AudioSource;

		time = timeCadence;
		turnRed ();
	}
	
	// Update is called once per frame
	void Update () {
		//Moviment
		float trans = transMovement * Mathf.Sin (Time.time * speedMovement);
		transform.localPosition += new Vector3 (0, trans, 0);

		//Colors per començar
		time -= Time.deltaTime;
		if (time <= 0) {
			if (fase == 1) {
				turnYellow ();
				asem1.Play ();
			} else if (fase == 2) {
				asem2.Play ();
			}
			else if (fase == 3) {
				turnGreen ();
				time = -1.0F;
				asem3.Play ();
				//START
				GameObject.Find("Config").SendMessage("go");
			}
			time = timeCadence;
			fase++;
		}

	}

	void turnRed(){
		red.enabled = true;
		yellow.enabled = false;
		green.enabled = false;
	}
	void turnYellow(){
		red.enabled = false;
		yellow.enabled = true;
		green.enabled = false;
	}
	void turnGreen(){
		red.enabled = false;
		yellow.enabled = false;
		green.enabled = true;
	}
}


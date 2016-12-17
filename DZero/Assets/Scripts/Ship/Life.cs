using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	// Use this for initialization
	private GameObject energyBar;
	public float life;
	private AudioSource audio;
	private ParticleSystem part;
	public float MAXlife = 100.0f;
	public GameObject model;

	void Start () {
		life = MAXlife;
		energyBar = GameObject.Find ("EnergyBar");
		audio = GetComponent<AudioSource> ();
		part = GetComponent<ParticleSystem> ();
	}

	void dmg (float f){
		life -= f;
		gameObject.SendMessage ("noShield");
		if (life < 0.0f) life = 0.0f;

		//TODO Molts ifs mal ficats XD
		if (gameObject.tag == "Player") energyBar.SendMessage("setValue", life);
		if (gameObject.tag == "Player" && life == 0) {
				gameObject.SendMessage ("GameOver");
		}
		if (life == 0) {
			audio.Play ();
			part.Play ();
			Destroy(model);
			if (gameObject.tag == "Player")gameObject.GetComponent<HoverCarControl>().enabled = false;
			else if (gameObject.tag == "AI")gameObject.GetComponent<AiControl>().enabled = false;
			gameObject.GetComponent<Rigidbody>().useGravity = false;

		}
	}

	void heal (float f){
		life += f;
		if (life > MAXlife) life = MAXlife;
		if (gameObject.tag == "Player")energyBar.SendMessage("setValue", life);
	}
}

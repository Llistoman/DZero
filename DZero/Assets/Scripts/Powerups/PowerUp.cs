using UnityEngine;
using System.Collections;

public class PowerUp : MonoBehaviour {

	public float speed = 10;
	public float sizeChange = 0.005F;
	private AudioSource audio;

	bool death = false;

	// Use this for initialization
	void Start () {
		audio = gameObject.GetComponent<AudioSource>();
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
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI")) {
			if (other.gameObject.CompareTag("Player")) {
				GameObject.Find ("item_icon").SendMessage("UpdateImage", gameObject.GetComponent<MeshRenderer>().material);
				other.gameObject.GetComponent<UseItems> ().item = what (gameObject.GetComponent<MeshRenderer> ().material);
			}
			else 
				other.gameObject.GetComponent<UseItemsAI> ().item = what (gameObject.GetComponent<MeshRenderer> ().material);
			audio.Play();
			gameObject.GetComponent<Collider>().enabled = false;
			death = true;
		}
	}

	int what(Material mat) {
		if (mat.name == "boost (Instance)")
			return 1;
		if (mat.name == "mine (Instance)")
			return 2;
		if (mat.name == "missile (Instance)")
			return 3;
		else
			return 4;
	}
}

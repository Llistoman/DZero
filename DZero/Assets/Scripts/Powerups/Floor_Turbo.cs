using UnityEngine;
using System.Collections;

public class Floor_Turbo : MonoBehaviour {

	private Material mat;
	private Color colorStart = Color.red;
	private Color colorEnd = Color.yellow;
	public float timeChangeColor = 0.3F;

	public float turboTime = 1.5F;

	// Use this for initialization
	void Start () {
		mat = gameObject.GetComponent<MeshRenderer>().material;
	}
	
	// Update is called once per frame
	void Update () {
		float lerp = Mathf.PingPong(Time.time, timeChangeColor) / timeChangeColor;
		mat.color = Color.Lerp (colorStart, colorEnd, lerp);
	}

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI")) {
			other.SendMessage ("turbo", turboTime);
			//audio.Play();
		}
	}
}

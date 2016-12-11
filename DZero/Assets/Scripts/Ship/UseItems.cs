using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UseItems : MonoBehaviour {
	public Image icon;
	public GameObject self;
	public GameObject mine;

	private GameObject childMine;

	public float turboTime = 5.0F;
	private float time = 0.0F;
	private bool turboOn = false;

	// Use this for initialization
	void Start () {
	
	}

	// Update is called once per frame
	void Update () {
		if (turboOn) {
			time -= Time.deltaTime;
			if (time <= 0) {
				self.SendMessage ("stopturbo");
				turboOn = false;
			}
		}

		if (Input.GetKeyDown ("space")) {
			if (icon.material.name == "boost (Instance)") {
				self.SendMessage ("turbo");
				time = turboTime;
				turboOn = true;
			}
			else if (icon.material.name == "mine (Instance)") {
				childMine = (GameObject)GameObject.Instantiate (mine, transform.position, transform.rotation);
				//Physics.IgnoreCollision (childMine.GetComponent<Collider> (), GetComponent<Collider> ());
			}
			icon.material = null;
		}
	}
}

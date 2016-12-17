using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UseItems : MonoBehaviour {

	public Vector3 MissilePoint;
	public Image icon;
	public int item = 0;
	public GameObject self;
	public GameObject mine;
	public float turboTime = 3.0f;
	private GameObject childMine;
	public GameObject lanzadera;
	public GameObject escut;
	public GameObject bullet;
	private bool turboOn = false;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown ("space")) {
			if (item == 1) {
				self.SendMessage ("turbo", turboTime);
			}
			else if (item == 2) {
				childMine = (GameObject)GameObject.Instantiate (mine, transform.position, transform.rotation);
			}
			else if (item == 3) {
				childMine = (GameObject)GameObject.Instantiate (bullet, lanzadera.transform.position, transform.rotation);
				Physics.IgnoreCollision (childMine.GetComponent<Collider> (), GetComponent<Collider> ());
				childMine.GetComponent<Missile> ().ActiveWaypoint = MissilePoint;
				childMine.GetComponent<Missile> ().Target = GetComponent<LapCount>().Victim;
			}
			else if (item == 4) {
				escut.active = true;
			}
			item = 0;
			icon.material = null;
		}
	}

	void noShield(){
		if (escut.active)
			gameObject.SendMessage ("heal", 20.0F);
		escut.active = false;
	}
}

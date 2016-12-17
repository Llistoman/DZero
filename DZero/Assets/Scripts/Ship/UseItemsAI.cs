using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UseItemsAI : MonoBehaviour {
	
	public int strat;
	public int item = 0;
	public Vector3 MissilePoint;
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
		if (strat == 0) {
			if (item == 1) {
				useBoost ();
			} else if (item == 2) {
				useMine ();
			} else if (item == 3) {
				//if second or less
				shoot ();
			} else if (item == 4) {
				useShield ();
			}
			item = 0;
		} else {
			if (item == 1) {
				Invoke ("useBoost", 3f);
			}
			else if (item == 2) {
				Invoke ("useMine",5f);
			}
			else if (item == 3) {
				//if second or less
				Invoke ("shoot", 1f);
			}
			else if (item == 4) {
				Invoke ("useShield", 3f);
			}
			item = 0;
		}
	}

	void noShield(){
		if (escut.active)
			gameObject.SendMessage ("heal", 20.0F);
		escut.active = false;
	}

	void useBoost() {
		self.SendMessage ("turbo", turboTime);
	}

	void useMine() {
		childMine = (GameObject)GameObject.Instantiate (mine, transform.position, transform.rotation);
	}

	void shoot() {
		childMine = (GameObject)GameObject.Instantiate (bullet, lanzadera.transform.position, transform.rotation);
		childMine.GetComponent<Missile> ().ActiveWaypoint = MissilePoint;
		childMine.GetComponent<Missile> ().Target = GetComponent<LapCount>().Victim;
	}

	void useShield() {
		escut.active = true;
	}
}

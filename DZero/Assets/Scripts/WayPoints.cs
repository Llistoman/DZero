using UnityEngine;
using System.Collections;

public class WayPoints : MonoBehaviour {
	
	GameObject[] ships;
	public Transform Next;
	float offset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider other) {
		if (other.CompareTag ("AI")) {
			offset = Random.value;
			offset = Mathf.Clamp (offset, 0.0f, 0.6f);
			if (Random.value >= 0.5) {
				other.gameObject.GetComponent <AiControl> ().ActiveWaypoint = (Next.position + new Vector3(offset,0,0));
			} else {
				other.gameObject.GetComponent <AiControl> ().ActiveWaypoint = Next.position + new Vector3(-offset,0,0);
			}
		}
	}

	void OnDrawGizmos() {
		//  show waypoints
		Gizmos.DrawWireSphere (transform.position,1.0f);
	}
}

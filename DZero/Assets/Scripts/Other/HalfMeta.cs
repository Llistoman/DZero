using UnityEngine;
using System.Collections;

public class HalfMeta : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI")) {
			other.SendMessage ("halfGoal");
		}
	}
}

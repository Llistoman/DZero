using UnityEngine;
using System.Collections;

public class Meta : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("AI")) {
			other.SendMessage ("goal");
		}
	}
}

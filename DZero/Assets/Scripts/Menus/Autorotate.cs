using UnityEngine;
using System.Collections;

public class Autorotate : MonoBehaviour {

	public float speed = 20.0F;

	// Update is called once per frame
	void Update () {
		transform.Rotate (0, Time.deltaTime * speed, 0);

	}
}

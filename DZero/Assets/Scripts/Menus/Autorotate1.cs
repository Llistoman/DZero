using UnityEngine;
using System.Collections;

public class Autorotate1 : MonoBehaviour {

	public float speed = 20.0F;

	// Update is called once per frame
	void Update () {
		transform.Rotate (Time.deltaTime * speed, Time.deltaTime * speed, Time.deltaTime * speed);

	}
}

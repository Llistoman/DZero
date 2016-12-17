using UnityEngine;
using System.Collections;

public class JetSound : MonoBehaviour {

	public AudioSource jetSound;
	private float jetPitch;
	private const float LowPitch = 0.1f;
	private const float HighPitch = 2.0f;
	private const float SpeedToRevs = 0.4f;
	Vector3 myVelocity;
	Rigidbody body;

	void Awake () 
	{
		body = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		myVelocity = body.velocity;
		float forwardSpeed = transform.InverseTransformDirection(body.velocity).z;
		float engineRevs = Mathf.Abs (forwardSpeed) * SpeedToRevs;
		jetSound.pitch = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
	}
}

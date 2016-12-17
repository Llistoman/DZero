using UnityEngine;
using System.Collections;

public class JetSmoke : MonoBehaviour {

	public ParticleSystem jetSmoke1;
	public ParticleSystem jetSmoke2;
	private float jetPitch;
	private const float LowPitch = 0.005f;
	private const float HighPitch = 0.5f;
	private const float SpeedToRevs = 0.0032f;
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
		jetSmoke1.startSize = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
		jetSmoke2.startSize = Mathf.Clamp (engineRevs, LowPitch, HighPitch);
	}
}

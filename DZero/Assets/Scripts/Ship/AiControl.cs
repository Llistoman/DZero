using UnityEngine;
using System.Collections;

public class AiControl : MonoBehaviour {

	Rigidbody body;
	public GameObject model;
	public GameObject center;
	public GameObject[] hoverPoints;

	public float deadZone = 0.0f;
	public float hoverForce = 9.0f;
	public float bounce = 3.0f;
	public float hoverHeight = 2.0f;
	public float reactionTime = 0.5f;

	float thrust = 0.0f;
	public float MaxSpeedFwd = 100.0f;
	public float MinSpeedFwd = 80.0f;
	public float accel = 1.5f;
	public float inertia = 0.9f;
	public float turboBoost = 1.5f;
	private float time = 0.0f;
	private bool turboOn = false;
	private float InitialMaxSpeedFwd;

	public float currentSpeed = 0.0f;
	public int state;
	public bool accelState;
	public bool slowState;
	public bool reverseState;

	float turn = 0.0f;
	public float turnStrength = 10f;
	public float incline = 0.0f;
	public float maxAngle = 10.0f;

	int layerMask;
	public GameObject SafePoints;
	public Vector3 ActiveWaypoint;
	Vector3 aux;
	float align;
	bool grounded = false;

	void Start() {
		grounded = false;
		InitialMaxSpeedFwd = MaxSpeedFwd;
		MaxSpeedFwd = 0;
		body = GetComponent<Rigidbody>();
		layerMask = 1 << LayerMask.NameToLayer("Vehicle");
		layerMask = ~layerMask;
		state = 0;
	}

	void OnDrawGizmos() {
		//  Hover Force
		RaycastHit hit;
		for (int i = 0; i < hoverPoints.Length; i++) {
			var hoverPoint = hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position,Vector3.down, out hit,hoverHeight,layerMask)) {
				Gizmos.color = Color.blue;
				Gizmos.DrawLine(hoverPoint.transform.position, hit.point);
				Gizmos.DrawSphere(hit.point, 0.05f);
			}
			else {
				Gizmos.color = Color.red;
				Gizmos.DrawLine(hoverPoint.transform.position,hoverPoint.transform.position - Vector3.up * hoverHeight);
			}
		}
	}

	void Update() {

		if (state == 0 && grounded)
			Accel ();
		else if (state == 1 && grounded)
			Slow ();
	}

	void FixedUpdate() {

		//Reset Position
		RaycastHit hit;
		if (!Physics.Raycast (center.transform.position, Vector3.down, out hit, hoverHeight * 30, layerMask)) {
			grounded = false;
			aux = body.transform.position;
			Invoke ("Reset", 1);
		} else
			grounded = true;
		//  Hover Force
		for (int i = 0; i < hoverPoints.Length; i++) {
			var hoverPoint = hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position,Vector3.down, out hit,hoverHeight,layerMask))
				body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
			else {
				body.AddForceAtPosition(hoverPoint.transform.up * -hoverForce,hoverPoint.transform.position);
			}
		}
	}

	void OnCollisionEnter(Collision collision) {
		//if (collision.relativeVelocity.magnitude > 2)
		//	audio.play ();
	}
		
	void OnTriggerEnter() {
		if(!turboOn)
			state = 1;
	}

	void OnTriggerExit() {
		state = 0;
	}

	void Accel() {
		if (accelState == false) {
			accelState = true;
			slowState = false;
		}
		// Normal Align
		RaycastHit hit;
		if (Physics.Raycast(center.transform.position,-center.transform.up, out hit,hoverHeight*30,layerMask)) {
			Vector3 myNormal = Vector3.Lerp(transform.up, hit.normal,10*Time.deltaTime);
			Vector3 myForward = Vector3.Cross(transform.right, myNormal);
			Quaternion rot = Quaternion.LookRotation(myForward,myNormal);
			transform.rotation = Quaternion.Slerp(transform.rotation,rot,10*Time.deltaTime);
		}
		// Turn & Incline
		Vector3 direction = ActiveWaypoint-transform.position;
		turn = 1.0f - Mathf.Abs(Vector3.Dot(transform.forward.normalized, direction.normalized));
		if (Dir(transform.forward.normalized,direction.normalized,transform.up.normalized) == -1f)
			turn = -turn;
		if (Mathf.Abs(turn) > 0)
			body.AddRelativeTorque (Vector3.up * turn * turnStrength);

		if (turn > deadZone) {
			incline += 0.5f;
			incline = Mathf.Clamp (incline, -maxAngle, maxAngle);
		} else if (turn < -deadZone) {
			incline -= 0.5f;
			incline = Mathf.Clamp (incline, -maxAngle, maxAngle);
		} else {
			if (incline < 0.0f) {
				incline += 0.5f;
				incline = Mathf.Clamp (incline,-maxAngle,0);
			} else {
				incline -= 0.5f;
				incline = Mathf.Clamp (incline,0,maxAngle);
			}
		}
		model.transform.localEulerAngles = new Vector3 (model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, -incline);

		// Turbo
		if (turboOn) {
			time -= Time.deltaTime;
			if (time <= 0) {
				stopturbo();
				turboOn = false;
			}
		}

		// Forward
		if(turboOn)
			thrust += accel*8;
		else 
			thrust += accel;
		if (thrust > MaxSpeedFwd)
			thrust = MaxSpeedFwd;
		if (Mathf.Abs (thrust) > deadZone) 
			body.AddForce (transform.forward * thrust);
	}

	void Slow() {
		if (slowState == false) {
			accelState = false;
			slowState = true;
		}
		thrust = thrust * inertia;
		if (thrust <= MinSpeedFwd) {
			thrust = MinSpeedFwd; 
			state = 0;
		}
		body.AddForce (transform.forward * thrust);
	}

	float Dir(Vector3 fwd, Vector3 targetDir, Vector3 up) {
		Vector3 perp = Vector3.Cross(fwd, targetDir);
		float dir = Vector3.Dot(perp, up);

		if (dir > 0f) { //Right
			return 1f;
		} else if (dir < 0f) { //Left
			return -1f;
		} else { //Centered
			return 0f;
		}
	}
			
	public void Reset() {
		Transform[] auxPoints = SafePoints.GetComponentsInChildren<Transform>();
		body.velocity = new Vector3(0.0f,0.0f,0.0f);
		body.angularVelocity = new Vector3(0.0f,0.0f,0.0f);
		float closest = 99999999999;
		Transform safePoint = auxPoints[0];

		// Go to the closest safepoint
		foreach(Transform trans in auxPoints) {
			float dist = Vector3.Distance (aux, trans.position);
			if (!trans.Equals (SafePoints.transform)) {
				if (dist < closest) {
					closest = dist;
					safePoint = trans;
				}
			}
		}
		body.transform.position = safePoint.position;
		body.transform.forward = safePoint.forward;
	}

	public void turbo(float t){
		time += t;
		turboOn = true;
		if (MaxSpeedFwd == InitialMaxSpeedFwd) {
			MaxSpeedFwd *= turboBoost;
		}
	}

	public void stopturbo(){
		turboOn = false;
		time = 0.0f;
		MaxSpeedFwd = InitialMaxSpeedFwd;
	}

	public void go(){
		MaxSpeedFwd = InitialMaxSpeedFwd;
	}
}

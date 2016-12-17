using UnityEngine;
using System.Collections;

public class HoverCarControl : MonoBehaviour {
	
	Rigidbody body;
	public GameObject model;
	public GameObject center;
	public GameObject camara;
	public GameObject[] hoverPoints;

	public float deadZone = 0.1f;
	public float hoverForce = 9.0f;
	public float bounce = 3.0f;
	public float hoverHeight = 2.0f;

	float thrust = 0.0f;
	public float MaxSpeedFwd = 100.0f;
	public float MaxSpeedBkd = 25.0f;
	public float turboBoost = 1.5f;
	private float time = 0.0f;
	private bool turboOn = false;

	private float InitialMaxSpeedFwd;
	private float InitialMaxSpeedBkd;

	private GameObject velocityBar;
	public GameObject V1;
	public GameObject V2;
	private UpdateText v1Text;
	private UpdateText v2Text;


	float turn = 0.0f;
	public float turnStrength = 10f;
	public float incline = 0.0f;
	public float maxAngle = 10.0f;

	int layerMask;
	public GameObject SafePoints;
	Vector3 aux;
	float align;

	void Start() {
		body = GetComponent<Rigidbody>();
		layerMask = 1 << LayerMask.NameToLayer("Vehicle");
		layerMask = ~layerMask;
		InitialMaxSpeedFwd = MaxSpeedFwd;
		InitialMaxSpeedBkd = MaxSpeedBkd;
		MaxSpeedFwd = 0;
		MaxSpeedBkd = 0;
		velocityBar = GameObject.Find ("AccelBar");
		v1Text = V1.GetComponent(typeof(UpdateText)) as UpdateText;
		v2Text = V2.GetComponent(typeof(UpdateText)) as UpdateText;

	}

	void OnDrawGizmos() {
		//  Hover Force
		RaycastHit hit;
		for (int i = 0; i < hoverPoints.Length; i++) {
			var hoverPoint = hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position, Vector3.down, out hit,hoverHeight,layerMask)) {
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
		velocityBar.SendMessage("setValue", body.velocity.magnitude * (75.0f/8.0f));
		v1Text.UpdateVU(body.velocity.magnitude);
		v2Text.UpdateVD(body.velocity.magnitude);

		//Turbo
		if (turboOn) {
			time -= Time.deltaTime;
			if (time <= 0) {
				stopturbo();
				turboOn = false;
			}
		}

		// Main Thrust
		thrust = 0.0f;
		float aclAxis = Input.GetAxis("Vertical");
		if (aclAxis > deadZone)
			thrust = aclAxis * MaxSpeedFwd;
		else if (aclAxis < -deadZone)
			thrust = aclAxis * MaxSpeedFwd;

    	// Turning
    	turn = 0.0f;
    	float turnAxis = Input.GetAxis("Horizontal");
		if (Mathf.Abs(turnAxis) > deadZone) {
      		turn = turnAxis;
		}
	}
		
	void FixedUpdate() {
		
		// Reset Position
		RaycastHit hit;
		if (!Physics.Raycast (center.transform.position, -center.transform.up, out hit, hoverHeight * 30, layerMask)) {
			aux = body.transform.position;
			Invoke ("Reset", 1);
		} 

		//  Hover Force
		for (int i = 0; i < hoverPoints.Length; i++) {
			var hoverPoint = hoverPoints [i];
			if (Physics.Raycast(hoverPoint.transform.position,Vector3.down, out hit,hoverHeight,layerMask))
				body.AddForceAtPosition(Vector3.up * hoverForce * (1.0f - (hit.distance / hoverHeight)), hoverPoint.transform.position);
			else {
				body.AddForceAtPosition(hoverPoint.transform.up * -hoverForce,hoverPoint.transform.position);
			}
		}
		// Normal Align
		if (Physics.Raycast(center.transform.position,-center.transform.up,out hit,hoverHeight*30,layerMask)) {
			Vector3 myNormal = Vector3.Lerp(transform.up, hit.normal,10*Time.deltaTime);
			Vector3 myForward = Vector3.Cross(transform.right, myNormal);
			Quaternion rot = Quaternion.LookRotation(myForward,myNormal);
			transform.rotation = Quaternion.Slerp(transform.rotation,rot,10*Time.deltaTime);
		}

		// Forward
		if (Mathf.Abs (thrust) > 0) {
			body.AddForce (transform.forward * thrust);
		}

		// Turn & incline
		if (turn > 0) {
			body.AddRelativeTorque (Vector3.up * turn * turnStrength);
			incline += 0.5f;
			incline = Mathf.Clamp (incline, -maxAngle, maxAngle);
		} else if (turn < 0) {
			body.AddRelativeTorque (Vector3.up * turn * turnStrength);
			incline -= 0.5f;
			incline = Mathf.Clamp (incline, -maxAngle, maxAngle);
		} else {
			if (incline < 0) {
				incline += 0.5f;
				incline = Mathf.Clamp (incline,-maxAngle,0);
			} else {
				incline -= 0.5f;
				incline = Mathf.Clamp (incline,0,maxAngle);
			}
		}
		model.transform.localEulerAngles = new Vector3 (model.transform.localEulerAngles.x, model.transform.localEulerAngles.y, -incline);
	}
		
	/*void OnCollisionEnter(Collision collision) {
		//if (collision.relativeVelocity.magnitude > 2)
		//	audio.play ();
	}*/

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
		camara.GetComponent<CameraShake>().shakeDuration = time;
		turboOn = true;
		if (MaxSpeedFwd == InitialMaxSpeedFwd) {
			MaxSpeedFwd *= turboBoost;
			MaxSpeedBkd *= turboBoost;
		}
	}

	public void stopturbo(){
		turboOn = false;
		time = 0.0F;
		MaxSpeedFwd = InitialMaxSpeedFwd;
		MaxSpeedBkd = InitialMaxSpeedBkd;
	}

	public void go(){
		MaxSpeedFwd = InitialMaxSpeedFwd;
		MaxSpeedBkd = InitialMaxSpeedBkd;
	}
}

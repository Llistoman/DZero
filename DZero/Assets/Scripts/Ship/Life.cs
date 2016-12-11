using UnityEngine;
using System.Collections;

public class Life : MonoBehaviour {

	// Use this for initialization
	private GameObject energyBar;
	private float life;
	public float MAXlife = 100.0f;

	void Start () {
		life = MAXlife;
		energyBar = GameObject.Find ("EnergyBar");
	}

	void dmg (float f){
		life -= f;
		if (life < 0.0f) life = 0.0f;
		energyBar.SendMessage("setValue", life);
	}

	void heal (float f){
		life += f;
		if (life > MAXlife) life = MAXlife;
		energyBar.SendMessage("setValue", life);
	}
}

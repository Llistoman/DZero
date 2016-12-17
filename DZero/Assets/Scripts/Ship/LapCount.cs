using UnityEngine;
using System.Collections;

public class LapCount : MonoBehaviour {

	private bool checkPoint = false;
	public int lapCount = 1;
	public int rank;
	public Transform WaypointObject;
	public GameObject Victim;
	private UpdateText lapText;
	private UpdateText positionText;
	private UpdateText positionText2;

	void Start () {
		lapText = GameObject.Find("LapC1").GetComponent(typeof(UpdateText)) as UpdateText;
		positionText = GameObject.Find("PositionText").GetComponent(typeof(UpdateText)) as UpdateText;
		positionText2 = GameObject.Find("PositionText2").GetComponent(typeof(UpdateText)) as UpdateText;
	}

	void Update () {
		if (gameObject.tag == "Player") {
			positionText.UpdateInt (rank);
			positionText2.UpdateRank2 (rank);
		}
	}
		
	public void goal(){
		if (checkPoint) {
			lapCount++;
			checkPoint = false;
			if (gameObject.tag == "Player") {
				if (lapCount == 4) {
					//TODO: Falta mirar si vas primer o no per mostrar un canvas o l'altre
					if (rank == 1)gameObject.SendMessage ("Win");
					else gameObject.SendMessage ("GameOver");
				}
				string s = lapCount + "/3";
				lapText.UpdateString(s);
			}
		}
	}

	public void halfGoal(){
		checkPoint = true;
	}
}

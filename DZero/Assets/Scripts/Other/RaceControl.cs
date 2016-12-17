using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using System.Linq;

public class RaceControl : MonoBehaviour {

	public GameObject Player1;
	public GameObject Player2;
	private int numEnems = 8;
	private GameObject[] enemyShips;
	private GameObject Player;
	private GameObject[] allShips;

	// Use this for initialization
	void Start () {
		enemyShips = GameObject.FindGameObjectsWithTag ("AI").OrderBy(g=>g.transform.GetSiblingIndex()).ToArray();
		if (ApplicationModel.nauSeleccionada == 0)
			Player = Player1;
		else
			Player = Player2;
		allShips = new GameObject[numEnems+1];
		allShips [0] = Player;
		for (int i = 0; i < enemyShips.Length; ++i) {
			allShips [i+1] = enemyShips [i];
		}
		//Debug.Log (allShips [0].name);
		//Debug.Log (allShips [1].name);
	}

	// Update is called once per frame
	void Update () {
		List<GameObject> aux = allShips.ToList();
		//GameObject[] aux2 = aux.ToArray ();
		/*for (int i = 0; i < aux2.Length; ++i) {
			Debug.Log (aux2 [i].name);
		}*/
		aux.Sort(delegate (GameObject a, GameObject b) {
			if (a.GetComponent<LapCount> ().lapCount > b.GetComponent<LapCount> ().lapCount)
				return -1;
			else if (a.GetComponent<LapCount> ().lapCount < b.GetComponent<LapCount> ().lapCount)
				return 1;
			else {
				if (a.GetComponent<LapCount> ().WaypointObject.GetSiblingIndex () > b.GetComponent<LapCount> ().WaypointObject.GetSiblingIndex ())
					return -1;
				else if (a.GetComponent<LapCount> ().WaypointObject.GetSiblingIndex () < b.GetComponent<LapCount> ().WaypointObject.GetSiblingIndex ())
					return 1;
				else {
					float distA = Vector3.Distance (a.transform.position, a.GetComponent<LapCount> ().WaypointObject.position);
					float distB = Vector3.Distance (b.transform.position, b.GetComponent<LapCount> ().WaypointObject.position);
					if (distA < distB)
						return -1;
					else
						return 1;
				}
			}
		});
		GameObject[] aux2 = aux.ToArray ();
		for (int i = 0; i < aux2.Length; ++i) {
			aux2[i].GetComponent<LapCount> ().rank = i + 1;
			int next = i-1;
			if (next < 0)
				next = aux2.Length-1;
			aux2 [i].GetComponent<LapCount> ().Victim = aux2 [next];
		}
	}
}

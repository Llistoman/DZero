using UnityEngine;
using System.Collections;

public class ConfigNauSelect : MonoBehaviour {
	public GameObject Bar1;
	public GameObject Bar2;
	public GameObject Bar3;
	public GameObject Nau1;
	public GameObject Nau2;

	private bool change = false;
	private bool selected = false;

	
	// Update is called once per frame
	void Update () {
		if (change) {
			selected = !selected;
			StartCoroutine (ChangeValues (selected));
			change = false;
		}
	}
		
	IEnumerator ChangeValues(bool s){
		yield return new WaitForSeconds (0.6F);
		if (s) {
			Bar1.SendMessage ("setValue", 100);
			Bar2.SendMessage ("setValue", 70);
			Bar3.SendMessage ("setValue", 50);
			Nau1.SetActive (false);
			Nau2.SetActive (true);
			ApplicationModel.nauSeleccionada = 1;

		} else {
			Bar1.SendMessage ("setValue", 70);
			Bar2.SendMessage ("setValue", 80);
			Bar3.SendMessage ("setValue", 100);
			Nau2.SetActive (false);
			Nau1.SetActive (true);
			ApplicationModel.nauSeleccionada = 0;
		}

	}

	void ChangeOn(){
		change = true;
	}
}

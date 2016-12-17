using UnityEngine;
using System.Collections;


public class Config : MonoBehaviour {

	public float timer = 0.0f;
	public GameObject Nau1;
	public GameObject Nau2;

	private bool ready = false;

	public GameObject[] Enemics;

	private UpdateText timeText;
	
	// Use this for initialization
	void Start () {
		Enemics = GameObject.FindGameObjectsWithTag ("AI");
		if (ApplicationModel.nauSeleccionada == 0) {
			Nau1.SetActive (true);
			Nau2.SetActive (false);
		}
		else if (ApplicationModel.nauSeleccionada == 1 ){
			Nau1.SetActive (false);
			Nau2.SetActive (true);
		}

		timeText = GameObject.Find("temps").GetComponent(typeof(UpdateText)) as UpdateText;
	}
	
	// Update is called once per frame
	void Update () {
		if(ready)timer += (Time.deltaTime);
			timeText.UpdateTimer(timer);
	}

	void gameOver(){
		//gameOver
	}

	void go(){
		foreach(GameObject ship in Enemics){
			ship.SendMessage ("go");
			ready = true;
		}
		if(ApplicationModel.nauSeleccionada == 0) Nau1.SendMessage ("go");
		else if(ApplicationModel.nauSeleccionada == 1) Nau2.SendMessage ("go");
	}
}

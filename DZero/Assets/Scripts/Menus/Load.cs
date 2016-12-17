using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Load : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("escape")) {
			if (Application.loadedLevelName == "Menu") {
				Application.Quit ();
			}
			else SceneManager.LoadScene ("Menu");
		}
		if (Input.GetKeyDown ("r")) {
			SceneManager.LoadScene(Application.loadedLevel);
		}
	}


	void LoadLevel (string s){
		SceneManager.LoadScene(s);
	}

	void LoadNauSelect (){
		StartCoroutine (LL ("NauSelect"));
	}

	void LoadCircuitSelect (){
		StartCoroutine (LL ("CircuitSelect"));
	}

	void LoadInstruccions (){
		StartCoroutine (LL ("Instruccions"));
	}

	void LoadCredits (){
		StartCoroutine (LL ("Credits"));
	}

	void LoadMenu (){
		StartCoroutine (LL ("Menu"));
	}

	void LoadCircuit3 (){
		StartCoroutine (LL ("Circuit3"));
	}

	void LoadCircuit2 (){
		StartCoroutine (LL ("Circuit2"));
	}

	void LoadCircuit1 (){
		StartCoroutine (LL ("Circuit1"));
	}

	void LoadLevelYield (string s){
		StartCoroutine (LL (s));
	}

	IEnumerator LL(string s){
		yield return new WaitForSeconds (1);
		if(s == "Circuit3" || s == "Circuit2" || s== "Circuit1") Destroy (AudoBetweenScenes.instance.gameObject);
		SceneManager.LoadScene(s);
	}

	IEnumerator RL(){
		yield return new WaitForSeconds (1);
		SceneManager.LoadScene(Application.loadedLevel);
	}

	void Reload () {
		StartCoroutine ((RL()));
	}

}

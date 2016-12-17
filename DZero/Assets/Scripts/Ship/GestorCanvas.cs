using UnityEngine;
using System.Collections;

public class GestorCanvas : MonoBehaviour {

	public GameObject canvasGeneral;
	public GameObject canvasGameOver;
	public GameObject canvasWin;

	public void Win(){
		canvasGeneral.SetActive (false);
		canvasGameOver.SetActive (false);
		canvasWin.SetActive (true);
	}

	public void GameOver(){
		canvasGeneral.SetActive (false);
		canvasGameOver.SetActive (true);
		canvasWin.SetActive (false);
	}

}

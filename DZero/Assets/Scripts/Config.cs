using UnityEngine;
using System.Collections;


public class Config : MonoBehaviour {

	public float timer = 0.0f;

	private UpdateText timeText;
	
	// Use this for initialization
	void Start () {
		timeText = GameObject.Find("temps").GetComponent(typeof(UpdateText)) as UpdateText;
	}
	
	// Update is called once per frame
	void Update () {
			timer += (Time.deltaTime);
			timeText.UpdateTimer(timer);
	}

	void gameOver(){
		//gameOver
	}
}

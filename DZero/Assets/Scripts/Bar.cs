using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif
public class Bar : MonoBehaviour {

	public float value = 100.0F;
	public float MAXvalue = 100.0F;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.GetComponent<Image>().fillAmount = value / 100.0F;
	}

	void setValue(float v){
		value = v;
		if(value > MAXvalue) value = MAXvalue;
	}
}

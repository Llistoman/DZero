﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UpdateText : MonoBehaviour {
	public Text text;
	
	public void UpdateInt(int x)
	{
		text.text = x.ToString();
	}
	
	public void UpdateTimer(float x)
	{
		text.text = ((int)x/60).ToString("00") + ":" + ((int)x%60).ToString("00") + ":" + ((int)(x*100)%100).ToString("00");

	}

	public void UpdateVU(float x)
	{
		text.text = (x*(350.0f/7.0f)).ToString("000");
	}

	public void UpdateVD(float x)
	{
		text.text = ((x*100)%100).ToString("0");
	}

	public void UpdateFloat(float x)
	{
		text.text = x.ToString(); 
	}

	public void UpdateFloat(float x, string y)
	{
		text.text = x.ToString(y);
	}
	
	public void UpdateString(string y)
	{
		text.text = y;
	}

	public void UpdateRank1(int v)
	{
		text.text = v.ToString();
	}

	public void UpdateRank2(int v)
	{
		if (v == 1) {
			text.text = "ST";
		}
		else if (v == 2) {
			text.text = "ND";
		}
		else if (v == 3) {
			text.text = "RD";
		}
		else  {
			text.text = "TH";
		}
	}
}
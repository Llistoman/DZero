using UnityEngine;
using System.Collections;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class UpdateSprite : MonoBehaviour {
	public Image icon;
	
	public void UpdateImage(Material x)
	{
		icon.material = x;
	}

}
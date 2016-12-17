using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class AudoBetweenScenes : MonoBehaviour {
	public static AudoBetweenScenes instance = null;

	public static AudoBetweenScenes Instance {
		get{ return instance; }
	}

	// Update is called once per frame
	void Awake () {
		if (instance != null && instance != this) {
         Destroy(this.gameObject);
         return;
     } else {
         instance = this;
     }
		DontDestroyOnLoad(gameObject);
	}

}

using UnityEngine;
using System.Collections;

public class Dont_Destroy_On_Scene_Load : MonoBehaviour {

	void Awake () {
		DontDestroyOnLoad(gameObject);
	}
}
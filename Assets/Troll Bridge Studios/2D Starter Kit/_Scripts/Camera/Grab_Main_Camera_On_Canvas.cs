using UnityEngine;
using System.Collections;

public class Grab_Main_Camera_On_Canvas : MonoBehaviour {

	void OnLevelWasLoaded(int level){
		GameObject camObject = GameObject.FindGameObjectWithTag("MainCamera");
		Canvas camCanvas = GetComponent<Canvas>();
		Camera cam1 = camObject.GetComponent<Camera>();
		camCanvas.worldCamera = cam1;
	}
}
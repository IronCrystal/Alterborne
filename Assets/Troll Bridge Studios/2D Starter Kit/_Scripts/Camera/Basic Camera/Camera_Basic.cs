using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class Camera_Basic : MonoBehaviour {

	// Used for the user to input their board section width and height.
	[Tooltip("The desired Camera Width.")]
	public int cameraWidth;
	[Tooltip("The desired Camera Height.")]
	public int cameraHeight;
	// The Camera and min and max bounds.
	private Camera _camera;


	void Awake () {
		// Grab the Camera Component.
		_camera = GetComponent<Camera>();
		// Set the camera ratios.
		Helper_Manager.instance.SetCameraRatios((float)cameraWidth, (float)cameraHeight, 
		                                        Screen.width, Screen.height, _camera);
	}

	void Start(){
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
	}

	void DebugCheck(){
		// IF the user forgot to put in the Cameras Width.
		if(cameraWidth <= 0){
			Helper_Manager.instance.DebugErrorCheck(2, this.GetType(), gameObject);
		}
		// IF the user forgot to put in the Cameras Height.
		if(cameraHeight <= 0){
			Helper_Manager.instance.DebugErrorCheck(3, this.GetType(), gameObject);
		}
	}
}
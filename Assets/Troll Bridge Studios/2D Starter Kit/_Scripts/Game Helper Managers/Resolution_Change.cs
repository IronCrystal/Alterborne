using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Resolution_Change : MonoBehaviour {

	[Tooltip("The width of the resolution to be changed to.")]
	public int width;
	[Tooltip("The height of the resolution to be changed to.")]
	public int height;
	[Tooltip("The Resolution Drop Down Menu Text.")]
	public Text resolutionDropdownText;

	void Start(){
		DebugCheck();
	}

	void DebugCheck(){
		// IF the user forgot to put in the Width.
		if(width <= 0){
			Helper_Manager.instance.DebugErrorCheck(2, this.GetType(), gameObject);
		}
		// IF the user forgot to put in the Height.
		if(height <= 0){
			Helper_Manager.instance.DebugErrorCheck(3, this.GetType(), gameObject);
		}
	}

	public void ChangeResolution(){
		Helper_Manager.instance.ChangeResolution(width, height, Screen.fullScreen);
		// IF there is a resolutions drop down Text.
		if(resolutionDropdownText != null){
			resolutionDropdownText.text = (width + "x" + height).ToString();
		}
	}
}
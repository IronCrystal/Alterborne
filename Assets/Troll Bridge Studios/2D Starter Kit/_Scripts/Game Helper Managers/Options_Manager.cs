using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Options_Manager : MonoBehaviour {

	// The Key that activates the Options Menu.
	[Tooltip("The Key that is used to bring up the options.")]
	public KeyCode optionsKey;
	// The Canvas the options are part of.
	public Canvas canvasGO;
	// The Panel the options are on.
	public GameObject optionsPanel;
	// The scenes that you do not want the optionsKey to work on.
	// These are the scenes such as Main Menu, cut scenes, etc.
	[Tooltip("The scenes that you do not want the optionsKey to work on.  Examples would be cut scenes or main menus.")]
	public string[] noOptionKeyScenes;

	// The music slider that is used for dictating the volume of the music.
	public Slider musicSlider;
	// the music toggle that is used for muting and unmuting the music.
	public Toggle musicToggle;
	// The sfx slider that is used for dictating the volume of the sfx.
	public Slider sfxSlider;
	// The sfx toggle that is used for muting and unmuting the sfx.
	public Toggle sfxToggle;
	
	// The Panel that the scaling will be applied to.
	public GameObject scalePanel;
	// The slider that adjusts the scaling.
	public Slider UISlider;
	// The text to display to notify the user what the scaling is.
	public Text UISliderText;

	// The fullscreen toggle.
	public Toggle fullscreen;

	// The resolutions dropdown.
	[Tooltip("The resolution dropdown menu Text.")]
	public Text resolutionDropdownText;

	// Used to store the inital scaling.
	private float xPanelScale, yPanelScale;

	// Offset used for UI scaling.
	// Defualt is at 0.5 due to default scaling is 0.5 so, 0.5 + 0.5 = 1 (original scaling).
	// When the slider is at 0.5 (halfway/default).
	private float UISliderOffset = 0.5f;

	// For faster calculation on UI scaling.
	private bool uiSliderTextExist;
	

	void Awake(){
		// The options are always persistent throughout the game.
		DontDestroyOnLoad(gameObject);

		// Check to make sure the user has the scripts working correctly.
		DebugCheck();

		// Change settings based on 
		ScalePanelCheck();

		// Set the resolution drop down menu text.
		SetResolutionDropDownText(Screen.width, Screen.height);

		// Set the fullscreen option to what was either made by default 
		// or saved from last session.
		if(fullscreen != null && Application.platform != RuntimePlatform.WebGLPlayer){
			fullscreen.isOn = Screen.fullScreen;
		}
	}

	void Start () {
		// Set the toggles and sliders in Sound_Manager.
		Sound_Manager.instance.SetSlidersAndToggles(musicSlider, musicToggle, sfxSlider, sfxToggle);
	}

	void Update() {
		// IF the user hits Escape.
		if(Input.GetKeyUp(optionsKey)){
			// Bool for the noOptionKeyScenes.
			bool hit = false;
			// Loop through the noOptionKeyScenes.
			for(int i = 0; i < noOptionKeyScenes.Length; i++){
				// IF our current scene matches the scenes we do not want to display options.
				if(Application.loadedLevelName == noOptionKeyScenes[i]){
					// We have a match.
					hit = true;
				}
			}
			// IF we didn't get a match on not bring up the options.
			if(!hit){
				// IF the panel is not showing then display it and pause the game.
				// ELSE the panel is showing then remove it and unpause the game.
				if(!optionsPanel.activeInHierarchy){
					OptionsDisplay(true);
					Helper_Manager.instance.SetTimeScale(0f);
				}else{
					OptionsDisplay(false);
					Helper_Manager.instance.SetTimeScale(1f);
				}
			}
		}
	}

	void DebugCheck(){
		// IF we have set our keycode to something other than None.
		if(optionsKey != KeyCode.None){
			// IF we don't have a Canvas that the options is on.
			if(canvasGO == null){
				Helper_Manager.instance.DebugErrorCheck(100, this.GetType(), gameObject);
			}
			// IF we don't have a Panel to display the options.
			if(optionsPanel == null){
				Helper_Manager.instance.DebugErrorCheck(101, this.GetType(), gameObject);
			}
		}
	}

	void ScalePanelCheck(){
		// Do we have UISliderText
		uiSliderTextExist = UISliderText != null;

		// IF the scalePanel is equal to null,
		// ELSE we have a scalePanel.
		if(scalePanel == null){
			// IF the user has a UISlider.
			if(UISlider != null){
				// Make the UISlider non-interactable.
				UISlider.interactable = false;
			}
		}else{
			// Get the original X and Y scale of the Scaling Panel.
			xPanelScale = scalePanel.transform.localScale.x;
			yPanelScale = scalePanel.transform.localScale.y;
		}
	}

	void SetResolutionDropDownText(int width, int height){
		// IF we have a resolution dropdown text.
		if(resolutionDropdownText != null){
			// Set the text.
			resolutionDropdownText.text = (width + "x" + height).ToString();
		}
	}

	public void OptionsDisplay(bool active){
		optionsPanel.SetActive(active);
	}

	public void RescaleUI() {

		float sliderMultiplier;

		// Set the ui scale multiplier for the panel.  This is based
		// on the current slider value + the default offset (0.5f)
		sliderMultiplier = UISlider.value + UISliderOffset;
		// What ever the value is we need to make sure we apply this
		// to all the children in the Panel Parent.
		for(int i = 0; i < scalePanel.transform.childCount; i++){
			scalePanel.transform.GetChild(i).localScale = new Vector3(xPanelScale * sliderMultiplier,
			                                               yPanelScale * sliderMultiplier,
			                                               1f);
		}
		// IF we have slider text.
		if(uiSliderTextExist){
			// Set the text to display the UI scaling number.
			UISliderText.text = UISlider.value.ToString("F2");
		}
	}

	public void MusicToggle(){
		Sound_Manager.instance.MuteUnMuteBGMusic(musicToggle.isOn);
	}
	
	public void MusicSlider(){
		Sound_Manager.instance.ChangeMusicVolume(musicSlider.value);
	}
	
	public void SFXToggle(){
		Sound_Manager.instance.MuteUnMuteSound(sfxToggle.isOn);
	}
	
	public void SFXSlider(){
		Sound_Manager.instance.ChangeSFXVolume(sfxSlider.value);
	}

	public void ChangeScreenMode(){
		Helper_Manager.instance.ChangeScreenMode(fullscreen.isOn);
	}
}
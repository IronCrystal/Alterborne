using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Options_Manager))]
public class Options_Manager_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Options_Manager myTarget = target as Options_Manager;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		EditorGUILayout.LabelField("Options Panel", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Get the keycode used to bring up the options.
		SerializedProperty optionsKeycode = serializedObject.FindProperty("optionsKey");
		// Set the Keycode.
		EditorGUILayout.PropertyField(optionsKeycode);
		// The Canvas that the settings are displayed on.
		myTarget.canvasGO = EditorGUILayout.ObjectField(new GUIContent("Options Canvas","The Canvas that the option settings are displayed on."),
		                                                    myTarget.canvasGO,
		                                                    typeof(Canvas),
		                                                    true) as Canvas;
		// The options panel that the settings are displayed on.
		myTarget.optionsPanel = EditorGUILayout.ObjectField(new GUIContent("Options Panel","The panel (GameObject) that the settings are displayed on."),
		                                                   myTarget.optionsPanel,
		                                                   typeof(GameObject),
		                                                   true) as GameObject;
		// The scenes that you do not want the options hot key to work on.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("noOptionKeyScenes"), true);
		// Decrease the indent.
		EditorGUI.indentLevel--;


		EditorGUILayout.LabelField("Sound Options", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// The music slider that is used for dictating the volume of the music.
		myTarget.musicSlider = EditorGUILayout.ObjectField(new GUIContent("Music Slider","The slider used to gauge the volume of the music"),
		                                                   myTarget.musicSlider,
		                                                   typeof(Slider),
		                                                   true) as Slider;
		// the music toggle that is used for muting and unmuting the music.
		myTarget.musicToggle = EditorGUILayout.ObjectField(new GUIContent("Music Toggle","The toggle used to mute/unmute the volume of the music"),
		                                                   myTarget.musicToggle,
		                                                   typeof(Toggle),
		                                                   true) as Toggle;
		// The sfx slider that is used for dictating the volume of the sfx.
		myTarget.sfxSlider = EditorGUILayout.ObjectField(new GUIContent("SFX Slider","The slider used to gauge the volume of the sound effects"),
		                                                   myTarget.sfxSlider,
		                                                   typeof(Slider),
		                                                   true) as Slider;
		// The sfx toggle that is used for muting and unmuting the sfx.
		myTarget.sfxToggle = EditorGUILayout.ObjectField(new GUIContent("SFX Toggle","The toggle used to mute/unmute the volume of the music"),
		                                                   myTarget.sfxToggle,
		                                                   typeof(Toggle),
		                                                   true) as Toggle;
		// Decrease the indent.
		EditorGUI.indentLevel--;


		EditorGUILayout.LabelField("UI Scaling", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// The options panel that the settings are displayed on.
		myTarget.scalePanel = EditorGUILayout.ObjectField(new GUIContent("Scaling Panel","The panel (GameObject) in which all its first children will be adjusted via the UI Slider."),
		                                                    myTarget.scalePanel,
		                                                    typeof(GameObject),
		                                                    true) as GameObject;
		// The slider which will adjust the scaling of the children of Scaling Panel.
		myTarget.UISlider = EditorGUILayout.ObjectField(new GUIContent("Scaling Slider","The slider which will adjust the scaling of the children of Scaling Panel."),
		                                                  myTarget.UISlider,
		                                                  typeof(Slider),
		                                                  true) as Slider;
		// The text which displays the current scaling.
		myTarget.UISliderText = EditorGUILayout.ObjectField(new GUIContent("Scaling Text","The text which displays the current scaling on the slider."),
		                                                  myTarget.UISliderText,
		                                                  typeof(Text),
		                                                  true) as Text;
		// Decrease the indent.
		EditorGUI.indentLevel--;


		EditorGUILayout.LabelField("Fullscreen Toggle", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// The toggle that makes the game fullscreen and windowed.
		myTarget.fullscreen = EditorGUILayout.ObjectField(new GUIContent("Fullscreen Toggle","The toggle that makes the game go to either fullscreen or windowed."),
		                                                  myTarget.fullscreen,
		                                                  typeof(Toggle),
		                                                  true) as Toggle;
		// Decrease the indent.
		EditorGUI.indentLevel--;


		EditorGUILayout.LabelField("Resolution", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		EditorGUIUtility.LookLikeControls(180f, 0f);
		// Get the resolution dropdown button.
		SerializedProperty resDropDown = serializedObject.FindProperty("resolutionDropdownText");
		// Set the resolution drop down menu.
		EditorGUILayout.PropertyField(resDropDown);
		// Decrease the indent.
		EditorGUI.indentLevel--;

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
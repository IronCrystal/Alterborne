using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Sound_Manager))]
public class Sound_Manager_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Sound_Manager myTarget = target as Sound_Manager;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// General Label.
		EditorGUILayout.LabelField("General", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// The Background.
		myTarget.bgMusicSource = EditorGUILayout.ObjectField(new GUIContent("BG Music Source","The background music AudioSource.  Drag the AudioSource in the inspector that is attached to this GameObject to this variable."),
		                                                     myTarget.bgMusicSource,
		                                                     typeof(AudioSource),
		                                                     true) as AudioSource;
		// Set the layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("musicOn"), new GUIContent("Music On"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("sfxOn"), new GUIContent("SFX On"));
		// The sound volume.
		myTarget.sfxVolume = EditorGUILayout.Slider(new GUIContent("SFX Volume", "The volume of the sound effects."), 
		                                            myTarget.sfxVolume, 
		                                            0f, 
		                                            1f);
		// Decrease the indent.
		EditorGUI.indentLevel--;

		// apply
		serializedObject.ApplyModifiedProperties();
	}
}
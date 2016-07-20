using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Terrain_Sound_By_Distance))]
public class Terrain_Sound_By_Distance_Editor : Editor {

	public override void OnInspectorGUI(){
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("soundClip"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("minPitch"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("maxPitch"));
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		EditorGUILayout.LabelField("Distance For Sound", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("distance"));
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
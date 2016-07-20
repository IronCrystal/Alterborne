using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Camera_Basic))]
public class Camera_Basic_Editor : Editor {

	public override void OnInspectorGUI(){
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// Resolution label.
		EditorGUILayout.LabelField("Camera Resolution", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Get the camera width.
		SerializedProperty width = serializedObject.FindProperty("cameraWidth");
		// Get the camera height.
		SerializedProperty height = serializedObject.FindProperty("cameraHeight");
		// Set the layout.
		EditorGUILayout.PropertyField(width);
		EditorGUILayout.PropertyField(height);
		// Clamp the desired values
		width.intValue = Mathf.Clamp(width.intValue, 0, 9999);
		height.intValue = Mathf.Clamp(height.intValue, 0, 9999);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
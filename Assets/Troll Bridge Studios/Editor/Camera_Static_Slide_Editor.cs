using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Camera_Static_Slide))]
public class Camera_Static_Slide_Editor : Editor{

	public override void OnInspectorGUI(){
		// Grab the script.
		Camera_Static_Slide myTarget = target as Camera_Static_Slide;
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

		// Camera Slide Movement label.
		EditorGUILayout.LabelField("Camera Panning", EditorStyles.boldLabel);
		// Edit the inspector.
		EditorGUIUtility.LookLikeControls(140f, 0f);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Player Slide Amount.
		SerializedProperty pSlideAmount = serializedObject.FindProperty("playerSlideAmount");
		// Camera Slide Speed.
		SerializedProperty camSlideSpeed = serializedObject.FindProperty("cameraSlideSpeed");
		// Set the layout.
		EditorGUILayout.PropertyField(pSlideAmount);
		EditorGUILayout.PropertyField(camSlideSpeed);
		// Clamp the desired values.
		pSlideAmount.floatValue = Mathf.Clamp(pSlideAmount.floatValue, 0f, 999f);
		camSlideSpeed.floatValue = Mathf.Clamp(camSlideSpeed.floatValue, 0f, 999f);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;


		// Scene Border label.
		EditorGUILayout.LabelField("Scene Boundaries", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// The top camera boundary.
		myTarget.bottomCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Bottom Boundary","Setting the bottom and left boundaries will automatically adjust your camera positioning."),
		                                                          myTarget.bottomCameraBorder,
		                                                          typeof(GameObject),
		                                                          true) as GameObject;
		// The left camera boundary
		myTarget.leftCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Left Boundary","Setting the bottom and left boundaries will automatically adjust your camera positioning."),
		                                                        myTarget.leftCameraBorder,
		                                                        typeof(GameObject),
		                                                        true) as GameObject;
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		// apply
		serializedObject.ApplyModifiedProperties();
	}
}
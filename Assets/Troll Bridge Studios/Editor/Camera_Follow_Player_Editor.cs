using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Camera_Follow_Player))]
public class Camera_Follow_Player_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Camera_Follow_Player myTarget = target as Camera_Follow_Player;
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


		// Scene Boundaries Label.
		EditorGUILayout.LabelField("Scene Boundaries", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Begin/Create a Toggle Group so that if this is checked you can set borders and gray out when disabled.
		myTarget.isCameraMoving = EditorGUILayout.BeginToggleGroup(new GUIContent("Is Camera Moving", "Create a moving (true) or stationary (false) Camera.  This can be useful in small scenes if you do not want the camera to follow the player."), 
		                                                           myTarget.isCameraMoving);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// The top camera border.
		myTarget.bottomCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Bottom Boundary","Bottom border for the Camera."),
		                                                          myTarget.bottomCameraBorder,
		                                                          typeof(GameObject),
		                                                          true) as GameObject;
		// The left camera border
		myTarget.leftCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Left Boundary","Left border for the Camera."),
		                                                        myTarget.leftCameraBorder,
	                                                      	    typeof(GameObject),
		                                                        true) as GameObject;
		// The top camera border
		myTarget.topCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Top Boundary","Top border for the Camera."),
		                                                       myTarget.topCameraBorder,
		                                                       typeof(GameObject),
		                                                       true) as GameObject;
		// The right camera border
		myTarget.rightCameraBorder = EditorGUILayout.ObjectField(new GUIContent("Right Boundary","Right border for the Camera."),
		                                                          myTarget.rightCameraBorder,
		                                                          typeof(GameObject),
		                                                          true) as GameObject;
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
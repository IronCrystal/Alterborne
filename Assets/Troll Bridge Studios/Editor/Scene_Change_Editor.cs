using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Scene_Change))]
public class Scene_Change_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Scene_Change myTarget = target as Scene_Change;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();
		
		// The Tag label.
		EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// The tags that can activate the scene change.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("targetTags"), new GUIContent("Tags That Activate", "The tags that can only activate the Scene Change."), true);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		// Edit the widths of the fields and labels.
		EditorGUIUtility.LookLikeControls(150, 20);
		EditorGUILayout.LabelField("Scene Change", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		myTarget.newScene = EditorGUILayout.TextField(new GUIContent("New Scene Name", "The name of the scene that will be loaded."), myTarget.newScene);
		myTarget.sceneSpawnLocation = EditorGUILayout.IntField(new GUIContent("Scene Spawn Location", "The location in the new scene that the player will be spawned at.  This number is correlated to the Transform locations on your Scene Manager Script."), myTarget.sceneSpawnLocation);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
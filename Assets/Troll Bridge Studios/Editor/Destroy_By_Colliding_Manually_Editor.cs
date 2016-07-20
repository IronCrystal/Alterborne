using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Destroy_By_Colliding_Manually))]
public class Destroy_By_Colliding_Manually_Editor : Editor {
	
	public override void OnInspectorGUI(){
		// Grab the script.
		Destroy_By_Colliding_Manually myTarget = target as Destroy_By_Colliding_Manually;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();


		// Get the set gameobjects inactive boolean
		myTarget.setInactive = EditorGUILayout.Toggle(new GUIContent("Set Inactive", "Set to 'true' if you want to set the gameobjects inactive rather than destroying."), myTarget.setInactive);
		// Begin/Create a Toggle Group so that if this is checked we can destroy on first collision.
		myTarget.onEnter = EditorGUILayout.BeginToggleGroup(new GUIContent("On Enter Collision", "Do we Destroy when we first enter collision."), myTarget.onEnter);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The tags that can activate the destroy mechanism on first collision.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onEnterActivateTags"), new GUIContent("Tags That Activate", "The tags that can only activate the Destroy."), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onEnterGameObjectsDestroy"), new GUIContent("GameObjects To Be Destroyed", "The GameObjects that are manually placed in here will be destroyed."), true);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		
		EditorGUILayout.Space();
		// ----------
		EditorGUILayout.Space();
		
		// Begin/Create a Toggle Group so that if this is checked we can destroy on first collision.
		myTarget.onExit = EditorGUILayout.BeginToggleGroup(new GUIContent("On Exit Collision", "Do we Destroy when we exit collision."), myTarget.onExit);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The tags that can activate the destroy mechanism on first collision.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onExitActivateTags"), new GUIContent("Tags That Activate", "The tags that can only activate the Destroy."), true);
		EditorGUILayout.PropertyField(serializedObject.FindProperty("onExitGameObjectsDestroy"), new GUIContent("GameObjects To Be Destroyed", "The GameObjects that are manually placed in here will be destroyed."), true);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
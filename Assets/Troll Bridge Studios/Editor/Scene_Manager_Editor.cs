using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Scene_Manager))]
public class Scene_Manager_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Scene_Manager myTarget = target as Scene_Manager;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// General Label.
		EditorGUILayout.LabelField("Background Music", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("music"));
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;


		// General Label.
		EditorGUILayout.LabelField("Player", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Begin ToggleGroup if there is a player.
		myTarget.isPlayer = EditorGUILayout.BeginToggleGroup(new GUIContent("Is Player", "Should there be a player spawned in this scene?"), myTarget.isPlayer);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Set the layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("player"));
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;


		// General Label.
		EditorGUILayout.LabelField("Spawn Locations", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the layout.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("sceneSpawnLocations"), true);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;
		// End the ToggleGroup.
		EditorGUILayout.EndToggleGroup();

		// General Label.
		EditorGUILayout.LabelField("UI", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Begin ToggleGroup if there is a player.
		myTarget.showUI = EditorGUILayout.BeginToggleGroup(new GUIContent("Show UI", "Do you want to show the UI in this scene?"), myTarget.showUI);
		// Decrease the Indent of the lines.
		EditorGUI.indentLevel--;
		// End the ToggleGroup.
		EditorGUILayout.EndToggleGroup();

		// apply
		serializedObject.ApplyModifiedProperties();
	}
}
using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Terrain_Pushable))]
public class Terrain_Pushable_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Terrain_Pushable myTarget = target as Terrain_Pushable;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// Sound Label.
		EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The audio clip.
		myTarget.soundClip = EditorGUILayout.ObjectField(new GUIContent("Sound Clip", "The sound clip to play while pushing."), 
		                                                 myTarget.soundClip, 
		                                                 typeof(AudioClip), 
		                                                 true) as AudioClip;
		// The minimum pitch.
		myTarget.minPitch = EditorGUILayout.FloatField(new GUIContent("Minimum Pitch", "The minimum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."), 
		                                               myTarget.minPitch);
		// The maximum pitch.
		myTarget.maxPitch = EditorGUILayout.FloatField(new GUIContent("Maximum Pitch", "The maximum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."), 
		                                               myTarget.maxPitch);
		// Decrease the Indent.
		EditorGUI.indentLevel--;


		// Movement Label.
		EditorGUILayout.LabelField("Movement", EditorStyles.boldLabel);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The movable toggle.
		myTarget.movable = EditorGUILayout.Toggle(new GUIContent("Is Movable", "Is this a movable GameObject right now."),
		                                          myTarget.movable);
		// the field.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("LayersThatMoveIt"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("timeToPush"));
		EditorGUILayout.PropertyField(serializedObject.FindProperty("moveSpeed"));
		// Decrease the Indent.
		EditorGUI.indentLevel--;


		// Raycast Label
		EditorGUILayout.LabelField("Raycast", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Do we want to show the raycast in the scene view
		myTarget.showRaycast = EditorGUILayout.Toggle(new GUIContent("Show Raycast", "Show the Raycast in the Scene View while the game is playing."),
		                                              myTarget.showRaycast);
		// The raycast point above the object.
		myTarget.up = EditorGUILayout.ObjectField(new GUIContent("Up", "The Raycast distance above the GameObject."),
		                                          myTarget.up,
		                                          typeof(Transform),
		                                          true) as Transform;
		// The raycast point below the object
		myTarget.down = EditorGUILayout.ObjectField(new GUIContent("Down", "The Raycast distance below the GameObject."),
		                                          myTarget.down,
		                                          typeof(Transform),
		                                          true) as Transform;
		// The raycast point to the left of the object.
		myTarget.left = EditorGUILayout.ObjectField(new GUIContent("Left", "The Raycast distance left of the GameObject."),
		                                          myTarget.left,
		                                          typeof(Transform),
		                                          true) as Transform;
		// The raycast point to the right of the object.
		myTarget.right = EditorGUILayout.ObjectField(new GUIContent("Right", "The Raycast distance right of the GameObject."),
		                                            myTarget.right,
		                                            typeof(Transform),
		                                            true) as Transform;
		// Decrease the indent.
		EditorGUI.indentLevel--;

		// apply
		serializedObject.ApplyModifiedProperties();
	}
}
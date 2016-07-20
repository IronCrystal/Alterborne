using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Target_Teleport))]
public class Target_Teleport_Editor : Editor{

	public override void OnInspectorGUI(){

		// Grab the script.
		Target_Teleport myTarget = target as Target_Teleport;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// Tag Label.
		EditorGUILayout.LabelField("Tags", EditorStyles.boldLabel);
		// Increase the Indent the lines.
		EditorGUI.indentLevel++;
		// Specific tags that can use the teleport.
		myTarget.specificTagTeleport = EditorGUILayout.BeginToggleGroup(new GUIContent("Specific Tag(s) for Teleport", "Do we have specific tags touching for teleport.  IF not then all GameObjects will teleport"), myTarget.specificTagTeleport);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Array for the Target Tags.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("targetTags"), new GUIContent("Tag Name", "The GameObjects with these tags can teleport."), true);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the toggle group.
		EditorGUILayout.EndToggleGroup();


		// Sound Label.
		EditorGUILayout.LabelField("Sound", EditorStyles.boldLabel);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The audio clip.
		myTarget.soundClip = EditorGUILayout.ObjectField(new GUIContent("Sound Clip", "The sound clip to play when teleporting."), myTarget.soundClip, typeof(AudioClip), false) as AudioClip;
		// The minimum pitch.
		myTarget.minPitch = EditorGUILayout.FloatField(new GUIContent("Minimum Pitch", "The minimum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."), myTarget.minPitch);
		// The maximum pitch.
		myTarget.maxPitch = EditorGUILayout.FloatField(new GUIContent("Maximum Pitch", "The maximum pitch this sound can be played at.  A random number between the minPitch and a maxPitch will be chosen."), myTarget.maxPitch);
		// Decrease the Indent.
		EditorGUI.indentLevel--;


		// Animation Label.
		EditorGUILayout.LabelField("Animation", EditorStyles.boldLabel);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The start animation.
		myTarget.teleportStartAnimation = EditorGUILayout.ObjectField(new GUIContent("Start Animation", "Start location teleport animation."), myTarget.teleportStartAnimation, typeof(GameObject), false) as GameObject;
		// The end animation.
		myTarget.teleportEndAnimation = EditorGUILayout.ObjectField(new GUIContent("End Animation", "End location teleport animation."), myTarget.teleportEndAnimation, typeof(GameObject), false) as GameObject;
		// Decrease the Indent.
		EditorGUI.indentLevel--;


		// Movement Label.
		EditorGUILayout.LabelField("Teleport", EditorStyles.boldLabel);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// The new location to be teleported to.
		myTarget.newLocation = EditorGUILayout.ObjectField(new GUIContent("Teleport Location", "The location to be teleported."), myTarget.newLocation, typeof(Transform), false) as Transform;
		// Decrease the Indent.
		EditorGUI.indentLevel--;

		// Apply
		serializedObject.ApplyModifiedProperties();
	}
}
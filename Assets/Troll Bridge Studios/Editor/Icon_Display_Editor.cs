using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Icon_Display))]
public class Icon_Display_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Icon_Display myTarget = target as Icon_Display;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// The Dialogue title.
		EditorGUILayout.LabelField("Icon", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the Icon GameObject.
		myTarget.Icon = EditorGUILayout.ObjectField(new GUIContent("Icon", "The GameObject to appear as the icon."), myTarget.Icon, typeof(GameObject), true) as GameObject;
		// Set the distance above the gameobject where the icon is placed.
		myTarget.SpaceForIcon = EditorGUILayout.FloatField(new GUIContent("Space For Icon", "The distance above the GameObject for the icon's location."), myTarget.SpaceForIcon);
		// Decrease the indent.
		EditorGUI.indentLevel--;


		EditorGUILayout.Space();


		// The Icon Movement Title.
		EditorGUILayout.LabelField("Icon Movements", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the toggle group for a bouncing icon.
		myTarget.BounceIcon = EditorGUILayout.BeginToggleGroup(new GUIContent("Bounce Icon", "Set to 'true' if you want the icon's movement to bounce."), myTarget.BounceIcon);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the bounce distance for the icon.
		myTarget.BounceDistance = EditorGUILayout.FloatField(new GUIContent("Bounce Distance", "The distance the icon moves before it moves back to its original location."), myTarget.BounceDistance);
		// Set the Bounce time for the icon.
		myTarget.BounceTime =  EditorGUILayout.FloatField(new GUIContent("Bounce Time", "The time it takes for the icon to move a distance of 'Bounce Distance'"), myTarget.BounceTime);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();


		EditorGUILayout.Space();


		// Set the toggle group for a pulsing icon.
		myTarget.PulseIcon = EditorGUILayout.BeginToggleGroup(new GUIContent("Pulse Icon", "Set to 'true' if you want the icon's movment to pulse."), myTarget.PulseIcon);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the pulse intensity.
		myTarget.PulseIntensity = EditorGUILayout.FloatField(new GUIContent("Pulse Intensity", "How intense the pulse should be.  \nIF set above 1 the pulse will go big-small-big, \nIF set under 1 the pulse will go small-big-small."), myTarget.PulseIntensity);
		// Set the pulse time for the icon.
		myTarget.PulseTime = EditorGUILayout.FloatField(new GUIContent("Pulse Time", "The time it take for the icon to pulse from small-big or big-small."), myTarget.PulseTime);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();


		EditorGUILayout.Space();


		// Set the toggle group for the spinning icon.
		myTarget.SpinIcon = EditorGUILayout.BeginToggleGroup(new GUIContent("Spin Icon", "Set to 'true' if you want the icon's movement to spin."), myTarget.SpinIcon);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Set the X rotation speed.
		myTarget.SpinXSpeed = EditorGUILayout.FloatField(new GUIContent("X Spin Speed", "The speed the gameobject rotates on its X."), myTarget.SpinXSpeed);
		// Set the y rotation speed.
		myTarget.SpinYSpeed = EditorGUILayout.FloatField(new GUIContent("Y Spin Speed", "The speed the gameobject rotates on its Y"), myTarget.SpinYSpeed);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();

		// Decrease the Indent.
		EditorGUI.indentLevel--;

		// Apply.
		serializedObject.ApplyModifiedProperties();
	}
}
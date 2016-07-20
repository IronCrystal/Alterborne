using UnityEngine;
using System.Collections;
using UnityEditor;

[CanEditMultipleObjects]
[CustomEditor(typeof(Action_Key_Dialogue))]
public class Action_Key_Dialogue_Editor : Editor {

	public override void OnInspectorGUI(){
		// Grab the script.
		Action_Key_Dialogue myTarget = target as Action_Key_Dialogue;
		// Set the indentLevel to 0 as default (no indent).
		EditorGUI.indentLevel = 0;
		// Update
		serializedObject.Update();

		// Scene View title.
		EditorGUILayout.LabelField("Scene View", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Get the togglegroup used to display the Collider2D area in the Scene View.
		myTarget.showAreaInScene = EditorGUILayout.BeginToggleGroup(new GUIContent("Show Area In Scene", "Set to 'true' if you want to see the area that the Player can interact with in the scene view."), myTarget.showAreaInScene);
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Get and Set the Collider2D used for the Interaction range.
		myTarget.rangeCollider = EditorGUILayout.ObjectField(new GUIContent("Area Collider", "The Collider2D that represents the range for the Interaction to happen."), myTarget.rangeCollider, typeof(Collider2D), true) as Collider2D;
		// Get and Set the color used to display the Collider2D area in the Scene View.
		myTarget.areaColor = EditorGUILayout.ColorField(new GUIContent("Area Color", "Set the color of the Collider2D that is being shown in your scene view."), myTarget.areaColor);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		// Decrease the indent.
		EditorGUI.indentLevel--;
		
		
		EditorGUILayout.Space();
		
		
		// The Dialogue title.
		EditorGUILayout.LabelField("Dialogue", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Get and Set the Destroy Area Dialogues Boolean.
		myTarget.destroyAreaDialogues = EditorGUILayout.Toggle(new GUIContent("Prevent Area Dialogues", "Set to 'true' if you want all Area Dialogues that are currently showing and Area Dialogues that could show during this dialogue to not be shown."), myTarget.destroyAreaDialogues);
		// Get and Set the Freeze Player boolean.
		myTarget.freezePlayer = EditorGUILayout.Toggle(new GUIContent("Freeze Player", "Set to 'true' if you want the player to not be able to move while the dialogue is up and running."), myTarget.freezePlayer);
		// Get and Set the Dialogue Box GameObject.
		myTarget.dialogueBox = EditorGUILayout.ObjectField(new GUIContent("Dialogue UI", "The Dialogue UI GameObject that will be displayed."), myTarget.dialogueBox, typeof(GameObject), true) as GameObject;
		// Get and Set the dialogue background color.
		myTarget.dialogueColor = EditorGUILayout.ColorField(new GUIContent("Dialogue Color", "The color alteration for the 'Dialogue UI'.  Leaving this color 'white' will keep the same look for your 'Dialogue UI' GameObject."), myTarget.dialogueColor);
		// Get and Set the spaceForDialogue distance.
		myTarget.spaceForDialogue = EditorGUILayout.FloatField(new GUIContent("Dialogue Space", "The distance above the GameObject for the dialogue's location."), myTarget.spaceForDialogue);
		// Decrease the indent.
		EditorGUI.indentLevel--;
		
		
		EditorGUILayout.Space();
		
		// The Dialogue Transition title.
		EditorGUILayout.LabelField("Dialogue Transition", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Get and Set the multiple transition boolean.
		myTarget.multipleTransitions = EditorGUILayout.Toggle(new GUIContent("Multiple Transitions", "Set to 'true' if you want dialogue UI transitions to happen after each dialogue string in the 'Dialogue' array."), myTarget.multipleTransitions);

		// Get and Set the instant dialogue transition boolean.
		myTarget.isInstantDialogue = EditorGUILayout.BeginToggleGroup(new GUIContent("Instant Transition", "Set to 'true' if you want the dialogue UI transition to appear/disappear instantly."), (!myTarget.isFadeDialogue && !myTarget.isGrowShrinkDialogue));
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		
		// Get and Set the fade dialogue transition boolean.
		myTarget.isFadeDialogue = EditorGUILayout.BeginToggleGroup(new GUIContent("Fade Transition", "Set to 'true' if you want the dialogue UI transition to fade."), (!myTarget.isInstantDialogue && !myTarget.isGrowShrinkDialogue) && (myTarget.isFadeDialogue && !myTarget.isInstantDialogue && !myTarget.isGrowShrinkDialogue));
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Get and Set the Fade Time.
		myTarget.fadeTime = EditorGUILayout.Slider(new GUIContent("Fade Time", "The fade time for when a dialogue box fades in and fades out."), Mathf.Round(myTarget.fadeTime * 100.0f) / 100f, 0.5f, 2.0f);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		
		// Get and Set the Grow/Shrink dialogue transition boolean.
		myTarget.isGrowShrinkDialogue = EditorGUILayout.BeginToggleGroup(new GUIContent("Grow/Shrink Transition", "Set to 'true' if you want the dialogue UI transition to grow and shrink."), (!myTarget.isInstantDialogue && !myTarget.isFadeDialogue) && (myTarget.isGrowShrinkDialogue && !myTarget.isFadeDialogue && !myTarget.isInstantDialogue));
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Get and Set the Grow/Shrink Time.
		myTarget.growShrinkTime = EditorGUILayout.Slider(new GUIContent("Grow/Shrink Time", "The grow/shrink time for when a dialogue box grows in and shrinks out."), Mathf.Round(myTarget.growShrinkTime * 100.0f) / 100f, 0.1f, 1.0f);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		
		
		EditorGUILayout.Space();
		
		
		// The Dialogue Text title.
		EditorGUILayout.LabelField("Dialogue Text", EditorStyles.boldLabel);
		// Increase the indent.
		EditorGUI.indentLevel++;
		// Get and Set the dialogue text color.
		myTarget.dialogueTextColor = EditorGUILayout.ColorField(new GUIContent("Text Color", "The color of the dialogue text."), myTarget.dialogueTextColor);
		
		// Get and Set the instant text boolean.
		myTarget.instantText = EditorGUILayout.BeginToggleGroup(new GUIContent("Instant Text", "Set to 'true' if you want the text transition to appear/disappear instantly."), (!myTarget.fadeText && !myTarget.typedText));
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		
		// Get and Set the fade text boolean.
		myTarget.fadeText = EditorGUILayout.BeginToggleGroup(new GUIContent("Faded Text", "Set to 'true' if you want the text transition to be faded in and out."), (!myTarget.typedText && !myTarget.instantText) && (myTarget.fadeText && !myTarget.instantText && !myTarget.typedText));
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Get and Set the text fade time.
		myTarget.textFadeTime = EditorGUILayout.Slider(new GUIContent("Fade Time", "The time at which the text is faded in and out."), (Mathf.Round(myTarget.textFadeTime * 10.0f) / 10f), 0.1f, 4.0f);
		// Decrease the Indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		
		// Get and Set the typed text boolean.
		myTarget.typedText = EditorGUILayout.BeginToggleGroup(new GUIContent("Typed Text", "Set to 'true' if you want the text transition to be typed out."), (!myTarget.fadeText && !myTarget.instantText) && (!myTarget.fadeText && !myTarget.instantText && myTarget.typedText));
		// Increase the Indent.
		EditorGUI.indentLevel++;
		// Get and Set the typed text pause time.
		myTarget.dialogueTextPause = EditorGUILayout.Slider(new GUIContent("Pause Time", "The time it takes for the next letter to be displayed.  Increasing this number slows the typing speed of the dialogue text while decreasing this number speeds up the typing speed of the dialogue."), Mathf.Round(myTarget.dialogueTextPause * 100.0f) / 100f, 0.01f, 0.5f);
		// Get and Set the type sound.
		myTarget.typeSound = EditorGUILayout.ObjectField(new GUIContent("Type Sound", "(Optional) The sound that plays when each character is typed in the dialogue."), myTarget.typeSound, typeof(AudioClip), true) as AudioClip;
		// Decrease the indent.
		EditorGUI.indentLevel--;
		// End the Toggle Group.
		EditorGUILayout.EndToggleGroup();
		// Get and Set the dialogue text.
		EditorGUILayout.PropertyField(serializedObject.FindProperty("dialogue"), true);
		// Decrease the indent.
		EditorGUI.indentLevel--;
		
		
		EditorGUILayout.Space();
		
		
		// Apply.
		serializedObject.ApplyModifiedProperties();
	}
}
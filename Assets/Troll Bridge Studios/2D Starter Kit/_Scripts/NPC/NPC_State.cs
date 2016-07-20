using UnityEngine;
using System.Collections;

public class NPC_State {

	// Can the player attack with melee.
	public bool CanNPCMove{get;set;}
	// The GameObject that is being focuses on by this GameObject.
	public GameObject FocusedObject{get;set;}
	// Is there an Area Dialogue.
	public bool HasAreaDialogue{get;set;}
	// Is the Area Dialogue currently running.
	public bool IsAreaDialogueRunning{get;set;}
	// The Area Dialogue.
	public Area_Dialogue AreaDialogue{get;set;}
	// Is there an Action Key Dialogue.
	public bool HasActionKeyDialogue{get;set;}
	// Is there a Action Key Dialogue currently running.
	public bool isActionKeyDialogueRunning{get;set;}
	// The Action Key Dialogue.
	public Action_Key_Dialogue ActionKeyDialogue{get;set;}
	// The Animation.
	public Animator NPCAnimator{get;set;}
	// Is there a Four Direction Animation.
	public bool FourDirAnim{get;set;}
	// Is there a Eight Direction Animation.
	public bool EightDirAnim{get;set;}


	public void Default(){
		CanNPCMove = true;
		HasAreaDialogue = false;
		IsAreaDialogueRunning = false;
		AreaDialogue = null;
		HasActionKeyDialogue = false;
		isActionKeyDialogueRunning = false;
		ActionKeyDialogue = null;
		NPCAnimator = null;
		FourDirAnim = false;
		EightDirAnim = false;
	}
}
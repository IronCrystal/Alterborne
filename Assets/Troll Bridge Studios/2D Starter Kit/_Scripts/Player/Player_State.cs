using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// These are states your player can be in.
public class Player_State {
	
	// Can the player move.
	public bool CanPlayerMove{get;set;}
	// The alter speed of the player.
	public float PlayerAlterSpeed{get;set;}
	// The invert movement for the X.
	public int PlayerInvertX{get;set;}
	// The invert movement for the Y.
	public int PlayerInvertY{get;set;}
	// Can the player attack with melee.
	public bool CanMeleeAttack{get;set;}
	// Can the player attack with a ranged weapon.
	public bool CanRangeAttack{get;set;}
	// Can the player attack with a spell.
	public bool CanSpellAttack{get;set;}
	// Is the player engaged in an Action Key Dialogue.
	public bool IsActionKeyDialogued{get;set;}
	// The Action Key Dialogue we are currently engaged in due to it being the closest.
	public Action_Key_Dialogue ClosestAKD;
	// The List of Area Dialogues currently inside.
	public List<Area_Dialogue> ListOfAreaDialogues;
	// The List of Action Key Dialogues currently inside.
	public List<Action_Key_Dialogue> ListOfActionKeyDialogues;
	// The Animation.
	public Animator PlayerAnimation;
	// Is there a Four Direction Animation.
	public bool FourDirAnim;
	// Is there a Eight Direction Animation.
	public bool EightDirAnim;


	public void Default(){
		// Set the default values you want your player to have here.
		CanPlayerMove = true;
		// Set the alter speed to dictate how fast the player will move.
		// 1 is normal speed, 0 will make the player not move and 2 will 
		// make the player move twice as fast as an example
		PlayerAlterSpeed = 1f;
		// Set invert to 1 for normal movement and -1 for inverted movement.
		PlayerInvertX = 1;
		// Set invert to 1 for normal movement and -1 for inverted movement.
		PlayerInvertY = 1;
		CanMeleeAttack = true;
		CanRangeAttack = true;
		CanSpellAttack = true;
		IsActionKeyDialogued = false;
		ClosestAKD = null;
		ListOfAreaDialogues = new List<Area_Dialogue>();
		ListOfActionKeyDialogues = new List<Action_Key_Dialogue>();
		PlayerAnimation = null;
		FourDirAnim = false;
		EightDirAnim = false;
	}
}
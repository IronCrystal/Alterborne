using UnityEngine;
using System.Collections;

public class NPC_Manager : MonoBehaviour {

	// The states for the Player.
	public NPC_State NPCState{get; private set;}
	private Vector2 prevLocation = Vector2.zero;
	private Transform _transform;
	private Animator _npcAnimator;

	void Awake () {
		// Grab the transform for more efficiency.
		_transform = gameObject.transform;
		// Create the Player State.
		NPCState = new NPC_State();
		// Set default values.
		NPCState.Default();
		// Assign the NPCs Animator.
		NPCState.NPCAnimator = GetComponent<Animator>();
		_npcAnimator = NPCState.NPCAnimator;
	}

	void Start(){
		// IF the NPC has an Animator.
		if(_npcAnimator != null){
			// IF the Animator is a Four Direction Animation,
			// ELSE IF the Animator is a Eight Direction Animation,
			if(_npcAnimator.GetLayerName(0) == "Four Base"){
				// Set true that we have a Four Direction Animation.
				NPCState.FourDirAnim = true;
				// Set false that we have a Eight Direction Animation.
				NPCState.EightDirAnim = false;
			}else if(_npcAnimator.GetLayerName(0) == "Eight Base"){
				// Set false since we dont have a Four Direction Animation.
				NPCState.FourDirAnim = false;
				// Set true since we dont have a Eight Direction Animation.
				NPCState.EightDirAnim = true;
			}
		}
	}

	void Update(){
		// IF the NPC can move.
		if(NPCState.CanNPCMove){
			// Handle NPC movement.
			NPCMovement();
		}
		// IF there is an Action Key Dialogue currently running.
		if(NPCState.isActionKeyDialogueRunning){
			// Handle the direction the NPC is looking.
			NPCLookDirection();
		}
	}
	
	void PlayAnimation(float hor, float vert){
		// IF the user has an animation set and ready to go.
		if(_npcAnimator != null){
			// IF the NPC has a Four Direction Animation,
			// ELSE IF the NPC has a Eight Direction Animation.
			if(NPCState.FourDirAnim){
				// Play animations.
				Helper_Manager.instance.FourDirectionAnimation(hor, vert, _npcAnimator);
			}else if(NPCState.EightDirAnim){
				// Play animation.
				Helper_Manager.instance.EightDirectionAnimation(hor, vert, _npcAnimator);
			}
		}
	}

	private void NPCMovement(){
		// Adjust for the current location.
		Vector2 curLocation = ((Vector2)_transform.position - prevLocation) / Time.deltaTime;
		// Play the animation.
		PlayAnimation(curLocation.x, curLocation.y);
		// Set where we were.
		prevLocation = _transform.position;
	}

	private void NPCLookDirection(){
		// Store the focused objects Transform.
		Transform focTransform = NPCState.FocusedObject.transform;
		// IF we have a Four Direction Animation for this gameobject,
		// ELSE IF we have a Eight Direction Animation for this gameobject.
		if(NPCState.FourDirAnim){
			Helper_Manager.instance.FourDirectionAnimation(focTransform.position.x - _transform.position.x, 
			                                               focTransform.position.y - _transform.position.y,
			                                               false,
			                                               _npcAnimator);
		}else if(NPCState.EightDirAnim){
			Helper_Manager.instance.EightDirectionAnimation(focTransform.position.x - _transform.position.x, 
			                                                focTransform.position.y - _transform.position.y,
			                                                false,
			                                                _npcAnimator);
		}
	}
}
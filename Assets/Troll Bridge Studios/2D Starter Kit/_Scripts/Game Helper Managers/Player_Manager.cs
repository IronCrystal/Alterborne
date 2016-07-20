using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Player_Manager : MonoBehaviour {

	[Tooltip("KeyCode for interaction.")]
	public KeyCode interactionKey;

	// The states for the Player.
	public Player_State playerState{get; private set;}

	// Used for Singleton.
	public static Player_Manager instance = null;

	private float _dist;
	private Transform _transform;
	private Animator _playerAnimator;


	void Awake(){
		// Cache the transform.
		_transform = gameObject.transform;
		// Create the Player State.
		playerState = new Player_State();
		// Set the Default Player States.
		playerState.Default();
		// Assign the Animator Component
		playerState.PlayerAnimation = GetComponent<Animator>();
		_playerAnimator = playerState.PlayerAnimation;
		// Check for singleton.
		Singleton();
		// Don't destroy this GameObject on scene change.
		DontDestroyOnLoad(gameObject);
	}

	void Start(){
		// IF there is a animator on the Player.
		if(_playerAnimator != null){
			// IF the Animator is a Four Direction Animation,
			// ELSE IF the Animator is a Eight Direction Animation.
			if(_playerAnimator.GetLayerName(0) == "Four Base"){
				// Set true that we have a Four Direction Animation.
				playerState.FourDirAnim = true;
				// Set false that we have a Eight Direction Animation.
				playerState.EightDirAnim = false;
			}else if(_playerAnimator.GetLayerName(0) == "Eight Base"){
				// Set false since we dont have a Four Direction Animation.
				playerState.FourDirAnim = false;
				// Set true since we dont have a Eight Direction Animation.
				playerState.EightDirAnim = true;
			}
		}
	}

	void Update(){
		// IF we press up on the interactionKey.
		if(Input.GetKeyUp(interactionKey)){
			// Handle any dialogue interactions.
			DialogueInteraction();
		}
	}

	private void Singleton(){
		// IF Player_Manager instance does not exist.
		// ELSE IF Player_Manager instance is not null AND Player_Manager instance does not equal to this Player_Manager.
		if(instance == null){
			// Set it to this.
			instance = this;
		}else if(instance != this){
			// Destroy this GameObject.
			Destroy (gameObject);
		}
	}

	private void DialogueInteraction(){
		// IF we are inside a Action Key Dialogue area.
		if(playerState.ListOfActionKeyDialogues.Count > 0){
			// IF we are not already engaged in a dialogue.
			if(!playerState.IsActionKeyDialogued){
				// Grab the list of dialogue gameobjects that the player is currently inside.
				List<Action_Key_Dialogue> akd = playerState.ListOfActionKeyDialogues;
				_dist = -1f;
				for(int i = 0; i < akd.Count; i++){
					// See which one is the closest
					float dist = Vector2.Distance(_transform.position, akd[i].gameObject.transform.position);
					// IF this is the first time in here. Also this takes care of 1 Interactive NPC in the List.
					// ELSE IF we have more interactive npcs and we need to compare distance to see which is closest.
					if(_dist == -1f){
						// Set the shortest distance.
						_dist = dist;
						// Set the closest action key dialogue.
						playerState.ClosestAKD = akd[i];
					}else if(dist < _dist){
						// Set the shortest distance.
						_dist = dist;
						// Set the closest action key dialogue.
						playerState.ClosestAKD = akd[i];
					}
				}
				// We are now engaged in a dialogue.
				playerState.IsActionKeyDialogued = true;
				// Make the player look at the NPC it is interacting with.
				PlayerLookDirection();
			}
			// We now have our closest interactive npc so we create the dialogue.
			playerState.ClosestAKD.CreateDialogue();
		}
	}

	private void PlayerLookDirection(){
		// Store the focused objects Transform.
		Transform focTransform = playerState.ClosestAKD.transform;
		// IF we have a Four Direction Animation for this gameobject,
		// ELSE IF we have a Eight Direction Animation for this gameobject.
		if(playerState.FourDirAnim){
			Helper_Manager.instance.FourDirectionAnimation(focTransform.position.x - _transform.position.x, 
			                                               focTransform.position.y - _transform.position.y,
			                                               false,
			                                               _playerAnimator);
		}else if(playerState.EightDirAnim){
			Helper_Manager.instance.EightDirectionAnimation(focTransform.position.x - _transform.position.x, 
			                                                focTransform.position.y - _transform.position.y,
			                                                false,
			                                                _playerAnimator);
		}
	}
}
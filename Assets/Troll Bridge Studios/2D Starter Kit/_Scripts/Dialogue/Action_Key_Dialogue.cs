using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Action_Key_Dialogue : MonoBehaviour {

	/// Set to true if you want to see the area that the Player can interact with the NPC
	public bool showAreaInScene = false;
	/// The Collider2D that represents the range for the Interaction to happen.
	public Collider2D rangeCollider;
	/// Sets the color of the Collider2D
	public Color areaColor = Color.black;

	/// Set to 'true' if you want all Area Dialogues that are currently showing and Area Dialogues that could show during this dialogue to not be shown.
	public bool destroyAreaDialogues = false;
	/// Set to 'true' if you want the player to not be able to move while the dialogue is up and running.
	public bool freezePlayer = false;
	/// The Dialogue UI GameObject that will be displayed.
	public GameObject dialogueBox;
	/// The color alteration for the 'Dialogue UI'.  Leaving this color 'white' will keep the same look for your 'Dialogue UI' GameObject.
	public Color dialogueColor = Color.white;
	/// The distance above the GameObject for the dialogue's location.
	public float spaceForDialogue;
	
	/// Set to 'true' if you want dialogue UI transitions to happen after each dialogue string in the 'Dialogue' array.
	public bool multipleTransitions = true;
	/// Set to 'true' if you want the dialogue UI transition to appear/disappear instantly.
	public bool isInstantDialogue = true;
	/// Set to 'true' if you want the dialogue UI transition to fade.
	public bool isFadeDialogue;
	/// The fade time for when a dialogue box fades in and fades out.
	public float fadeTime = 0.25f;
	/// Set to 'true' if you want the dialogue UI transition to grow and shrink.
	public bool isGrowShrinkDialogue;
	/// The grow/shrink time for when a dialogue box grows in and shrinks out.
	public float growShrinkTime = 0.25f;
	
	/// The color of the dialogue text.
	public Color dialogueTextColor = Color.black;
	/// Set to 'true' if you want the text transition to to appear/disappear instantly.
	public bool instantText;
	/// Set to 'true' if you want the text transition to be faded in and out.
	public bool fadeText;
	/// The time it takes for the next letter to be displayed. 
	/// Increasing this number slows the typing speed of the dialogue text while decreasing this number speeds up the typing speed of the dialogue.
	public float dialogueTextPause = 0.1f;
	/// Set to 'true' if you want the text transition to be typed out.
	public bool typedText;
	/// The time at which the text is faded in and out.
	public float textFadeTime = 0.5f;
	/// The sound that plays when each character is typed in the dialogue.
	public AudioClip typeSound;
	[Tooltip("The text that is displayed in the Dialogue UI.")]
	[Multiline]
	public string[] dialogue;

	// Is the dialogue already created.
	private bool isCreated = false;
	// The index in the dialogue.
	private int dialogueIndex;
	// The dialogue component.
	private Dialogue dialogueComponent;
	// The Area Dialogue on this GameObject.
	private Area_Dialogue areaDialogueComponent;
	// The bools for dialogue transitions.
	private bool inTransition = false;
	// The NPC State.
	private NPC_Manager _npcManager;
	// The Player State.
	private Player_Manager _playerManager;
	// The Transform.
	private Transform _transform;
	// Get the parent of this gameobject.
	private GameObject _parent;


	void Awake(){
		// Cache the Transform of this GameObject.
		_transform = gameObject.transform;
		// Grab the parent of this gameobject.
		_parent = _transform.parent.gameObject;
		// IF there is a parent gameobject AND the parent does not have a NPC_Manager component.
		if(_parent != null && _parent.GetComponent<NPC_Manager>() == null){
			_parent.AddComponent<NPC_Manager>();
		}

		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
	}

	void Start(){
		// Get the NPC manager from the parent.
		_npcManager = _parent.GetComponent<NPC_Manager>();
		// Set that there is an Action Key Dialogue on here.
		_npcManager.NPCState.HasActionKeyDialogue = true;
		_npcManager.NPCState.ActionKeyDialogue = this;
		// Set the start of the dialogueIndex.
		dialogueIndex = 0;
	}

	void DebugCheck(){
		// IF user has the show area in scene 
		if(showAreaInScene && (rangeCollider == null)){
			Helper_Manager.instance.DebugErrorCheck(70, this.GetType(), gameObject);
		}
		// IF the user didnt set a dialogue box gameobject to be shown.
		if(dialogueBox == null){
			Helper_Manager.instance.DebugErrorCheck(71, this.GetType(), gameObject);
		}
		// IF the user didn't set any dialogue for the NPC.
		if(dialogue.Length == 0){
			Helper_Manager.instance.DebugErrorCheck(72, this.GetType(), gameObject);
		}
		// IF the user didn't set up the Action Key dialogue properly by not making this script
		// be on a child gameobject of the main NPC gameobject.
		if(_parent == null){
			Helper_Manager.instance.DebugErrorCheck(77, this.GetType(), gameObject);
		}
	}

	void OnDrawGizmos(){
		// This is used for Scene view.
		if(showAreaInScene && rangeCollider != null){
			// IF we have a CircleCollider2D,
			// ELSE IF we have a BoxCollider2D.
			if(rangeCollider.GetType() == typeof(CircleCollider2D)){
				// Display the Circle Collider on the scene view.
				SceneCircleCollider(rangeCollider.GetComponent<CircleCollider2D>(), areaColor);
			}else if(rangeCollider.GetType() == typeof(BoxCollider2D)){
				// Display the Box Collider on the scene view.
				SceneBoxCollider(rangeCollider.GetComponent<BoxCollider2D>(), areaColor);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		// Attempt to grab the Player_Manager script
		Player_Manager _player = coll.GetComponent<Player_Manager>();
		// IF the colliding object doesnt have the Player Manager script.
		if(_player == null){
			return;
		}
		// IF the colliding object's tag isn't Player.
		if(coll.tag != "Player"){
			return;
		}
		// Assign the Player Manager script.
		_playerManager = _player;
		// We add this Script to our player state list.
		_playerManager.playerState.ListOfActionKeyDialogues.Add(this);				
	}

	void OnTriggerExit2D(Collider2D coll){
		// Attempt to grab the Player_Manager script
		Player_Manager _player = coll.GetComponent<Player_Manager>();
		// IF the colliding object doesnt have the Player Manager script.
		if(_player == null){
			return;
		}
		// IF the colliding object's tag isn't Player.
		if(coll.tag != "Player"){
			return;
		}
		// We remove this script from our player state list.
		_playerManager.playerState.ListOfActionKeyDialogues.Remove(this);
		// IF the dialogue component isn't null.
		if(dialogueComponent != null){
			// Stop all the coroutines in the dialogueComponent.
			dialogueComponent.StopAllCoroutines();
			// Stop all coroutines on this script.
			StopAllCoroutines();
			// Finish off all actions while exiting the collider.
			StartCoroutine(ExitCollider());
		}
	}

	// Create the Dialogue box.
	public void CreateDialogue(){
		// IF we are not in a transition.
		if(!inTransition){
			// IF we do not have a dialogue box created.
			if(!isCreated){
				// IF we are in a area dialogue.
				if(_npcManager.NPCState.HasAreaDialogue){
					// IF the area dialogue gameobject parent is the same object as the action key dialogue gameobject parent.
					if(_npcManager.NPCState.AreaDialogue.transform.parent.gameObject == _transform.parent.gameObject){
						// Assign the areaDialogueComponent we are colliding with.
						areaDialogueComponent = _npcManager.NPCState.AreaDialogue;
						// We remove that dialogue as the focus is now this dialogue.
						areaDialogueComponent.ResetDialogue();
					}
				}
				// IF we want to freeze the player during this dialogue.
				if(freezePlayer){
					// Freeze the players movement.
					_playerManager.playerState.CanPlayerMove = false;
				}
				// Freeze the NPC while the dialogue is up.
				_npcManager.NPCState.CanNPCMove = false;
				// Set the IsActionKeyDialogueRunning for this GameObjects Manager.
				_npcManager.NPCState.isActionKeyDialogueRunning = true;
				// Set who the NPC is focusing on.
				_npcManager.NPCState.FocusedObject = _playerManager.gameObject;
				// Remove Area Dialogues.
				RemoveAreaDialogues();
				// Set-up the dialogue.
				SetupDialogue();
			}
			// Since we created a dialogue GameObject (or already have one created) it only means 
			// we go to the first/next part of the dialogue chat.
			StartCoroutine(GoToNextDialogue());
		}
	}

	// Go to the next part of the dialogue.
	private IEnumerator GoToNextDialogue(){
		// IF we are on the first message.
		// ELSE IF we are on the last message.
		// ELSE we are in the middle of the entire dialogue.
		if(dialogueIndex == 0){
			inTransition = true;
			// When a Dialogue UI is created we now choose which transition we want.
			yield return StartCoroutine(InitialTransition());
			// The transition is over.
			inTransition = false;
		}else if(dialogueIndex >= dialogue.Length){
			// The transition is starting.
			inTransition = true;
			// Start dialogue out transitions.
			yield return StartCoroutine(FinalTransition());
			// The transition is over.
			inTransition = false;
			// Reset the Dialogue.
			ResetDialogue();
			yield break;
		}else {
			// The transition is starting.
			inTransition = true;
			// Start dialogue out transitions.
			yield return StartCoroutine(DialogueOutTransition());
			// When a Dialogue UI is created we now choose which transition we want.
			yield return StartCoroutine(DialogueInTransition());
			// The transition is over.
			inTransition = false;
		}
		// Increase the dialogueIndex.
		dialogueIndex++;
	}

	private IEnumerator InitialTransition(){
		// The first transition.
		yield return StartCoroutine(DialogueIn());
	}

	private IEnumerator FinalTransition(){
		// The last transition.
		yield return StartCoroutine(DialogueOut());
	}

	private IEnumerator DialogueInTransition(){
		// IF we want multiple transitions.
		if(multipleTransitions){
			// Bring in the dialogue background.
			yield return StartCoroutine(DialogueIn());
		}else{
			// Bring in the dialogue text.
			yield return StartCoroutine(DialogueTextIn());
		}
	}
	
	private IEnumerator DialogueOutTransition(){
		// IF we want multiple transitions.
		if(multipleTransitions){
			// Take away the dialogue background.
			yield return StartCoroutine(DialogueOut());
		}else{
			// Take away the dialogue text.
			yield return StartCoroutine(DialogueTextOut());
		}
	}
	
	private IEnumerator DialogueIn(){
		// IF we want to fade in the dialogue.
		// ELSE IF we want to grow/shrink the dialogue.
		if(isFadeDialogue){
			// Fade in image.
			StartCoroutine(dialogueComponent.FadeInImage(fadeTime));
			// IF we have fading text as well.
			if(fadeText){
				dialogueComponent.SwitchText(dialogue[dialogueIndex]);
				// Fade in text.
				StartCoroutine(dialogueComponent.FadeInText(textFadeTime, dialogue[dialogueIndex]));
				// Wait for the longer time of the dialogue fade or the dialogue fade time.
				yield return new WaitForSeconds(Mathf.Max(fadeTime, textFadeTime));
				yield break;
			}
			yield return new WaitForSeconds(fadeTime);
		}else if(isGrowShrinkDialogue){
			// Grow the dialogue.
			yield return StartCoroutine(dialogueComponent.Grow(growShrinkTime, dialogue[dialogueIndex]));
		}else if(isInstantDialogue){
			// Instantly show the dialogue.
			dialogueComponent.SetInitialDialogueUIAlpha();
		}
		
		dialogueComponent.SwitchText(dialogue[dialogueIndex]);
		// IF we want to fade the text.
		if(fadeText){
			// Start the fade on the text.
			yield return StartCoroutine(dialogueComponent.FadeInText(textFadeTime, dialogue[dialogueIndex]));
		}else if(typedText){
			// Set the text alpha back to its initial.
			dialogueComponent.SetInitialDialogueTextAlpha();
			// Dont move forward until the typing of the text is finished.
			yield return StartCoroutine(dialogueComponent.TypeText(dialogueTextPause, dialogue[dialogueIndex], typeSound));
		}else if(instantText){
			// Set the text alpha back to its initial.
			dialogueComponent.SetInitialDialogueTextAlpha();
		}
	}

	private IEnumerator DialogueOut(){
		// IF we want the dialogue to fade.
		if(isFadeDialogue){
			// Fade out the dialogue.
			StartCoroutine(dialogueComponent.FadeOutImage(fadeTime));
			// We fade the text.
			yield return StartCoroutine(dialogueComponent.FadeOutText(fadeTime));
			yield break;
		}else if(isGrowShrinkDialogue){
			// Shrink the dialogue
			yield return StartCoroutine(dialogueComponent.Shrink(growShrinkTime));
		}else if(isInstantDialogue){
			// Set the dialogue alpha to 0f.
			dialogueComponent.SetDialogueUIAlpha(0f);
		}
		// Set the dialogue alpha to 0f.
		dialogueComponent.SetDialogueTextAlpha(0f);
	}

	private IEnumerator DialogueTextIn(){
		// Switch the text of the dialogue.
		dialogueComponent.SwitchText(dialogue[dialogueIndex]);
		// IF we want to fade the text.
		if(fadeText){
			// Fade in the text.
			yield return StartCoroutine(dialogueComponent.FadeInText(textFadeTime, dialogue[dialogueIndex]));
		}else if(typedText){
			// Dont move forward until the typing of the text is finished.
			dialogueComponent.SetInitialDialogueTextAlpha();
			// Type out the text.
			yield return StartCoroutine(dialogueComponent.TypeText(dialogueTextPause, dialogue[dialogueIndex], typeSound));
		}else if(instantText){
			// Set the dialogue text alpha back to its initial.
			dialogueComponent.SetInitialDialogueTextAlpha();
		}
	}
	
	private IEnumerator DialogueTextOut(){
		// IF we want the dialogue to fade.
		// ELSE IF we have typed texted or instant text.
		if(fadeText){
			// We fade the text.
			yield return StartCoroutine(dialogueComponent.FadeOutText(textFadeTime));
		}else if(typedText || instantText){
			// Set the dialogue text alpha to 0f.
			dialogueComponent.SetDialogueTextAlpha(0f);
		}
	}

	private IEnumerator ExitCollider(){
		// Start the final transition.
		yield return StartCoroutine(FinalTransition());
		// We are no longer in a transition.
		inTransition = false;
		// Reset the dialogue.
		ResetDialogue();
	}

	private void SetupDialogue(){
		// Create the Dialogue Box and set the position.
		GameObject dialogueGO = Instantiate(dialogueBox) as GameObject;
		// Set the dialogue parent.
		dialogueGO.transform.SetParent(_transform);
		// Based on manual positioning or the collider sets the location of the dialogue.
		dialogueGO.transform.position = new Vector2(_transform.position.x, _transform.position.y + spaceForDialogue);
		
		// Get the Dialogue Component.
		dialogueComponent = dialogueGO.GetComponent<Dialogue>();
		// Set the color for the dialogue box.
		dialogueComponent.SetDialogueUIColors(dialogueColor, dialogueTextColor);
		// Set the bool to show we have a created Dialogue.
		isCreated = true;
		// Based on the transition, Set the start variables.
		InitDialogue();
	}

	private void InitDialogue(){
		// IF we want the dialogue to fade.
		if(isFadeDialogue){
			// Set the dialogue background alpha to 0f.
			dialogueComponent.SetDialogueUIAlpha(0f);
		}else if(isGrowShrinkDialogue){
			// Set the dialogue scale to 0f.
			dialogueComponent.SetDialogueScale(0f, 0f);
		}
		
		// IF we want the text to fade.
		if(fadeText){
			// Set the dialogue text alpha to 0f.
			dialogueComponent.SetDialogueTextAlpha(0f);
		}
	}

	private void ResetDialogue(){
		// Reset the dialogueIndex.
		dialogueIndex = 0;
		// Set the bool to show we have destroyed the Dialogue.
		isCreated = false;
		// Since the dialogue is done this can move now.
		_npcManager.NPCState.CanNPCMove = true;
		// Set the IsActionKeyDialogueRunning for this GameObjects Manager.
		_npcManager.NPCState.isActionKeyDialogueRunning = false;
		// Set who the NPC is focusing on.
		_npcManager.NPCState.FocusedObject = null;
		// InteractiveDowntime the Area Dialogues we are currently engaged in.
		RestartAreaDialogues();
		// IF the dialogueComponent exist then destroy it.
		if(dialogueComponent != null){
			// Stop all the coroutines the dialogue component.
			dialogueComponent.StopAllCoroutines();
			// Destroy the dialogue.
			Destroy (dialogueComponent.gameObject);
		}
		// IF the players state closest action key dialogue is this gameobject.
		if(_playerManager.playerState.ClosestAKD == this){
			// Set the closest action key dialogue to null.
			_playerManager.playerState.ClosestAKD = null;
			// Set the bool to show if this is an action key dialogue to false.
			_playerManager.playerState.IsActionKeyDialogued = false;
		}
		// IF we have an Area Dialogue Component.
		if(areaDialogueComponent != null){
			// Reactivate the Area Dialogue Component.
			areaDialogueComponent.StartCoroutine(areaDialogueComponent.InteractiveDowntime());
			// Set to null for reset purposes.
			areaDialogueComponent = null;
		}
		// IF we have the player frozen during dialogue.
		if(freezePlayer){
			// Unfreeze the player.
			_playerManager.playerState.CanPlayerMove = true;
		}
	}

	private void RemoveAreaDialogues(){
		// IF we want to destroy all Area Dialogues while this Dialogue is up.
		if(destroyAreaDialogues){
			// Get the list of all the Area Dialogues we are currently engaged in.
			List<Area_Dialogue> ad = _playerManager.playerState.ListOfAreaDialogues;
			// Loop through them all and reset them with no InteractiveDowntime
			for(int i = 0; i < ad.Count; i++){
				ad[i].ResetDialogue();
			}
		}
	}

	private void RestartAreaDialogues(){
		// IF we want to restart all Area Dialogues while this Dialogue is ended.
		if(destroyAreaDialogues){
			// Get the list of all the Area Dialogues we are currently engaged in.
			List<Area_Dialogue> ad = _playerManager.playerState.ListOfAreaDialogues;
			// Loop through them all and InteractiveDowntime them.
			for(int i = 0; i < ad.Count; i++){
				ad[i].InteractiveDowntime();
			}
		}
	}

	// Used for displaying collider information on the Scene View.
	private void SceneCircleCollider(CircleCollider2D coll, Color areaColor){
		#if UNITY_EDITOR
		// Set the color.
		UnityEditor.Handles.color = areaColor;
		// Get the offset.
		Vector3 offset = coll.offset;
		// Get the position of the collider gameobject.
		Vector3 discCenter = coll.transform.position;
		// Scaling incase the gameobject has been scaled.
		float scale;
		// IF the x scale is larger than the y scale.
		if(transform.lossyScale.x > transform.lossyScale.y){
			// Make scale the size of the x.
			scale = transform.lossyScale.x;
		}else{
			// Make scale the size of the y.
			scale = transform.lossyScale.y;
		}
		// Draw the Disc on the Scene View.
		UnityEditor.Handles.DrawWireDisc(discCenter + offset, Vector3.back, coll.radius * scale);
		#endif
	}

	// Used for displaying collider information on the Scene View.
	private void SceneBoxCollider(BoxCollider2D coll, Color areaColor){
		// Set the color.
		Gizmos.color = areaColor;
		// Get the offset.
		Vector3 offset = coll.offset;
		// Get the position of the collider gameobject.
		Vector3 boxCenter = coll.transform.position;
		// Draw the Box on the Scene View.
		Gizmos.DrawWireCube(boxCenter + offset, new Vector2(coll.size.x * transform.lossyScale.x, coll.size.y * transform.lossyScale.y));
	}
}
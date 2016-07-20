using UnityEngine;
using System.Collections;

public class Area_Dialogue : MonoBehaviour {

	/// Set to 'true' if you want to see the area that the Player can interact with the NPC
	public bool showAreaInScene = false;
	/// The Collider2D that represents the range for the Interaction to happen.
	public Collider2D rangeCollider;
	/// Sets the color of the Collider2D
	public Color areaColor = Color.black;

	/// The Dialogue UI GameObject that will be displayed.
	public GameObject dialogueBox;
	/// The color alteration for the 'Dialogue UI'.  Leaving this color 'white' will keep the same look for your 'Dialogue UI' GameObject.
	public Color dialogueColor = Color.white;
	/// The distance above the GameObject for the dialogue's location.
	public float spaceForDialogue;
	/// This time dictates how long before the dialogue can be displayed again after it has been completed/destroyed.
	public float inactiveTime = 3;

	/// Set to 'true' if you want dialogue UI transitions to happen after each dialogue string in the 'Dialogue' array.
	public bool multipleTransitions = true;
	/// The time each dialogue is visible before transitioning to the next dialogue.
	public float chatDuration = 5;
	/// Set to 'true' if you want the dialogue UI transition to appear/disappear instantly.
	public bool isInstantDialogue = true;
	/// Set to 'true' if you want the dialogue UI transition to fade.
	public bool isFadeDialogue;
	/// The fade time for when a dialogue box fades in and fades out.
	public float fadeTime = 0.25f;
	/// Set to 'true' if you want the dialogue UI transition to grow and shrink.
	public bool isGrowShrinkDialogue;
	/// The grow/shrink time for when a dialogue box grows in and shrinks out.
	public float growShrinkTime;

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
	public float textFadeTime;
	/// The sound that plays when each character is typed in the dialogue.
	public AudioClip typeSound;
	[Tooltip("The text that is displayed in the Dialogue UI.")]
	[Multiline]
	public string[] dialogue;

	// The boolean to let us know we are inside of the Area.
	private bool isInside = false;
	// The boolean to let us know we have waited out the Inactive Time.
	private bool isActivatable = true;
	// The index in the dialogue.
	private int dialogueIndex;
	// The dialogue component.
	private Dialogue dialogueComponent;
	// Get the manager of this gameobject.
	private NPC_Manager _npcManager;
	// The Player State.
	private Player_Manager _playerManager;
	// The Transform.
	private Transform _transform;
	// Get the parent of this gameobject.
	private GameObject _parent;


	void Awake(){
		_transform = gameObject.transform;
		// Get this gameobjects parent gameobject
		_parent = _transform.parent.gameObject;
		// IF the parent is null AND the parent gameobject NPC_Manager does not exist.
		if(_parent != null && _parent.GetComponent<NPC_Manager>() == null){
			// Add the component.
			_parent.AddComponent<NPC_Manager>();
		}
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
	}

	void Start(){
		// Get the NPC manager from the parent.
		_npcManager = _parent.GetComponent<NPC_Manager>();
		// Set that there is an Action Key Dialogue on here.
		_npcManager.NPCState.HasAreaDialogue = true;
		_npcManager.NPCState.AreaDialogue = this;
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
		// IF the user didn't set up the Area Key dialogue properly by not making this script
		// be on a child gameobject of the main NPC gameobject.
		if(_parent == null){
			Helper_Manager.instance.DebugErrorCheck(78, this.GetType(), gameObject);
		}
	}

	void OnDrawGizmos(){
#if UNITY_EDITOR
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
#endif
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
		// We are inside.
		isInside = true;
		// Assign the Player Manager script.
		_playerManager = _player;
		// Since we have a Player Manager script we assign the state.
		_playerManager.playerState.ListOfAreaDialogues.Add(this);
		// IF we do not have a null closest Action Key Dialogue component AND the Action Key Dialogue variable 'destroyAreaDialogue' is set to 'true'.
		if(_playerManager.playerState.ClosestAKD != null && _playerManager.playerState.ClosestAKD.destroyAreaDialogues){
			// Do nothing.
		}else{
			// We begin the dialogue.
			CreateDialogue();
		}
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
		// We are no longer inside.
		isInside = false;
		// Since the dialogue is being destroyed we assign the state.
		_playerManager.playerState.ListOfAreaDialogues.Remove(this);
		// Reset the dialogueIndex.
		dialogueIndex = 0;
		if(dialogueComponent != null){
			dialogueComponent.StopAllCoroutines();
		}
		// IF the dialogue is activatable.
		if(isActivatable){
			// Stop the coroutines for resetting the dialogue.
			StopAllCoroutines();
			// We need to create some time before the NPC talks again.
			StartCoroutine(InteractiveDowntime());
		}
	}

	// Create the Dialogue box.
	private void CreateDialogue(){
		// IF we are inside the area.
		if(isInside && isActivatable){
			// IF the dialogueComponent is null.
			if(dialogueComponent == null){
				// Set up how the dialogue is initialized.
				SetupDialogue();
			}
			// Since we created a dialogue GameObject we go to the next part of the dialogue chat.
			StartCoroutine(GoToNextDialogue());
		}
	}

	// Go to the next part of the dialogue.
	private IEnumerator GoToNextDialogue(){
		// IF we are on the first message.
		// ELSE IF we are on the last message.
		// ELSE we are in the middle of the entire dialogue.
		if(dialogueIndex == 0){
			// When a Dialogue UI is created we now choose which transition we want.
			yield return StartCoroutine(InitialTransition());
		}else if(dialogueIndex >= dialogue.Length){
			// Reset the dialogueIndex.
			dialogueIndex = 0;
			// Start dialogue out transitions.
			yield return StartCoroutine(FinalTransition());
			// We need to create some time before the NPC talks again.
			StartCoroutine(InteractiveDowntime());
			yield break;
		}else {
			// Start dialogue out transitions.
			yield return StartCoroutine(DialogueOutTransition());
			// When a Dialogue UI is created we now choose which transition we want.
			yield return StartCoroutine(DialogueInTransition());
		}
		// Increase the dialogueIndex.
		dialogueIndex++;
		// Coroutine the next AutoPlayDialogue.
		StartCoroutine(AutoPlayDialogue());
	}

	// Dialogue transitions are done automatically.
	private IEnumerator AutoPlayDialogue(){
		// Suspend the coroutine for 'chatDuration' seconds.
		yield return new WaitForSeconds (chatDuration);
		// Coroutine the next part of the Dialogue.
		StartCoroutine(GoToNextDialogue());
	}

	// How long before the NPC can be interacted with again.
	public IEnumerator InteractiveDowntime(){
		// We are not activatable.
		isActivatable = false;
		// IF we dont have a null dialogueComponent.
		if(dialogueComponent != null){
			// Finish up the last transition.
			yield return StartCoroutine(FinalTransition());
			// Destroy the dialogue.
			Destroy(dialogueComponent.gameObject);
		}
		// Suspend the coroutine for 'inactiveTime' seconds.
		yield return new WaitForSeconds(inactiveTime);
		// We are now activatable again.
		isActivatable = true;
		// Go back into creating a dialogue.
		CreateDialogue();
	}

	private IEnumerator InitialTransition(){
		// The first transition.
		yield return StartCoroutine(DialogueIn());
	}

	private IEnumerator FinalTransition(){
		// The last transition.
		yield return StartCoroutine(DialogueOut());
	}

	// What type of dialogue transition do we want.
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
		// ELSE IF we want to grow the dialogue,
		// ELSE IF we want the dialogue to appear instantly.
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
		// IF we want to fade the text,
		// ELSE IF we want the text to be typed out,
		// ELSE IF we want the text to be instant.
		if(fadeText){
			// Start the fade on the text.
			yield return StartCoroutine(dialogueComponent.FadeInText(textFadeTime, dialogue[dialogueIndex]));
		}else if(typedText){
			dialogueComponent.SetInitialDialogueTextAlpha();
			// Dont move forward until the typing of the text is finished.
			yield return StartCoroutine(dialogueComponent.TypeText(dialogueTextPause, dialogue[dialogueIndex], typeSound));
		}else if(instantText){
			// Set the dialogue text alpha to its initial.
			dialogueComponent.SetInitialDialogueTextAlpha();
		}
	}	

	private IEnumerator DialogueOut(){
		// IF we want the dialogue to fade,
		// ELSE IF we want to shrink the dialogue,
		// ELSE IF we want the dialogue to disappear instantly.
		if(isFadeDialogue){
			// Fade out the dialogue.
			StartCoroutine(dialogueComponent.FadeOutImage(fadeTime));
			// We fade the text.
			yield return StartCoroutine(dialogueComponent.FadeOutText(fadeTime));
			yield break;
		}else if(isGrowShrinkDialogue){
			// Shrink the dialogue.
			yield return StartCoroutine(dialogueComponent.Shrink(growShrinkTime));
		}else if(isInstantDialogue){
			// Set the dialogue background alpha to 0f.
			dialogueComponent.SetDialogueUIAlpha(0f);
		}
		// Set the dialogue text alpha to 0f.
		dialogueComponent.SetDialogueTextAlpha(0f);
	}

	private IEnumerator DialogueTextIn(){
		// Switch the dialogue text.
		dialogueComponent.SwitchText(dialogue[dialogueIndex]);
		// IF we want to fade the text,
		// ELSE IF we want the text to be typed out,
		// ELSE IF we want the text to appear instantly.
		if(fadeText){
			yield return StartCoroutine(dialogueComponent.FadeInText(textFadeTime, dialogue[dialogueIndex]));
		}else if(typedText){
			// Dont move forward until the typing of the text is finished.
			dialogueComponent.SetInitialDialogueTextAlpha();
			yield return StartCoroutine(dialogueComponent.TypeText(dialogueTextPause, dialogue[dialogueIndex], typeSound));
		}else if(instantText){
			dialogueComponent.SetInitialDialogueTextAlpha();
		}
	}

	private IEnumerator DialogueTextOut(){
		// IF we want the dialogue to fade,
		// ELSE IF we have the typed text OR instant text to transition out.
		if(fadeText){
			// We fade the text.
			yield return StartCoroutine(dialogueComponent.FadeOutText(textFadeTime));
		}else if(typedText || instantText){
			// Set the dialogue text alpha to 0f.
			dialogueComponent.SetDialogueTextAlpha(0f);
		}
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

		// Based on the transition, Set the start variables.
		InitDialogue();
	}

	private void InitDialogue(){
		// IF we have a fading dialogue,
		// ELSE IF we have a grow/shrinking dialogue.
		if(isFadeDialogue){
			dialogueComponent.SetDialogueUIAlpha(0f);
		}else if(isGrowShrinkDialogue){
			dialogueComponent.SetDialogueScale(0f, 0f);
		}

		// IF we want the text to fade.
		if(fadeText){
			dialogueComponent.SetDialogueTextAlpha(0f);
		}
	}

	public void ResetDialogue(){
		// Reset the dialogueIndex.
		dialogueIndex = 0;
		// Stop the coroutines for resetting the dialogue.
		StopAllCoroutines();
		// Start a coroutine to shut down the dialogue.
		StartCoroutine(ShutDownDialogue());
	}

	private IEnumerator ShutDownDialogue(){
		// IF the dailogue component exists
		if(dialogueComponent != null){
			// Destroy the dialogue gameobject.
			dialogueComponent.StopAllCoroutines();
			// Start the final transition.
			yield return StartCoroutine(FinalTransition());
			// Destroy the dialogue component
			Destroy (dialogueComponent.gameObject);
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
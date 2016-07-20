using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Collider2D))]
public class Destroy_By_Colliding_Manually : MonoBehaviour {

	// Set to 'true' if you want to set the gameobjects inactive rather than destroying.
	public bool setInactive;

	// Action when we enter collide.
	[Tooltip("Active a Destroy when we first enter collision.")]
	public bool onEnter;	
	// The onEnter tags that can trigger the deletion.
	[Tooltip("The tags that can only activate the Destroy.")]
	public string[] onEnterActivateTags;
	// The manual onEnter GameObjects used to be destroyed.
	[Tooltip("The GameObjects that are manually placed in here will be destroyed.")]
	public GameObject[] onEnterGameObjectsDestroy;

	// Action when we exit collision.
	[Tooltip("Active a Destroy when we exit collision?")]
	public bool onExit;
	// The onExit tags that can trigger the deletion.
	[Tooltip("The tags that can only activate the Destroy.")]
	public string[] onExitActivateTags;
	// The manual onExit GameObjects used to be destroyed.
	[Tooltip("The GameObjects that are manually placed in here will be destroyed.")]
	public GameObject[] onExitGameObjectsDestroy;


	void Awake(){
		DebugCheck();
	}

	// Enter Physical Collision.
	void OnCollisionEnter2D(Collision2D coll){
		// IF we allow it.
		if(onEnter){
			DeleteGameObjectsOnEnter(coll.gameObject);
		}
	}

	// Exit Physical Collision.
	void OnCollisionExit2D(Collision2D coll){
		// IF we allow it.
		if(onExit){
			DeleteGameObjectsOnExit(coll.gameObject);
		}
	}

	// Enter Trigger Collision.
	void OnTriggerEnter2D(Collider2D coll){
		// IF we allow it.
		if(onEnter){
			DeleteGameObjectsOnEnter(coll.gameObject);
		}
	}

	// Exit Trigger Collision.
	void OnTriggerExit2D(Collider2D coll){
		// IF we allow it.
		if(onExit){
			DeleteGameObjectsOnExit(coll.gameObject);
		}
	}

	void DebugCheck(){
		// IF neither of the onEnter or onExit are TRUE then this script wont do anything, 
		// so notify the user that.
		// ELSE One of them is true.
		if(!onEnter && !onExit){
			Helper_Manager.instance.DebugErrorCheck(29, this.GetType(), gameObject);
		}else{
			// IF we are using onEnter.
			if(onEnter){
				// IF there is not any onEnterActivation tags.
				if(onEnterActivateTags.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(20, this.GetType(), gameObject);
				}
				// IF there is not any tags set to onEnterDestroyTags.
				if(onEnterGameObjectsDestroy.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(26, this.GetType(), gameObject);
				}
			}
			// IF we are using onExit.
			if(onExit){
				// IF there is not any onExitActivation tags.
				if(onExitActivateTags.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(21, this.GetType(), gameObject);
				}
				// IF there is not any parents set to OnExitParents.
				if(onExitGameObjectsDestroy.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(27, this.GetType(), gameObject);
				}
			}
		}
	}

	void DeleteGameObjectsOnEnter(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < onEnterActivateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == onEnterActivateTags[i]){
				// IF we want to set them inactive,
				// ELSE we want to destroy them.
				if(setInactive){
					// Time to set inactive.
					Helper_Manager.instance.SetActiveGameObjects(onEnterGameObjectsDestroy, false);
				}else{
					// Time to destroy.
					Helper_Manager.instance.DestroyGameObjects(onEnterGameObjectsDestroy);
				}
				// Make sure to deactivate the script.
				DeActivateOnEnter();
				// Once we have 1 match we are done.
				return;
			}
		}
	}

	void DeleteGameObjectsOnExit(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < onExitActivateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == onExitActivateTags[i]){
				// IF we want to set them inactive,
				// ELSE we want to destroy them.
				if(setInactive){
					// Time to set inactive.
					Helper_Manager.instance.SetActiveGameObjects(onExitGameObjectsDestroy, false);
				}else{
					// Time to destroy.
					Helper_Manager.instance.DestroyGameObjects(onExitGameObjectsDestroy);
				}
				// Make sure to deactivate the script.
				DeActivateOnExit();
				// Once we have 1 match we are done.
				return;
			}
		}
	}
	
	void DeActivateOnEnter(){
		// Turn off.
		onEnter = false;
	}
	
	void DeActivateOnExit(){
		// Turn off.
		onExit = false;
	}
}
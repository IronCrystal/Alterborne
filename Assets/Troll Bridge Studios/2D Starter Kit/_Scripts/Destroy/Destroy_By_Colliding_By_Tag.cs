using UnityEngine;
using System.Collections;


[RequireComponent (typeof (Collider2D))]
public class Destroy_By_Colliding_By_Tag : MonoBehaviour {

	// Set to 'true' if you want to set the gameobjects inactive rather than destroying.
	public bool setInactive;

	// We assign based on personal preference.
	[Tooltip("Active a Destroy when we first enter collision.")]
	public bool onEnter;
	// The number of times the script can be ran, -1 for infinite.
	[Tooltip("The amount of times this can be triggered.  Assigning this to -1 will make it run infinite")]
	public int onEnterLimitedUse = -1;
	// The onEnter tags that can trigger the deletion.
	[Tooltip("The tags that can only activate the Destroy.")]
	public string[] onEnterActivateTags;
	// The onEnter tags to be destroyed.
	[Tooltip("The GameObjects with these tags will be destroyed.")]
	public string[] onEnterDestroyTags;

	// We assign based on personal preference.
	[Tooltip("Active a Destroy when we exit collision.")]
	public bool onExit;
	// The number of times the script can be ran, -1 for infinite.
	[Tooltip("The amount of times this can be triggered.  Assigning this to -1 will make it run infinite")]
	public int onExitLimitedUse = -1;
	// The onExit tags that can trigger the deletion.
	[Tooltip("The tags that can only activate the Destroy.")]
	public string[] onExitActivateTags;
	// The onExit tags to be destroyed.
	[Tooltip("The GameObjects with these tags will be destroyed.")]
	public string[] onExitDestroyTags;


	void Awake(){
		// Check to see if the user made any mistakes.
		DebugCheck();
	}

	// Enter Physical Collision.
	void OnCollisionEnter2D(Collision2D coll){
		// IF we allow it.
		if(onEnter){
			LoopAndCompareTagsOnEnter(coll.gameObject);
		}
	}

	// Exit Physical Collision.
	void OnCollisionExit2D(Collision2D coll){
		// IF we allow it.
		if(onExit){
			LoopAndCompareTagsOnExit(coll.gameObject);
		}
	}

	// Enter Trigger Collision.
	void OnTriggerEnter2D(Collider2D coll){
		// IF we allow it.
		if(onEnter){
			LoopAndCompareTagsOnEnter(coll.gameObject);
		}
	}

	// Exit Trigger Collision.
	void OnTriggerExit2D(Collider2D coll){
		// IF we allow it.
		if(onExit){
			LoopAndCompareTagsOnExit(coll.gameObject);
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
				if(onEnterDestroyTags.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(24, this.GetType(), gameObject);
				}
			}
			// IF we are using onExit.
			if(onExit){
				// IF there is not any onExitActivation tags.
				if(onExitActivateTags.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(21, this.GetType(), gameObject);
				}
				// IF there is not any parents set to OnExitParents.
				if(onExitDestroyTags.Length == 0){
					Helper_Manager.instance.DebugErrorCheck(25, this.GetType(), gameObject);
				}
			}
		}
	}
	
	void LoopAndCompareTagsOnEnter(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < onEnterActivateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == onEnterActivateTags[i]){
				// IF we want to set them inactive,
				// ELSE we want to destroy them.
				if(setInactive){
					// Time to set inactive.
					Helper_Manager.instance.SetActiveGameObjectsByTags(onEnterDestroyTags, false);
				}else{
					// Time to destroy.
					Helper_Manager.instance.DestroyGameObjectsByTags(onEnterDestroyTags);
				}
				// Reduce the scripts life if it has any.
				ReduceOnEnterLimitedUse();
				// Once we have 1 match we are done.
				return;
			}
		}
	}

	void LoopAndCompareTagsOnExit(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < onExitActivateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == onExitActivateTags[i]){
				// IF we want to set them inactive,
				// ELSE we want to destroy them.
				if(setInactive){
					// Time to set inactive.
					Helper_Manager.instance.SetActiveGameObjectsByTags(onExitDestroyTags, false);
				}else{
					// Time to destroy.
					Helper_Manager.instance.DestroyGameObjectsByTags(onExitDestroyTags);
				}
				// Reduce the scripts life if it has any.
				ReduceOnExitLimitedUse();
				// Once we have 1 match we are done.
				return;
			}
		}
	}

	void ReduceOnEnterLimitedUse(){
		// Check to see if the script is infinite.
		if(onEnterLimitedUse != -1){
			// Since it isnt we reduce by 1.
			--onEnterLimitedUse;
			// Check to see if we reached 0.
			if(onEnterLimitedUse <= 0){
				// Since we are 0 or under we deactive a portion of the script.
				DeActivateOnEnter();
			}
		}
	}
	
	void ReduceOnExitLimitedUse(){
		// Check to see if the script is infinite.
		if(onExitLimitedUse != -1){
			// Since it isnt we reduce by 1.
			--onExitLimitedUse;
			// Check to see if we reached 0.
			if(onExitLimitedUse <= 0){
				// Since we are 0 or under we deactive a portion of the script.
				DeActivateOnExit();
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
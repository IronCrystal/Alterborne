using UnityEngine;
using System.Collections;

public class Destroy_Objects_By_Colliding : MonoBehaviour {

	// The number of times the script can be ran, -1 for infinite.
	public int limitedUse = -1;

	// We assign based on personal preference.
	public bool onEnter;
	public bool onExit;

	// The tags that can trigger the deletion.
	public string[] activateTags;

	// The boolean for activating selected destroy.
	public bool isManual;
	// Objects manually place to be destroyed.
	public GameObject[] objectsToBeDestroyed;

	// The boolean for activating destroy by parent.
	public bool isParent;
	// The tags to be destroyed.
	public GameObject[] parents;

	// The boolean for activating destroy by tag.
	public bool isTag;
	// The tags to be destroyed.
	public string[] destroyTags;


	void OnCollisionEnter2D(Collision2D coll){
		if(onEnter){
			IsCheck(coll.gameObject);
		}
	}

	void OnCollisionExit2D(Collision2D coll){
		if(onExit){
			IsCheck(coll.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D coll){
		if(onEnter){
			IsCheck(coll.gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D coll){
		if(onExit){
			IsCheck(coll.gameObject);
		}
	}

	void IsCheck(GameObject coll){
		if(isManual){
			GameManagerDestroy(coll);
		}
		if(isParent){
			LoopAndCompareParents(coll);
		}
		if(isTag){
			LoopAndCompareTags(coll);
		}
	}

	void GameManagerDestroy(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < activateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == activateTags[i]){
				// Time to destroy.
				DestroyGameObjects(objectsToBeDestroyed);
				// Reduce the scripts life if it has any.
				ReduceLimitedUse();
				// Return once we have a match.
				return;
			}
		}
	}

	void LoopAndCompareParents(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < activateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == activateTags[i]){
				// Time to destroy.
				DestroyGameObjectsByParent(parents[i]);
				// Reduce the scripts life if it has any.
				ReduceLimitedUse();
				// Once we have 1 match we are done.
				return;
			}
		}
	}

	void LoopAndCompareTags(GameObject coll){
		// When we have a collision make sure it is the activatedTags begin it.
		for(int i = 0; i < activateTags.Length; i++){
			// IF we have matching tags for activation.
			if(coll.gameObject.tag == activateTags[i]){
				// Time to destroy.
				DestroyGameObjectsByTag(destroyTags);
				// Reduce the scripts life if it has any.
				ReduceLimitedUse();
				// Once we have 1 match we are done.
				return;
			}
		}
	}

	void ReduceLimitedUse(){
		// Check to see if the script is infinite.
		if(limitedUse != -1){
			// Since it isnt we reduce by 1.
			--limitedUse;
			// Check to see if we reached 0.
			if(limitedUse <= 0){
				// Since we are 0 or under we deactive the script.
				DeActivate();
			}
		}
	}

	void DeActivate(){
		// Turn them all off.
		onEnter = false;
		onExit = false;
	}

	// Manually drag game objects for destroy.
	void DestroyGameObjects(GameObject[] objectsToBeDestroyed){
		// Loop through each GameObject in the array to destroy.
		for(int i = 0; i < objectsToBeDestroyed.Length; i++){
			// Destroy the object.
			Destroy(objectsToBeDestroyed[i]);
		}
	}

	// Destroy gameobjects by parent.
	void DestroyGameObjectsByParent(GameObject parent){
		// Get the amount of children.
		int children = parent.transform.childCount;
		// Loop the amount of times as you have children.
		for(int i = 0; i < children; i++){
			// Get each child.
			foreach(Transform child in parent.transform){
				// Destroy each child.
				Destroy (child.gameObject);
			}
		}
	}

	// Destroy gameobjects by tag.
	void DestroyGameObjectsByTag(string[] destroyTags){
		// Loop through all the destroy tags.
		for(int i = 0; i < destroyTags.Length; i++){
			// Grab all GameObjects with the tags for being destroyed.
			GameObject[] objects = GameObject.FindGameObjectsWithTag(destroyTags[i]);
			// Delete each one we get.
			for(int j = 0; j < objects.Length; j++){
				// Destroy each object.
				Destroy (objects[j]);
			}
		}
	}
}
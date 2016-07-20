using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof (Effector2D))]
public class Terrain_Collision_Animation: MonoBehaviour {

	// The animation to be played while colliding.
	[Tooltip("The animation to be played while colliding.")]
	public GameObject terrainAnimation;

	
	void Awake(){
		// Check for mistakes.
		DebugCheck();
	}
	
	void OnTriggerEnter2D(Collider2D coll){
		// Loop through the children of this gameobject.
		foreach(Transform child in coll.transform){
			// IF the child gameobject has a tag of 'feet'?
			if(child.gameObject.tag == "Feet"){
				// Spawn the animation at the feet.
				GameObject anim = Instantiate(terrainAnimation, child.gameObject.transform.position, Quaternion.identity) as GameObject;
				// Set the animation as a child.
				Helper_Manager.instance.SetParentTransform(child.gameObject.transform, anim);
			}
		}
	}

	void OnTriggerExit2D(Collider2D coll){

		// Loop through the children of this gameobject.
		foreach(Transform child in coll.transform){
			// IF the child gameobject has a tag of 'feet'?
			if(child.gameObject.tag == "Feet"){
				// Destroy all child gameobjects.
				Helper_Manager.instance.DestroyGameObjectsByParent(child.gameObject);
			}
		}
	}

	void DebugCheck(){
		// IF there isn't a terrain animation we need to let the user know.
		if(terrainAnimation == null){
			Helper_Manager.instance.DebugErrorCheck(44, this.GetType(), gameObject);
		}
	}
}
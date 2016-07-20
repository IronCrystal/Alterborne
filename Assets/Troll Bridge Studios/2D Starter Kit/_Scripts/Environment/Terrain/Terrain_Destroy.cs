using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class Terrain_Destroy : MonoBehaviour {

	public string[] destroyableByTags;

	public int hp;


	void Awake(){
		DebugCheck();
	}

	void OnTriggerEnter2D(Collider2D coll){

		for(int i = 0; i < destroyableByTags.Length; i++){
			// Is the object being collided with have the tag that can destroy it.
			if(coll.gameObject.tag == destroyableByTags[i]){
				// Reduce the HP.
				hp -= 1;
				// IF the HP is 0.
				if(hp <= 0){
					Destroy (gameObject);
				}
				// The one item has collided and we are done looping.
				return;
			}
		}
	}

	void DebugCheck(){
		// IF the user forgot to put the tags that can destroy this GameObject.
		if(destroyableByTags.Length == 0){
			Helper_Manager.instance.DebugErrorCheck(33, this.GetType(), gameObject);
		}
		// IF the HP for this GameObject is set to 0 initially then notify the user.
		if(hp == 0){
			Helper_Manager.instance.DebugErrorCheck(34, this.GetType(), gameObject);
		}
	}
}
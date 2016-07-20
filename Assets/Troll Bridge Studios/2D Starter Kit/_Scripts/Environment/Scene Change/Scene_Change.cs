using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public class Scene_Change : MonoBehaviour {

	// The tags that trigger the scene change.
	[Tooltip("The tags that trigger the scene change.")]
	public string[] targetTags;
	// The scene that will be loaded.
	[Tooltip("The name of the scene that will be loaded.")]
	public string newScene;
	// The location in the new scene that the player will be spawned at.  This number is correlated to the
	// Transform locations on your Camera.
	[Tooltip("The location in the new scene that the player will be spawned at.  This number is correlated to the " +
	         "Transform locations on your Scene Manager Script.")]
	public int sceneSpawnLocation = 0;
	

	void Awake(){
		// Check for user errors.
		DebugCheck();
	}

	void OnTriggerEnter2D(Collider2D coll){
		// Loop through the array.
		for(int i = 0; i < targetTags.Length; i++){
			// IF this Tag is touched by the Tag(s) you consided in your targetTags.
			if(coll.gameObject.tag == targetTags[i]){
				// We go to the next scene.
				ChangeScene();
			}
		}
	}

	void DebugCheck(){
		// IF the 'TargetTags' is empty.
		if(targetTags.Length == 0){
			Helper_Manager.instance.DebugErrorCheck(30, this.GetType(), gameObject);
		}
		// IF the user forgot to place the Scene they want to go to.
		if(newScene.Equals("")){
			Helper_Manager.instance.DebugErrorCheck(31, this.GetType(), gameObject);
		}
	}

	void ChangeScene(){
		// Save the location number to be used to spawn in the next scene.
		Helper_Manager.sceneSpawnLocation = sceneSpawnLocation;
		// Change the scene.
		Application.LoadLevel(newScene);
	}
}
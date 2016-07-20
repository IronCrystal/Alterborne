using UnityEngine;
using System.Collections;

public class Scene_Manager : MonoBehaviour {

	// This script is in charge of setting up the scene.
	// This will start the music if supplied with a song.
	// This will spawn the player unless the player is already spawned due to this being a singleton.
	// This will setup the locations where you can spawn in this scene when COMING FROM another scene.

	// The music to be played during the Scene.
	[Tooltip("The music to be played during the Scene.  Leaving this empty when the scene loads will make the scene not play music.  " +
	         "If the music being played in the previous scene matches the new scene the music will keep playing.")]
	public AudioClip music;

	// Bool if there is player on the scene.
	[Tooltip("Is there is player on the scene?")]
	public bool isPlayer;
	// Player prefab.
	[Tooltip("The Player Prefab that will be spawned when the Scene starts.")]
	public GameObject player;

	// The list of areas that you can spawn via scene change.
	[Tooltip("The list of Transform locations that you can spawn at when transferring to this scene.")]
	public Transform[] sceneSpawnLocations;

	// Do we show the UI?
	[Tooltip("Do you want to show the UI in this scene?")]
	public bool showUI;
	

	void Awake () {
		// IF there is a player to be spawn.
		if(isPlayer){
			// Check to make sure the user has the scripts working correctly.
			DebugCheck();
			// Let us now spawn the 'Player' at a location.
			SetPlayerLocationOnSceneLoad();
		}

		// Show or Hide the UI for this Scene.
		ShowUI();
	}

	void Start(){
		// First thing the camera does is start the music.
		Sound_Manager.instance.PlayBGMusic(music);
	}
	
	void DebugCheck(){
		// IF the user forgot to assign the player Prefab for spawn.
		if(player == null){
			Helper_Manager.instance.DebugErrorCheck(40, this.GetType(), gameObject);
		}
		// IF the user forgot to assign 'sceneSpawnLocation'.  This is used for the initial spawn of
		// the player and when the player changes scenes.
		if(sceneSpawnLocations.Length == 0){
			Helper_Manager.instance.DebugErrorCheck(41, this.GetType(), gameObject);
		}
	}
	
	void SetPlayerLocationOnSceneLoad(){
		// Grab the number to match the new spot in the scene.
		int location = Helper_Manager.sceneSpawnLocation;
		// Find if there is a GameObject with a tag called "Player" already spawned.
		GameObject pGO = GameObject.FindGameObjectWithTag("Player");
		// IF there is not a Player GameObject.
		// ELSE there is a Player GameObject.
		if(pGO == null){
			// Spawn a Player GameObject at sceneSpawnLocation[location].
			Instantiate(player, sceneSpawnLocations[location].position, Quaternion.identity);
		}else{
			// Set the location at sceneSpawnLocation[location].
			pGO.transform.position = new Vector3(sceneSpawnLocations[location].position.x, sceneSpawnLocations[location].position.y, sceneSpawnLocations[location].position.z);
		}
	}

	void ShowUI(){
		Helper_Manager.instance.SetUIActive(showUI);
	}
}
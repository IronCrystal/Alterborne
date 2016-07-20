using UnityEngine;
using System.Collections;

public class Terrain_Sound_By_Distance : MonoBehaviour {

	// The sound clip to play when Colliding.
	[Tooltip("The sound clip to play when Colliding.")]
	public AudioClip soundClip;
	// The min and max pitch for when this sound is played.
	[Tooltip("The minimum pitch this sound can be played at.")]
	public float minPitch = 1;
	[Tooltip("The maximum pitch this sound can be played at.")]
	public float maxPitch = 1;

	// The distance before playing another sound
	[Tooltip("The distance traveled before another sound clip plays.")]
	public float distance = 1;
	// The distance variable that holds the current distance.
	private float currDistance;

	// Previous position.
	private Vector2 prev;
	// Current position.
	private Vector2 curr;


	void Awake(){
		// Check for mistakes.
		DebugCheck();
	}

	void OnTriggerEnter2D(Collider2D coll){		
		// IF there is a sound to play.
		if(soundClip != null){
			// Set the start distance.
			curr = coll.transform.position;
		}
	}

	void OnTriggerStay2D(Collider2D coll){
		if(coll.gameObject.tag == "Player"){
			// The new is the old.
			prev = curr;
			// Get the current distance.
			curr = coll.transform.position;
			// Compare distances.
			currDistance += Vector2.Distance(curr, prev);

			// IF we have traveled the required amount.
			if(currDistance > distance && Sound_Manager.instance.sfxOn){
				// Play the sound.
				AudioSource soundSource = Sound_Manager.instance.PlaySound(soundClip, coll.transform.position, minPitch, maxPitch);
				// Set the parent of this gameobject.
				Helper_Manager.instance.SetParentTransform(coll.gameObject.transform, soundSource.gameObject);
				// Reset currDistance.
				currDistance = 0;
			}
		}
	}

	void DebugCheck(){
		// IF there isn't a soundClip we need to let the user know.
		if(soundClip == null){
			Helper_Manager.instance.DebugErrorCheck(46, this.GetType(), gameObject);
		}
		// IF the distance is 0 there will be a spam, notify to make greater than 0.
		if(distance == 0){
			Helper_Manager.instance.DebugErrorCheck(47, this.GetType(), gameObject);
		}
	}
}
using UnityEngine;
using System.Collections;

public class End_Of_Animation_Destroy : MonoBehaviour {

	[Tooltip("Place the Animation Clip that is played in your Animator as this script will destroy this GameObject when it is done.")]
	public AnimationClip objectAnimation;

	void Start () {
		// Destroy the GameObject based on the length of the Animation.
		Destroy (gameObject, objectAnimation.length);
	}
}
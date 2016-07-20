using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Collider2D))]
public class Terrain_Pushable : MonoBehaviour {

	// Are we able to move this GameObject.
	[Tooltip("Is this a Movable GameObject right now.")]
	public bool movable = true;

	// The sound clip to play when Colliding.
	[Tooltip("The sound clip to play when Pushing.")]
	public AudioClip soundClip;
	// The min and max pitch for when this sound is played.
	[Tooltip("The minimum pitch this sound can be played at.")]
	public float minPitch = 1;
	[Tooltip("The maximum pitch this sound can be played at.")]
	public float maxPitch = 1;
	
	// Which layers can actually move this GameObject.
	[Tooltip("Which layers can actually move this GameObject.")]
	public LayerMask LayersThatMoveIt;
	// The delay time it takes to push this GameObject.
	[Tooltip("The delay time it takes to push this GameObject.")]
	public float timeToPush = 1f;
	// The speed at which this gets moved.
	[Tooltip("The speed at which this gets moved when being pushed.")]
	public float moveSpeed = 1f;

	// Show the user the raycast on the Scene View.
	[Tooltip("Show the Raycast in the Scene View while the game is playing.")]
	public bool showRaycast;
	// Where to push this object on all 4 sides.
	[Tooltip("The Raycast distance above the GameObject.")]
	public Transform up;
	[Tooltip("The Raycast distance below the GameObject.")]
	public Transform down;
	[Tooltip("The Raycast distance left of the GameObject.")]
	public Transform left;
	[Tooltip("The Raycast distance right of the GameObject.")]
	public Transform right;

	// Timer for timeToPush.
	private float timer;
	// Bool for moving.
	private bool _up, _down, _left, _right = false;

	// The GameObjects Rigidbody.
	private Rigidbody2D rb;
	
	// The sound object that is being played.
	private AudioSource soundSource;

	// The Transform.
	private Transform _transform;


	void OnValidate(){
		// Clamp this so the user has a better idea on the range to set this which will result in faster
		// understanding how this variable works.
		moveSpeed = Mathf.Clamp(moveSpeed, 0f, 5f);
	}

	void Awake(){
		// Check for user mistakes.
		DebugCheck();
		// Get the Transform.
		_transform = gameObject.transform;
	}

	void Start(){
		// Set the timer for push delay.
		timer = timeToPush;
		// Grab the rigidbody for faster access.
		rb = GetComponent<Rigidbody2D>();
	}

	void Update(){

		// Check and make sure we dont have null uses in our checks.
		SideCheck();

		// Are we able to move this GameObject yet.
		if(movable){
			// Display the RayCasts during gameplay?
			ShowRaycast();

			// IF we are colliding with the top trying to move the object down WHILE trying to move down.
			if(Physics2D.Linecast(_transform.position, up.position, LayersThatMoveIt) && Input.GetAxisRaw("Vertical") == -1){
				// Reduce the timer for the timeToPush.
				timer -= Time.deltaTime;
				// IF we have finished the wait time to push.
				if(timer <= 0){
					// Move up.
					_up = true;
					// Play the pushing sound.
					PlayPushSound();
				}
			// ELSE IF we are colliding with the bottom trying to move the object up WHILE trying to move up.
			}else if(Physics2D.Linecast (_transform.position, down.position, LayersThatMoveIt) && Input.GetAxisRaw("Vertical") == 1){
				// Reduce the timer for the timeToPush.
				timer -= Time.deltaTime;
				// IF we have finished the wait time to push.
				if(timer <= 0){
					// Move down.
					_down = true;
					// Play the pushing sound.
					PlayPushSound();
				}
			// IF we are colliding with the right trying to move the object left WHILE trying to move left.
			}else if(Physics2D.Linecast (_transform.position, right.position, LayersThatMoveIt) && Input.GetAxisRaw("Horizontal") == -1){
				// IF we have finished the wait time to push.
				timer -= Time.deltaTime;
				// IF we have finished the wait time to push.
				if(timer <= 0){
					// Move right.
					_right = true;
					// Play the pushing sound.
					PlayPushSound();
				}
			// IF we are colliding with the left trying to move the object right WHILE trying to move right.
			}else if(Physics2D.Linecast (_transform.position, left.position, LayersThatMoveIt) && Input.GetAxisRaw("Horizontal") == 1){
				// IF we have finished the wait time to push.
				timer -= Time.deltaTime;
				// IF we have finished the wait time to push.
				if(timer <= 0){
					// Move left.
					_left = true;
					// Play the pushing sound.
					PlayPushSound();
				}
			// ELSE reset everything.
			}else{
				_up = false;
				_down = false;
				_right = false;
				_left = false;
				timer = timeToPush;
				Destroy (soundSource);
			}
		}
	}

	void FixedUpdate(){

		Vector2 movement;
		// Which way are we moving.
		if(_up && up != _transform){
			movement = new Vector2(0f, -1f);
		}else if(_down && down != _transform){
			movement = new Vector2(0f, 1f);
		}else if(_right && right != _transform){
			movement = new Vector2(-1f, 0f);
		}else if(_left && left != _transform){
			movement = new Vector2(1f, 0f);
		}else{
			movement = new Vector2(0f, 0f);
		}
		// Set the velocity of the object.
		rb.velocity = movement * moveSpeed;
	}

	void SideCheck(){
		// Make checks incase the user left some sides null.
		if(up == null){
			up = _transform;
		}
		if(down == null){
			down = _transform;
		}
		if(left == null){
			left = _transform;;
		}
		if(right == null){
			right  = _transform;;
		}
	}

	void ShowRaycast(){
		// IF we are showing the raycast.
		if(showRaycast){
			// Show the raycast.
			Debug.DrawLine(_transform.position, up.transform.position);
			Debug.DrawLine(_transform.position, down.transform.position);
			Debug.DrawLine(_transform.position, right.transform.position);
			Debug.DrawLine(_transform.position, left.transform.position);
		}
	}

	void PlayPushSound(){
		// IF we have a SoundClip AND 
		// We don't have an active SoundSource anything.
		if(soundClip != null && soundSource == null){
			soundSource = Sound_Manager.instance.PlaySound(soundClip, _transform.position, minPitch, maxPitch);
		}
	}
		
	void DebugCheck(){
		// IF the moveSpeed is 0.
		if(moveSpeed == 0){
			Helper_Manager.instance.DebugErrorCheck(35, this.GetType(), gameObject);
		}
		// IF the Transform up, down, left and right are set to null.
		if(up == null && down == null && left == null && right == null){
			Helper_Manager.instance.DebugErrorCheck(36, this.GetType(), gameObject);
		}
	}
}
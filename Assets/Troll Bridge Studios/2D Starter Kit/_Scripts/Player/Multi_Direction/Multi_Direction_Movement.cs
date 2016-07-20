using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody2D))]
[RequireComponent (typeof (Player_Manager))]
public class Multi_Direction_Movement : MonoBehaviour {

	// Speed of the GameObject when moving.
	[Tooltip("Speed of the Player when it's moving.")]
	public float speed;
	// Vector direction we are moving.
	private Vector2 movement;
	// The GameObjects Rigidbody.
	private Rigidbody2D rb;
	// The Player State
	private Player_Manager _playerManager;
	// Holders for the movements.
	private float moveHorizontal;
	private float moveVertical;


	void Awake(){
		// Get the Player State.
		_playerManager = GetComponent<Player_Manager>();
		// Get the Rigidbody2D Component.
		rb = GetComponent<Rigidbody2D>();
		// Check for mistakes.
		DebugCheck();
	}

	void Update(){
		// IF we are allowed to move.
		if(_playerManager.playerState.CanPlayerMove){
			// Get a -1, 0 or 1.
			moveHorizontal = Input.GetAxis ("Horizontal");
			moveVertical = Input.GetAxis ("Vertical");
		}
	}
	
	void FixedUpdate() {
		// IF we are able to move.
		// ELSE we cannot move.
		if(_playerManager.playerState.CanPlayerMove){
			// Get Vector2 direction.
			movement = new Vector2(moveHorizontal * _playerManager.playerState.PlayerInvertX, moveVertical * _playerManager.playerState.PlayerInvertY);
			// Apply direction with speed, alterspeed and if we have the ability to even move.
			movement *= speed * _playerManager.playerState.PlayerAlterSpeed;
			// IF the user has an animation set and ready to go.
			PlayAnimation(moveHorizontal, moveVertical);
			// Apply force.
			rb.AddForce(movement);
		}else{
			PlayAnimation(0f, 0f);
		}
	}

	void PlayAnimation(float hor, float vert){
		// IF the user has an animation set and ready to go.
		if(_playerManager.playerState.PlayerAnimation != null){
			// IF the player has a Four Direction Animation,
			// ELSE IF the player has a Eight Direction Animation.
			if(_playerManager.playerState.FourDirAnim){
				// Play animations.
				Helper_Manager.instance.FourDirectionAnimation(hor * _playerManager.playerState.PlayerInvertX, vert * _playerManager.playerState.PlayerInvertY, _playerManager.playerState.PlayerAnimation);
			}else if(_playerManager.playerState.EightDirAnim){
				// Play animation.
				Helper_Manager.instance.EightDirectionAnimation(hor * _playerManager.playerState.PlayerInvertX, vert * _playerManager.playerState.PlayerInvertY, _playerManager.playerState.PlayerAnimation);
			}
		}
	}

	void DebugCheck(){
		// IF the user forgot to apply a movement speed.
		if(speed == 0){
			Helper_Manager.instance.DebugErrorCheck(60, this.GetType(), gameObject);
		}
	}
}
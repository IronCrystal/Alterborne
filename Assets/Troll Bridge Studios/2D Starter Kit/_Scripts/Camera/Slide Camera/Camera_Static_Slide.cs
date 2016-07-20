using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class Camera_Static_Slide : MonoBehaviour {
	// Used for the user to input their board section width and height.
	[Tooltip("The desired Camera Width.")]
	public int cameraWidth;
	[Tooltip("The desired Camera Height.  If you have a UI that is not transparent then you will need to incorporate the height of the UI for this.")]
	public int cameraHeight;

	// The amount to slide the player over when a camera pans.
	[Tooltip("The amount to slide the player over when a camera pans.")]
	public float playerSlideAmount = 0.2f;

	// Make a public speed for the Camera Panning, with a default speed of 30f.
	[Tooltip("The camera panning speed.")]
	public float cameraSlideSpeed = 30f;

	// 4 GameObjects used to set the boundaries of the camera.
	[Tooltip("Bottom border for the Camera.")]
	public GameObject bottomCameraBorder;
	[Tooltip("Left border for the Camera.")]
	public GameObject leftCameraBorder;

	// The Camera and min and max bounds.
	private Camera _camera;
	private int direction;
	private Vector3 moveRight;
	private Vector3 moveLeft;
	private Vector3 moveDown;
	private Vector3 moveUp;
	private Vector3 destination;

	// Cache the Transform.
	private Transform _transform;
	// Used for if the camera is panning or not.
	private bool panning = false;
	// Is it ok for the Camera to move.
	private bool timeToMove = false;
	// Used to store the player GameObject.
	private GameObject playerGO;
	// Used to store the player GameObjects Transform.
	private Transform _transformPlayer;
	// Used to store the players Player State.
	private Player_Manager _playerManager;

	// Border float holders for the camera boundaries.
	private float bb, lb;

	// The ui height from the panel UI
	private float _uiHeight;
	// The offset if the UI is at the top.
	private float moveUpOffset;


	void Awake () {
		// Get the Transform.
		_transform = gameObject.transform;
		// Grab the Camera Component.
		_camera = GetComponent<Camera>();
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();

		// Get the UI height.
		_uiHeight = Helper_Manager.instance.GetUIHeight();
		// Set the offset for moving UP since the UI is at the top.
		moveUpOffset = _uiHeight / cameraHeight;
		// Set the camera ratios.
		Helper_Manager.instance.SetCameraRatios((float)cameraWidth, (float)cameraHeight, Screen.width, Screen.height, _camera);
		// Position in the bottom left initially so the user doesnt have 
		// to set the position of the camera.
		PositionCamera();
	}

	void Start(){
		// Grab the player GameObject.
		playerGO = GameObject.FindGameObjectWithTag("Player");
		// Grab the Players Transform.
		_transformPlayer = playerGO.transform;
		// Grab the Player State.
		_playerManager = playerGO.GetComponent<Player_Manager>();
		// Now lets adjust the camera so that it is where the player is.
		AlignCamera();
		// Lets get the UP DOWN LEFT RIGHT screens.
		calcCamPos();
	}

	void Update () {
		// Detect IF we are at the edges of the Camera to Pan.
		CameraEdgeDetection();
	}

	void LateUpdate(){
		// Move the Camera.
		CameraMove();
	}

	void DebugCheck(){
		// IF the user forgot to put in the Cameras Width.
		if(cameraWidth <= 0){
			Helper_Manager.instance.DebugErrorCheck(2, this.GetType(), gameObject);
		}
		// IF the user forgot to put in the Cameras Height.
		if(cameraHeight <= 0){
			Helper_Manager.instance.DebugErrorCheck(3, this.GetType(), gameObject);
		}
		// IF the player forgot to assign a Camera Slide Speed.
		if(cameraSlideSpeed == 0){
			Helper_Manager.instance.DebugErrorCheck(4, this.GetType(), gameObject);
		}
		// IF the bottom camera border is null.
		if(bottomCameraBorder == null){
			Helper_Manager.instance.DebugErrorCheck(5, this.GetType(), gameObject);
		}
		// IF the left camera border is null.
		if(leftCameraBorder == null){
			Helper_Manager.instance.DebugErrorCheck(7, this.GetType(), gameObject);
		}
	}

	void calcCamPos() {
		//Calculate new Vector3 camera positions for when we touch an edge.
		moveRight = new Vector3(_transform.position.x + cameraWidth/100f, _transform.position.y, _transform.position.z);
		moveLeft = new Vector3(_transform.position.x - cameraWidth/100f, _transform.position.y, _transform.position.z);
		moveUp = new Vector3(_transform.position.x, _transform.position.y + (cameraHeight-_uiHeight)/100f, _transform.position.z);
		moveDown = new Vector3(_transform.position.x, _transform.position.y - (cameraHeight-_uiHeight)/100f, _transform.position.z);
	}

	void moveCam(int dir) {
		// We are currently panning.
		panning = true;
		switch(dir) {
			
		// Move UP
		case 1:
			_transform.position = Vector3.MoveTowards(_transform.position, moveUp, cameraSlideSpeed * Time.deltaTime);
			break;
		// Move DOWN
		case 2:
			_transform.position = Vector3.MoveTowards(_transform.position, moveDown, cameraSlideSpeed * Time.deltaTime);
			break;
		// Move LEFT
		case 3:
			_transform.position = Vector3.MoveTowards(_transform.position, moveLeft, cameraSlideSpeed * Time.deltaTime);
			break;
		// Move RIGHT
		case 4:
			_transform.position = Vector3.MoveTowards(_transform.position, moveRight, cameraSlideSpeed * Time.deltaTime);
			break;
		}
	}

	void CameraEdgeDetection(){
		// Are we not panning?
		if(!panning) {
			// Grab the Vector3 viewport of Camera to the Player. Range is 0 -> 1.
			Vector3 screenPos = _camera.WorldToViewportPoint (_transformPlayer.position);
			// IF moving to the LEFT.
			if (screenPos.x < 0f) {
				MoveCamLeft();
			}else if (screenPos.x > 1f) {
				// ELSE IF moving to the RIGHT.
				MoveCamRight();
			}else if (screenPos.y + moveUpOffset > 1) {
				// ELSE IF moving to the TOP.
				MoveCamUp();
			}else if (screenPos.y < 0f) {
				// ELSE IF moving to the BOTTOM.
				MoveCamDown();
			}
		}
	}

	void CameraMove(){

		// Are we able to move?
		if(timeToMove){
			
			moveCam(direction);
			
			// IF we reached our destination!!!
			if(_transform.position == destination){
				// Then we do not move the camera anymore.
				timeToMove = false;
				
				// We are not panning anymore.
				panning = false;
				
				// Since we have made it to the destination, lets recalculate our
				// UP DOWN LEFT AND RIGHT possibilities.
				calcCamPos();

				// We can now move the character.
				ChangePlayerMovement(true);
			}
		}
	}

	void ChangePlayerMovement(bool isMovable){
		// Change the player variable for movement purposes.
		_playerManager.playerState.CanPlayerMove = isMovable;
	}

	void MoveCamLeft(){
		// Set the bool (timeToMove) to true since we are panning.
		timeToMove = true;
		// Assign the direction to Pan the Camera.
		direction = 3;
		// The new Vector3 location to move to.
		destination = moveLeft;
		
		// As the camera is moving we need to move Link a little bit over.
		_transformPlayer.Translate(new Vector2(-playerSlideAmount, 0f));
		// Assign the player movement variable to false so the player doesn't move during transition.
		ChangePlayerMovement(false);
	}
	
	void MoveCamRight(){
		// Set the bool (timeToMove) to true since we are panning.
		timeToMove = true;
		// Assign the direction to Pan the Camera.
		direction = 4;
		// The new Vector3 location to move to.
		destination = moveRight;
		
		// As the camera is moving we need to move Link a little bit over.
		_transformPlayer.Translate(new Vector2(playerSlideAmount, 0f));
		// Assign the player movement variable to false so the player doesn't move during transition.
		ChangePlayerMovement(false);
	}
	
	void MoveCamUp(){
		// Set the bool (timeToMove) to true since we are panning.
		timeToMove = true;
		// Assign the direction to Pan the Camera.
		direction = 1;
		// The new Vector3 location to move to.
		destination = moveUp;
		
		// As the camera is moving we need to move Link a little bit over.
		_transformPlayer.Translate(new Vector2(0f, playerSlideAmount));
		// Assign the player movement variable to false so the player doesn't move during transition.
		ChangePlayerMovement(false);
	}
	
	void MoveCamDown(){
		// Set the bool (timeToMove) to true since we are panning.
		timeToMove = true;
		// Assign the direction to Pan the Camera.
		direction = 2;
		// The new Vector3 location to move to.
		destination = moveDown;
		
		// As the camera is moving we need to move Link a little bit over.
		_transformPlayer.Translate(new Vector2(0f, -playerSlideAmount));
		// Assign the player movement variable to false so the player doesn't move during transition.
		ChangePlayerMovement(false);
	}

	void PositionCamera(){
		// We need to get the position of the left and bottom tile.
		SetBottomLeftBounds(bottomCameraBorder, leftCameraBorder);
		// Now we need to get the camera to be positioned at the bottom left.
		float halfWidth = cameraWidth/200f;
		float halfHeight = _camera.orthographicSize;
		// Now that we have what we need lets set the position.
		_transform.position = new Vector3(lb + halfWidth, bb + halfHeight, _transform.position.z);
	}

	void SetBottomLeftBounds(GameObject _bottomCameraBorder, GameObject _leftCameraBorder){
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _BColl = _bottomCameraBorder.GetComponent<Collider2D>();
		Renderer _BRend = _bottomCameraBorder.GetComponent<Renderer>();
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _LColl = _leftCameraBorder.GetComponent<Collider2D>();
		Renderer _LRend = _leftCameraBorder.GetComponent<Renderer>();

		// let's check, 
		// IF the bounding border is an invisible Collider object, Border. (Collider && !Renderer),
		// ELSE IF the bounding border is a visable Sprite || Collider Sprite, Ground Tile || Wall Tile.,
		// ELSE then the user has just made a point where they do not want the camera to go past -- Just a plain GameObject.

		// Bottom Collider check.
		if((_BColl != null && _BRend == null)){
			bb = _BColl.bounds.max.y;
		}else if((_BColl != null && _BRend != null) || (_BColl == null && _BRend != null)){
			bb = _BRend.bounds.min.y;
		}else {
			bb = _bottomCameraBorder.transform.position.y;
		}

		// Left Collider check.
		if((_LColl != null && _LRend == null)){
			lb = _LColl.bounds.max.x;
		}else if((_LColl != null && _LRend != null) || (_LColl == null && _LRend != null)){
			lb = _LRend.bounds.min.x;
		}else {
			lb = _leftCameraBorder.transform.position.x;
		}
	}

	void AlignCamera(){
		// Grab the Vector3 viewport of Camera to the Player.
		Vector3 screenPos = _camera.WorldToViewportPoint (_transformPlayer.position);
		// Floor the float of number of screens away on the X axis.
		int tempX = Mathf.FloorToInt(screenPos.x);
		// Floor the float of number of screens away on the Y axis.
		int tempY = Mathf.FloorToInt(screenPos.y);
		// Start the Camera here.
		_transform.position = new Vector3(_transform.position.x + (tempX * (cameraWidth/100f)), _transform.position.y + (tempY * ((cameraHeight-_uiHeight)/100f)), _transform.position.z);
	}
}
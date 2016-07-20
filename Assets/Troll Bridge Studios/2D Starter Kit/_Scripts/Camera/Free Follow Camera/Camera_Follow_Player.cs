using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Camera))]
public class Camera_Follow_Player : MonoBehaviour {

	// Used for the user to input their board section width and height.
	[Tooltip("The desired Camera Width.")]
	public int cameraWidth;
	[Tooltip("The desired Camera Height.")]
	public int cameraHeight;

	// Boolean to decide if the camera should be stationary.
	[Tooltip("Create a moving (true) or stationary (false) Camera.  This can be useful in small scenes if you do not want " +
		"the camera to follow the player.")]
	public bool isCameraMoving = true;
	// 4 GameObjects used to set the boundaries of the camera.
	[Tooltip("Bottom border for the Camera.")]
	public GameObject bottomCameraBorder;
	[Tooltip("Left border for the Camera.")]
	public GameObject leftCameraBorder;
	[Tooltip("Top border for the Camera.  " +
		"If you have a UI that is not transparent then you will need to set a GameObject for this that will compensate " +
		"for the UI height so the UI doesn't interfere with your scene.")]
	public GameObject topCameraBorder;
	[Tooltip("Right border for the Camera")]
	public GameObject rightCameraBorder;

	// Used to store the player GameObject.
	private GameObject playerGO;
	private Transform playerTransform;

	// The gameobjects Transform.
	private Transform _transform;

	// The Camera and min and max bounds.
	private Camera _camera;
	private float boundMinX;
	private float boundMaxX;
	private float boundMinY;
	private float boundMaxY;

	// Used for Camera bounds.
	private float leftBound;
	private float rightBound;
	private float bottomBound;
	private float topBound;

	// The cameras x and y.
	private float camX;
	private float camY;

	// Border float holders for the camera boundaries.
	private float bb, tb, lb, rb;

	// The ui offset from the panel UI.
	private float heightOffset;
	

	void Awake(){
		// Set the transform.
		_transform = gameObject.transform;
		// Grab the Camera Component.
		_camera = GetComponent<Camera>();
		// Get the UI height.
		heightOffset = Helper_Manager.instance.GetUIHeight();
		// Adjust the Camera ratios.
		Helper_Manager.instance.SetCameraRatios((float)cameraWidth, (float)cameraHeight, Screen.width, Screen.height, _camera);
		// If the camera is moving then we need to set the boundaries.
		if(isCameraMoving){
			// Set the bottom, left, top and right borders.
			SetCameraBounds(bottomCameraBorder, topCameraBorder, leftCameraBorder, rightCameraBorder);
		}
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
	}

	void Start(){
		// Get the player GameObject.
		playerGO = GameObject.FindGameObjectWithTag("Player");
		playerTransform = playerGO.transform;
	}

	void Update () {
		// IF the camera is moving.
		if(isCameraMoving){
			// Clamp values between the bounds.
			camX = Mathf.Clamp(playerTransform.position.x, leftBound, rightBound);
			camY = Mathf.Clamp(playerTransform.position.y + heightOffset/200f, bottomBound, topBound);
		}
	}

	void LateUpdate(){
		// IF the camera is moving.
		if(isCameraMoving){
			_transform.position = new Vector3(camX, camY, _transform.position.z);
		}
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
		// IF the Bottom Camera Border is not setup with a GameObject AND we have a moving camera.
		if(bottomCameraBorder == null && isCameraMoving){
			Helper_Manager.instance.DebugErrorCheck(5, this.GetType(), gameObject);
		}
		// IF the Top Camera Border is not setup with a GameObject AND we have a moving camera.
		if(topCameraBorder == null && isCameraMoving){
			Helper_Manager.instance.DebugErrorCheck(6, this.GetType(), gameObject);
		}
		// IF the Left Camera Border is not setup with a GameObject AND we have a moving camera.
		if(leftCameraBorder == null && isCameraMoving){
			Helper_Manager.instance.DebugErrorCheck(7, this.GetType(), gameObject);
		}
		// IF the Right Camera Border is not setup with a GameObject AND we have a moving camera.
		if(rightCameraBorder == null && isCameraMoving){
			Helper_Manager.instance.DebugErrorCheck(8, this.GetType(), gameObject);
		}
		// IF the user mixed up the Top Camera Border and the Bottom Camera Border.
		if(bb > tb){
			Helper_Manager.instance.DebugErrorCheck(9, this.GetType(), gameObject);
		}
		// IF the user mixed up the Left Camera Border and the Right Camera Border.
		if(lb > rb){
			Helper_Manager.instance.DebugErrorCheck(10, this.GetType(), gameObject);
		}
	}

	void SetXBounds(float minX, float maxX){
		// Set min and max x bounds.
		boundMinX = minX;
		boundMaxX = maxX;
	}

	void SetYBounds(float minY, float maxY){
		// Set min and max y bounds
		boundMinY = minY;
		boundMaxY = maxY;
	}

	void RefreshCameraBounds(){
		// Get the camera ratios.
		float camVertExtent = _camera.orthographicSize;
		float camHorzExtent = _camera.aspect * camVertExtent;
		
		// Grab the current bounds.
		leftBound   = boundMinX + camHorzExtent;
		rightBound  = boundMaxX - camHorzExtent;
		bottomBound = boundMinY + camVertExtent;
		topBound    = boundMaxY - camVertExtent;
	}

	public void SetCameraBounds(GameObject _bottomCameraBorder, GameObject _topCameraBorder, GameObject _leftCameraBorder, GameObject _rightCameraBorder){
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _BColl = _bottomCameraBorder.GetComponent<Collider2D>();
		Renderer _BRend = _bottomCameraBorder.GetComponent<Renderer>();
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _TColl = _topCameraBorder.GetComponent<Collider2D>();
		Renderer _TRend = _topCameraBorder.GetComponent<Renderer>();
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _LColl = _leftCameraBorder.GetComponent<Collider2D>();
		Renderer _LRend = _leftCameraBorder.GetComponent<Renderer>();
		// Grabbing the collider2d and the renderer if they exists for future checks.
		Collider2D _RColl = _rightCameraBorder.GetComponent<Collider2D>();
		Renderer _RRend = _rightCameraBorder.GetComponent<Renderer>();
		
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

		// Top Collider check.
		if((_TColl != null && _TRend == null)){
			tb = _TColl.bounds.min.y;
		}else if((_TColl != null && _TRend != null) || (_TColl == null && _TRend != null)){
			tb = _TRend.bounds.max.y;
		}else {
			tb = _topCameraBorder.transform.position.y;
		}

		// Left Collider check.
		if((_LColl != null && _LRend == null)){
			lb = _LColl.bounds.max.x;
		}else if((_LColl != null && _LRend != null) || (_LColl == null && _LRend != null)){
			lb = _LRend.bounds.min.x;
		}else {
			lb = _leftCameraBorder.transform.position.x;
		}

		// Right Collider check.
		if((_RColl != null && _RRend == null)){
			rb = _RColl.bounds.min.x;
		}else if((_RColl != null && _RRend != null) || (_RColl == null && _RRend != null)){
			rb = _RRend.bounds.max.x;
		}else {
			rb = _rightCameraBorder.transform.position.x;
		}

		// Set the new bounds.
		SetXBounds(lb, rb);
		SetYBounds(bb, tb);
		RefreshCameraBounds();
	}
}
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Helper_Manager : MonoBehaviour {

	// The Integer that correlates to the scenes spawn location.
	public static int sceneSpawnLocation;
	// Used for Singleton.
	static Helper_Manager _instance = null;
	// Used for Screen changing.  Store the Height and the Width of the Camera.
	private int camHeight, camWidth;
	// The UI in your game.
	[Tooltip("The UI that is in your game.  This is optional and will work if you leave this blank/null.")]
	public GameObject UI;


	public static Helper_Manager instance {
		get {
			if (_instance == null) {
				_instance = Object.FindObjectOfType(typeof(Helper_Manager)) as Helper_Manager;
				
				if (_instance == null) {
					GameObject go = new GameObject("_Helper_Manager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<Helper_Manager>();
				}
			}
			return _instance;
		}
	}

	void Awake(){
		// Keep this gameobject through all scenes changes.
		DontDestroyOnLoad(gameObject);
	}

	public void QuitGame(){
		Application.Quit();
	}

	public void SetTimeScale(float timeScale){
		// Set the time scaling.
		Time.timeScale = timeScale;
	}

	public void SetUIActive(bool isActive){
		// IF we have a UI
		if(UI != null){
			// Set the UI activity.
			UI.SetActive(isActive);
		}
	}

	public float GetUIHeight(){
		// IF we have a UI Panel so grab the Height of the Panel.
		// ELSE we dont have a UI Panel so the Height will be set to 0.
		if(UI != null){
			// Get the new height of the UI Panel.
			return UI.GetComponent<RectTransform>().rect.height;
		}else{
			// No Panel UI.
			return 0f;
		}
	}

	public void ChangeScene(string newScene){
		// Change the scene.
		Application.LoadLevel(newScene);
	}

	// Fading for images.
	public IEnumerator FadeImage(Image image, float fadeTime, float from, float to){
		// IF we have a null image.
		if(image == null){
			yield break;
		}
		// Loop in a lerp manner to get a smooth fade.
		for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / fadeTime){
			// IF the Image is destroyed before it is finished fading.
			if(image == null){
				yield break;
			}
			// Get the images color.
			Color col = image.color;
			// Smooth the alpha.
			col.a = Mathf.SmoothStep(from, to, x);
			// Set the color.
			image.color = col;
			yield return null;
		}
	}

	// Fading for text.
	public IEnumerator FadeText(Text txt, float fadeTime, float from, float to){
		// IF we have a null text.
		if(txt == null){
			yield break;
		}
		
		for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / fadeTime){
			// IF the Image is destroyed before it is finished fading.
			if(txt == null){
				yield break;
			}
			// Get the text color.
			Color col = txt.color;
			// Smooth the alpha
			col.a = Mathf.SmoothStep(from, to, x);
			// Set the color.
			txt.color = col;
			yield return null;
		}
	}

	// Grow/Shrink for images.
	public IEnumerator GrowShrinkImage(Image image, float resizeTime, float fromX, float fromY, float toX, float toY){
		// IF we have a null image.
		if(image == null){
			yield break;
		}

		for(float x = 0.0f; x < 1.0f; x += Time.deltaTime / resizeTime){
			// IF the image is destroyed before it is finished resizing.
			if(image == null){
				yield break;
			}
			// Smooth the sizing.
			image.rectTransform.localScale = new Vector2(Mathf.SmoothStep(fromX, toX, x), Mathf.SmoothStep(fromY, toY, x));
			yield return null;
		}
	}

	// Make the text type out based on the text speed.
	public IEnumerator TypeText(Text txt, float pauseTime, string dialogue, AudioClip typeSound){
		// IF we have a null text.
		if(txt == null){
			yield break;
		}

		txt.text = "";
		for(int i = 0; i <= dialogue.Length; i++){
			txt.text = dialogue.Substring(0, i);
			if(typeSound != null){
				Sound_Manager.instance.PlaySound(typeSound, transform.position, 1f, 1f);
			}
			yield return new WaitForSeconds(pauseTime);
			yield return null;
		}
	}

	// Make a method for setting parent for objects.
	public void SetParentTransform(Transform parent, GameObject child){
		// IF we actually have a child gameobject and a parent to place it in.
		if(child != null && parent != null){
			// Set its parent to the current child GameObject.
			child.transform.SetParent(parent);
		}
	}

	// Set the scaling of a gameobject.
	public void SetGameObjectScaling(GameObject go,float x, float y, float z){
		// Assign the gameobject with scales from X, Y and Z.
		go.transform.localScale = new Vector3(x, y, z);
	}

	// Set the gameobjects activity by tags.
	public void SetActiveGameObjectsByTags(string[] activeTags, bool isActive){
		// Loop through all the active tags.
		for(int i = 0; i < activeTags.Length; i++){
			// Grab all GameObjects with the tags.
			GameObject[] objects = GameObject.FindGameObjectsWithTag(activeTags[i]);
			// Loop through the array.
			for(int j = 0; j < objects.Length; j++){
				// Set the active of each object.
				objects[j].SetActive(isActive);
			}
		}
	}

	// Remove all the gameobjects by tags.
	public void DestroyGameObjectsByTags(string[] destroyTags){
		// Loop through all the destroy tags.
		for(int i = 0; i < destroyTags.Length; i++){
			// Grab all GameObjects with the tags for being destroyed.
			GameObject[] objects = GameObject.FindGameObjectsWithTag(destroyTags[i]);
			// Loop through the array.
			for(int j = 0; j < objects.Length; j++){
				// Destroy each object.
				Destroy (objects[j]);
			}
		}
	}

	// Set the gameobjects activity by parent
	public void SetActiveGameObjectsByParent(GameObject parent, bool isActive){
		// Get the amount of children.
		int children = parent.transform.childCount;
		// Loop the amount of times as you have children.
		for(int i = 0; i < children; i++){
			// Get each child.
			foreach(Transform child in parent.transform){
				// Set the active of each object.
				child.gameObject.SetActive(isActive);
			}
		}
	}
	
	// Destroy gameobjects by parent.
	public void DestroyGameObjectsByParent(GameObject parent){
		// Get the amount of children.
		int children = parent.transform.childCount;
		// Loop the amount of times as you have children.
		for(int i = 0; i < children; i++){
			// Get each child.
			foreach(Transform child in parent.transform){
				// Destroy each child.
				Destroy (child.gameObject);
			}
		}
	}

	// Set the gameobjects activity manually.
	public void SetActiveGameObjects(GameObject[] gameObjectsToActive, bool isActive){
		// Loop through all the gameobjects.
		for(int i = 0; i < gameObjectsToActive.Length; i++){
			// Set the activity of each gameobject.
			gameObjectsToActive[i].SetActive(isActive);
		}
	}

	// Destroy gameobjects by what the user placed.
	public void DestroyGameObjects(GameObject[] gameObjectsToDestroy){
		// Loop through all the destroy tags.
		for(int i = 0; i < gameObjectsToDestroy.Length; i++){
			// Delete each one we get.
			Destroy (gameObjectsToDestroy[i]);
		}
	}

	public void ChangeScreenMode(bool isFullscreen){
		// IF we are in full screen then make it windowed.
		// ELSE we are in window then lets make it fullscreen.
		if(isFullscreen){
			ChangeResolution(Screen.width, Screen.height, true);
		}else{
			ChangeResolution(Screen.width, Screen.height, false);
		}
	}

	public void ChangeResolution(int screenWidth, int screenHeight, bool fullscreen){
		// Set the resolution to be changed at the end of the execution.
		Screen.SetResolution(screenWidth, screenHeight, fullscreen);
		// Get the main camera.
		Camera cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
		// Now adjust the visuals with the new settings.
		SetCameraRatios(camWidth, camHeight, screenWidth, screenHeight, cam);
	}

	public void SetCameraRatios(float cameraWidth, float cameraHeight, int screenWidth, int screenHeight, Camera _camera){
		// Store original camera dimensions.
		camWidth = (int) cameraWidth;
		camHeight = (int) cameraHeight;
		// Set the desired aspect ratio.
		float targetAspect = cameraWidth / cameraHeight;
		// Determine the game window's current aspect ratio.
		float windowAspect = (float)screenWidth / (float)screenHeight;
		// Current viewport height should be scaled by this amount.
		float scaleHeight = windowAspect / targetAspect;
		// Set the camera height to the desired height of the cameraHeight.
		_camera.orthographicSize = cameraHeight/200f;
		
		// IF scaled height is larger than the width,
		// ELSE IF scaled width is larger than the height.
		// ELSE scaled height is the same as scaled width.
		if (scaleHeight < 1.0f)	{
			// Get Camera Viewport Rect
			Rect rect = _camera.rect;
			// Set width.
			rect.width = 1.0f;
			// Set height to scaleHeight.
			rect.height = scaleHeight;
			// Set the x.
			rect.x = 0;
			// Set the y.
			rect.y = (1.0f - scaleHeight) / 2.0f;
			// Apply changes.
			_camera.rect = rect;

		}else if(scaleHeight > 1.0f) {
			// Get the scaled width.
			float scaleWidth = 1.0f / scaleHeight;
			// Get Camera Viewport Rect.
			Rect rect = _camera.rect;
			// Set width.
			rect.width = scaleWidth;
			// Set height.
			rect.height = 1.0f;
			// Set x.
			rect.x = (1.0f - scaleWidth) / 2.0f;
			// Set y.
			rect.y = 0;
			// Apply changes.
			_camera.rect = rect;
		}
	}

	public void FourDirectionAnimation(float moveHorizontal, float moveVertical, Animator anim){
		// IF we are moving we set the animation IsMoving to true,
		// ELSE we are not moving.
		if(moveHorizontal != 0 || moveVertical != 0){
			anim.SetBool("IsMoving", true);
		}else{
			anim.SetBool("IsMoving", false);
		}

		// We are wanting to go in the positive X direction.
		if(moveHorizontal > 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 4);
		// We are wanting to move in the negative X direction.
		}else if(moveHorizontal < 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 2);
		// We are wanting to move in the negative Y direction.
		}else if(moveVertical < 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 3);
		// We are wanting to move in the positive Y direction.
		}else if(moveVertical > 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 1);
		}
	}

	public void FourDirectionAnimation(float moveHorizontal, float moveVertical, bool isMoving, Animator anim){
		// Manually set if this GameObject is moving for animation.
		anim.SetBool("IsMoving", isMoving);
		
		// We are wanting to go in the positive X direction.
		if(moveHorizontal > 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 4);
			// We are wanting to move in the negative X direction.
		}else if(moveHorizontal < 0 && Mathf.Abs(moveVertical) <= Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 2);
			// We are wanting to move in the negative Y direction.
		}else if(moveVertical < 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 3);
			// We are wanting to move in the positive Y direction.
		}else if(moveVertical > 0 && Mathf.Abs(moveVertical) > Mathf.Abs(moveHorizontal)){
			anim.SetInteger("Direction", 1);
		}
	}

	public void EightDirectionAnimation(float moveHorizontal, float moveVertical, Animator anim){
		// IF we are moving we set the animation IsMoving to true,
		// ELSE we are not moving.
		if(moveHorizontal != 0 || moveVertical != 0){
			anim.SetBool("IsMoving", true);
		}else{
			anim.SetBool("IsMoving", false);
		}

		// IF we are going bottom right - Direction 8.
		// ELSE IF we are going bottom left - Direction 7.
		// ELSE IF we are going top left - Direction 6.
		// ELSE IF we are going top right - Direction 5.
		// ELSE IF we are going right - Direction 4.
		// ELSE IF we are going down - Direction 3.
		// ELSE IF we are going left - Direction 2.
		// ELSE IF we are going up - Direction 1.
		if(moveHorizontal > 0 && moveVertical < 0){
			// Set the down right animation.
			anim.SetInteger("Direction", 8);

		}else if(moveHorizontal < 0 && moveVertical < 0){
			// Set the down left animation.
			anim.SetInteger("Direction", 7);

		}else if(moveHorizontal < 0 && moveVertical > 0){
			// Set the up left animation.
			anim.SetInteger("Direction", 6);

		}else if(moveHorizontal > 0 && moveVertical > 0){
			// Set the up right animation.
			anim.SetInteger("Direction", 5);

		}else if(moveHorizontal > 0){
			// Set the right animation.
			anim.SetInteger("Direction", 4);

		}else if(moveVertical < 0){
			// Set the down animation.
			anim.SetInteger("Direction", 3);

		}else if(moveHorizontal < 0){
			// Set the left animation.
			anim.SetInteger("Direction", 2);

		}else if(moveVertical > 0){
			// Set the up animation.
			anim.SetInteger("Direction", 1);
		}
	}

	public void EightDirectionAnimation(float moveHorizontal, float moveVertical, bool isMoving, Animator anim){
		// Manually set if this GameObject is moving for animation.
		anim.SetBool("IsMoving", isMoving);
		
		// IF we are going bottom right - Direction 8.
		// ELSE IF we are going bottom left - Direction 7.
		// ELSE IF we are going top left - Direction 6.
		// ELSE IF we are going top right - Direction 5.
		// ELSE IF we are going right - Direction 4.
		// ELSE IF we are going down - Direction 3.
		// ELSE IF we are going left - Direction 2.
		// ELSE IF we are going up - Direction 1.
		if(moveHorizontal > 0 && moveVertical < 0){
			// Set the down right animation.
			anim.SetInteger("Direction", 8);
			
		}else if(moveHorizontal < 0 && moveVertical < 0){
			// Set the down left animation.
			anim.SetInteger("Direction", 7);
			
		}else if(moveHorizontal < 0 && moveVertical > 0){
			// Set the up left animation.
			anim.SetInteger("Direction", 6);
			
		}else if(moveHorizontal > 0 && moveVertical > 0){
			// Set the up right animation.
			anim.SetInteger("Direction", 5);
			
		}else if(moveHorizontal > 0){
			// Set the right animation.
			anim.SetInteger("Direction", 4);
			
		}else if(moveVertical < 0){
			// Set the down animation.
			anim.SetInteger("Direction", 3);
			
		}else if(moveHorizontal < 0){
			// Set the left animation.
			anim.SetInteger("Direction", 2);
			
		}else if(moveVertical > 0){
			// Set the up animation.
			anim.SetInteger("Direction", 1);
		}
	}

	public void DebugErrorCheck(int choice, System.Type script, GameObject _gameObject){

		switch(choice){
		// One of the camera scripts was placed on a GameObject that does not have a Camera Component.
		case 0:
			Debug.Log("The '" + _gameObject.name + "' GameObject does not have a Camera Component attached to it.  " +
			          "Remove the '" + script + "' that is attached to your '" + _gameObject.name + " GameObject' and " +
			          "attach it to your Camera GameObject. ", _gameObject);
			break;
		// A empty 'Player Tag'.
		case 1:
			Debug.Log("The 'Player Tag' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Tag that is associated with your Player GameObject for the 'Player Tag' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// Setting the Camera Width on a camera script is less than or equal to 0.
		case 2:
			Debug.Log("The 'Width' is set less than or equal to 0 in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Change the 'Width' in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject to the desired width of your Camera.", _gameObject);
			break;
		// Setting the Camera Width on a camera script is less than or equal to 0.
		case 3:
			Debug.Log("The 'Height' is set less than or equal to 0 in your '" + script + "' that is attached on your '" + _gameObject.name + "' GameObject.  " +	
			          "Change the 'Height' in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject to the desired height of your Camera.", _gameObject);
			break;
		// A camera slide speed that is set to 0, so the camera will not Pan if the player touches the edge of the camera.
		case 4:
			Debug.Log("The 'Camera Slide Speed' is set at 0 in your '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
			          "This will result in the camera not panning when you touch the edges of the camera.  " +
			          "Set the 'Camera Slide Speed' in the '" + script + "' greater than 0 to get a desired effect.  " +
			          "If this is intended ignore this message.", _gameObject);
			break;
		// The Bottom Camera Border is not set and is equal to null.
		case 5:
			Debug.Log("The 'Bottom Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Select a GameObject (Bottom Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Bottom Camera Border.'  " +
			          "If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
			          "If not having a boundary is intended then ignore this message", _gameObject);
			break;
		// The Top Camera Border is not set and is equal to null.
		case 6:
			Debug.Log("The 'Top Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
			          "Select a GameObject (Top Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Top Camera Border.'  " +
			          "If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
			          "If not having a boundary is intended then ignore this message", _gameObject);
			break;
		// The Left Camera Border is not set and is equal to null.
		case 7:
			Debug.Log("The 'Left Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
			          "Select a GameObject (Left Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Left Camera Border.'  " +
			          "If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders.  " +
			          "If not having a boundary is intended then ignore this message", _gameObject);
			break;
		// The Right Camera Border is not set and is equal to null.
		case 8:
			Debug.Log("The 'Right Camera Border' is not assigned and equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " +
			          "Select a GameObject (Right Tile, Collider or Empty GameObject) for your chosen boundary in your scene for 'Right Camera Border.'  " +
			          "If your ground tile consist of 1 large tile (Same size or bigger than the camera dimensions.) then apply that tile to all Camera Borders." +
			          "If not having a boundary is intended then ignore this message", _gameObject);
			break;
		// The Top Camera Border is lower than the Bottom Camera Border.
		case 9:
			Debug.Log("The 'Top Camera Border' is lower than the 'Bottom Camera Border' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  ", _gameObject);
			break;
		// The Right Camera Border is lower than the Left Camera Border.
		case 10:
			Debug.Log("The 'Right Camera Border' is lower than the 'Left Camera Border' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  ", _gameObject);
			break;


		// A empty 'On Enter Activation Tag'
		case 20:
			Debug.Log("The 'On Enter Activation Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Tag(s) that you want to activate this script in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Exit Activation Tag'
		case 21:
			Debug.Log("The 'On Exit Activation Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Tag(s) that you want to activate this script in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Enter Parent' Array.
		case 22:
			Debug.Log("The 'On Enter Parents' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
		          	  "Put the Parent GameObject(s) that you want so the Children can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Exit Parent' Array.
		case 23:
			Debug.Log("The 'On Exit Parents' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Parent GameObject(s) that you want so the Children can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Enter Destroy Tag' Array.
		case 24:
			Debug.Log("The 'On Enter Destroy Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the tag of the GameObject(s) that you want so they can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Exit Destroy Tag' Array.
		case 25:
			Debug.Log("The 'On Exit Destroy Tag' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the tag of the GameObject(s) that you want so they can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Enter GameObjects Destroy' Array.
		case 26:
			Debug.Log("The 'On Enter GameObjects Destroy' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the GameObject(s) you want so they can be manually destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'On Exit GameObjects Destroy' Array.
		case 27:
			Debug.Log("The 'On Exit GameObjects Destroy' Array is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the GameObject(s) you want so they can be manually destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// Both 'OnEnter' and 'OnExit' are set to false which makes this script do nothing, so notify the user.
		case 29:
			Debug.Log("Both 'OnEnter' and 'OnExit' are set to false which makes this script do nothing in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set 'OnEnter' and/or 'OnExit' equal to TRUE for this script to work unless you plan on turning this on manually at a later time then ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;


		// A empty 'Target Tags'.
		case 30:
			Debug.Log("The 'Target Tags' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Tag that you want to activate the Scene Change on Collision in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'New Scene' when transferring, for Scene Change.
		case 31:
			Debug.Log("The 'New Scene' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the Scene Name you want to go to in 'New Scene' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A null 'New Location' for the Teleport Script.
		case 32:
			Debug.Log("The 'New Location' is currently 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign a Location so GameObject(s) can be teleported in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// A empty 'Destroyable By Tags'.
		case 33:
			Debug.Log("The 'Destroyable By Tags' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign the Tags for 'Destroyable By Tags' so that this GameObject can be destroyed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'HP' was initially set to 0.
		case 34:
			Debug.Log("The 'HP' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'HP' greater than 0 in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'moveSpeed' is set to 0.
		case 35:
			Debug.Log("The 'moveSpeed' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'moveSpeed' greater than 0 so the GameObject can move in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The Four Transform directions are null.
		case 36:
			Debug.Log("The 'up', 'down', 'left' and 'right' are equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign a minimum of 1 side so the GameObject can be moved in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;


		// The initial player GameObject to spawn is null.
		case 40:
			Debug.Log("The 'Player' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign your player prefab for the 'Player' GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'Scene Spawn Location' is initially null.
		case 41:
			Debug.Log("The 'Scene Spawn Location' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign Transform locations that you want your player to spawn for the start of the game/scene and/or scene change in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'canMove' is set to 0.
		case 42:
			Debug.Log("The 'canMove' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign 'canMoves' value greater than 0 so the GameObject can move, if this was intended ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'alterSpeed' is set to 0.
		case 43:
			Debug.Log("The 'alterSpeed' is initially set to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign 'alterSpeed' value greater than 0 so the GameObject can move, if this was intended ignore this message in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'terrainAnimation' is initially null.
		case 44:
			Debug.Log("The 'terrainAnimation' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'terrainAnimation' to the selected GameObject that you want to display for showing visuals in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'Sound Clip' is null.
		case 46:
			Debug.Log("Your 'Sound Clip' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set Sound Clip for this variable in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'Sound Clip' is null.
		case 47:
			Debug.Log("The 'Distance' is equal to 0 which may give unwanted sound results in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set a 'Distance' value greater than 0 in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;


		// The 'joltTags' is initially null and empty.
		case 50:
			Debug.Log("The 'joltTags' is equal to 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Put the tags that you want associated for 'joltTags' so the script will work in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The 'joltAmount' is equal to 0.
		case 51:
			Debug.Log("The 'joltAmount' is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'joltAmount' greater than 0 so the colliding GameObject will be jolted away in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The time the player is invulnerable after being damaged is set to 0.
		case 52:
			Debug.Log("The 'Invulnerability Time' is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'Invulnerability Time' greater than 0 so the GameObject will be immune to any colliding jolts in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		// The speed of the player is set to 0.
		case 60:
			Debug.Log("The 'speed' is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the 'speed' greater than 0 so you can move the GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user wants to see the collider in the scene view but forgot to set a collider.
		case 70:
			Debug.Log("The 'Range Collider' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the collider you wish to represent in your scene view to 'Range Collider' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;
		
		// The user did not set the dialogue box.
		case 71:
			Debug.Log("The 'Dialogue Box' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set a gameobject that you want to be your dialogue display for 'Dialogue Box' in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user did not set any dialogue.
		case 72:
			Debug.Log("The 'Dialogue' is currently empty in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set an array of strings that you want your Dialogue to display in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user did not set the Dialogue Image.
		case 73:
			Debug.Log("The 'Dialogue Image' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the Image component that is on your GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user did not set the Dialogue Text.
		case 74:
			Debug.Log("The 'Dialogue Text' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set the Text component that is found as a child of your Image GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user did not set an Icon.
		case 75:
			Debug.Log("The 'Icon' is 'null' in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set a GameObject that you wish to be your icon in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user did not set any locations in Point To Point.
		case 76:
			Debug.Log("The 'Locations' array is equal to 0 in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Set locations for the GameObject to move towards in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user user didn't set up the Action Key dialogue properly by not making 
		// this script be on a child gameobject of the main NPC gameobject.
		case 77:
			Debug.Log("The GameObject you have your Action Key Dialogue script on is not a child of the main NPC GameObject in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Create a child GameObject for your NPC GameObject and assign this script and the desired Collider2D for that GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// The user user didn't set up the Area Key dialogue properly by not making 
		// this script be on a child gameobject of the main NPC gameobject.
		case 78:
			Debug.Log("The GameObject you have your Area Key Dialogue script on is not a child of the main NPC GameObject in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Create a child GameObject for your NPC GameObject and assign this script and the desired Collider2D for that GameObject in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// There is a button to bring up the options menu but the Options Canvas is not assigned.
		case 100:
			Debug.Log("You have a button to bring up your options menu but no Options Canvas is assigned in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign a Canvas so your options menu panel can be displayed in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		// There is a button to bring up the options menu but the Options Panel Menu is not assigned.
		case 101:
			Debug.Log("You have a button to bring up your options menu but no Options Panel Menu is assigned in the '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.  " + 
			          "Assign a Panel to represent your options menu in '" + script + "' that is attached to your '" + _gameObject.name + "' GameObject.", _gameObject);
			break;

		default:
			Debug.Log ("Error in Debug Check for number: " + choice + "in " + script, _gameObject);
			break;
		}
	}
}
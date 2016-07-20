using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NPC_Manager))]
public class Point_To_Point_Movement : MonoBehaviour {
	
	[Header("Point To Point Locations")]
	[Tooltip("The order in which the GameObject will be moving towards, once the end is reached it will traverse backwards through the array.")]
	public Transform[] Locations;
	[Header("Object Speed")]
	[Tooltip("The speed at which the GameObject will be moving.")]
	public float Speed = 1f;
	[Header("Pause Time")]
	[Tooltip("The minimum time the GameObject waits before going to the next 'Location'.")]
	public float WaitTimeMin = 3f;
	[Tooltip("The maximum time the GameObject waits before going to the next 'Location'.")]
	public float WaitTimeMax = 3f;

	private int _locationIndex;
	private Transform _transform;
	private bool forward = true;
	private NPC_Manager _npcManager;


	void Awake(){
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
	}

	void Start () {
		// Set the index to 0 for the start.
		_locationIndex = 0;
		// Grab the transform for more efficiency.
		_transform = gameObject.transform;
		// Grab the manager of this gameobject.
		_npcManager = GetComponent<NPC_Manager>();
		// Lets start moving!
		StartCoroutine(GoToNextLocation());
	}

	void DebugCheck(){
		// IF the locations were not set.
		if(Locations.Length == 0){
			// Say the locations are not set.
			Helper_Manager.instance.DebugErrorCheck(76, this.GetType(), gameObject);
		}
	}

	private IEnumerator GoToNextLocation(){
		while(true){
			// WHILE the current position has not reached the next position.
			while(_transform.position != Locations[_locationIndex].position){
				// IF we are not pausing the movement.
				if(_npcManager.NPCState.CanNPCMove){
					// Move towards the next point.
					_transform.position = Vector2.MoveTowards(_transform.position, Locations[_locationIndex].position, Speed * Time.deltaTime);
				}
				yield return null;
			}
			// IF we are moving forward in the array,
			// ELSE we are moving backwards in the array.
			if(forward){
				// Increase the index.
				_locationIndex++;
			}else{
				// Decrease the index.
				_locationIndex--;
			}
			// IF we are at the end of the array,
			// ELSE IF we are at the start of the array.
			if((_locationIndex + 1 == Locations.Length)){
				// Time to move backwards in the array.
				forward = false;
			}else if((_locationIndex == 0)){
				// Time to move forward in the array.
				forward = true;
			}
			// Wait somewhere inbetween the min and max time.
			yield return new WaitForSeconds(Random.Range(WaitTimeMin, WaitTimeMax));
		}
	}
}
using UnityEngine;
using System.Collections;

public class WalkToPlayer : MonoBehaviour {

	public Transform aggro;
    public float speed;

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(transform.position, aggro.position);
		if (aggro != null && transform != null && distance >= 1.0f) {
            transform.position = Vector2.MoveTowards(transform.position, aggro.position, speed * Time.deltaTime);
        }
		/*else
        {
            Debug.Log("Something is null!");
        }*/
	}
}

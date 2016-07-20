using UnityEngine;
using System.Collections;

public class WalkToPlayer : MonoBehaviour {

    public Transform myTransform;
    private GameObject GOToGoto;
    public float speed;

	// Use this for initialization
	void Start () {
        GOToGoto = GameObject.Find("player");
    }
	
	// Update is called once per frame
	void Update () {
        GOToGoto = GameObject.Find("player");
        if (GOToGoto != null && myTransform != null) {
            Transform target = GOToGoto.transform;

            //if (Vector3.Distance(myTransform.position, target.position) > 1f) {
            //   myTransform.position += target.position * speed * Time.deltaTime;
            //}
            myTransform.position = Vector2.MoveTowards(myTransform.position, target.position, speed * Time.deltaTime);
        }else
        {
            Debug.Log("Something is null!");
        }
	}
}

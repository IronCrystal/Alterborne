using UnityEngine;
using System.Collections;

public class CheckHealth : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Stats statsScript = this.gameObject.GetComponent<Stats>();
        if (statsScript.currentHP <= 0) {
            Destroy(this.gameObject);
        }
    }
}

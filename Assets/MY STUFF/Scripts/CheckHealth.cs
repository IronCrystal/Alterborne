using UnityEngine;
using System.Collections;

public class CheckHealth : MonoBehaviour {
	Transform HPBar;
	int currentHP;
	float scale;
	Vector3 startScale;
	// Use this for initialization
	void Start () {
		HPBar = transform.GetChild (0);
		startScale = HPBar.localScale;
	}
	
	// Update is called once per frame
	void Update () {
        Stats statsScript = GetComponent<Stats>();

		scale = (float)(statsScript.currentHP) / (float)(statsScript.maxHP);
		//Debug.Log (HPBar.localScale);
		//Debug.Log (scale);

		//Debug.Log (HPBar.lossyScale);
		HPBar.localScale = new Vector3(scale * startScale.x, startScale.y, startScale.z);// = new Vector3(scale * currentScale.x, currentScale.y, currentScale.z);

        if (statsScript.currentHP <= 0) {
            Destroy(this.gameObject);
        }
    }
}

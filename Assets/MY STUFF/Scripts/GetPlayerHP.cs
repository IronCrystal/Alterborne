using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetPlayerHP : MonoBehaviour {

    public Slider slider;
	// Use this for initialization
	void Start () {
        GameObject thePlayer = GameObject.Find("player");
        if (thePlayer != null) {
            Stats statsScript = thePlayer.GetComponent<Stats>();
            if (slider != null) {
                slider.maxValue = statsScript.maxHP;
                slider.value = statsScript.currentHP;
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        GameObject thePlayer = GameObject.Find("player");
        if (thePlayer != null) {
            Stats statsScript = thePlayer.GetComponent<Stats>();
            if (slider != null) {
                slider.maxValue = statsScript.maxHP;
                slider.value = statsScript.currentHP;
            }
        }
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GetManaDescription : MonoBehaviour {

    public Text display;
    public Slider slider;
	// Use this for initialization
	void Start () {
	    if (slider != null && display != null) {
            display.text = slider.value + "/" + slider.maxValue;
        }
	}
	
	// Update is called once per frame
	void Update () {
        if (slider != null && display != null) {
            display.text = slider.value + "/" + slider.maxValue;
        }
    }
}

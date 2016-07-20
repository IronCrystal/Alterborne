using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(RectTransform))]
public class Dialogue : MonoBehaviour {

	public Image dialogueImage;
	public Text dialogueText;

	private Text _dialogueText;
	private Color _dialogueColor;
	private Color _textColor;
	private float _initialDialogueUIAlpha;
	private float _initialDialogueTextAlpha;
	private Vector2 _initialDialogueScale;

	void Awake(){
		// Set the txt initially to "".
		dialogueText.text = "";
		// Grab the initial dialogue UI alpha.
		_initialDialogueUIAlpha = dialogueImage.color.a;
		// Grab the initial dialogue text alpha.
		_initialDialogueTextAlpha = dialogueText.color.a;
		// Grab the initial dialogue scale.
		_initialDialogueScale = dialogueImage.transform.localScale;
	}

	void Start(){
		// Check to make sure the user has the scripts working correctly.
		DebugCheck();
    }

	void DebugCheck(){
		// IF user has did not set the Dialogue Image. 
		if(dialogueImage == null){
			Helper_Manager.instance.DebugErrorCheck(73, this.GetType(), gameObject);
		}
		// IF the user did not set the Dialogue Text.
		if(dialogueText == null){
			Helper_Manager.instance.DebugErrorCheck(74, this.GetType(), gameObject);
		}
	}

	public void SetDialogueUIColors(Color diaColor, Color txtColor){
		// Assign new colors to private variables.
		_dialogueColor = diaColor;
		_textColor = txtColor;

		// Set the dialogue and text colors.
		dialogueImage.color = _dialogueColor;
		dialogueText.color = _textColor;
	}

	public void SetDialogueUIAlpha(float diaAlpha){
		// When we create the dialogue box/text colors we always start with alpha at 0.
		_dialogueColor.a = diaAlpha;
		// Set the dialogue and text colors.
		dialogueImage.color = _dialogueColor;

	}

	public void SetDialogueTextAlpha(float txtAlpha){
		_textColor.a = txtAlpha;
		dialogueText.color = _textColor;
	}

	public void SetInitialDialogueUIAlpha(){
		_dialogueColor.a = _initialDialogueUIAlpha;
		dialogueImage.color = _dialogueColor;
	}
	
	public void SetInitialDialogueTextAlpha(){
		_textColor.a = _initialDialogueTextAlpha;
		dialogueText.color = _textColor;
	}

	public void SetDialogueScale(float diaXScale, float diaYScale){
		gameObject.transform.localScale = new Vector2(diaXScale, diaYScale);
	}

	public void SwitchText(string newText){
		// Change the dialogueText to newText.
		dialogueText.text = newText;
	}

	public IEnumerator FadeInImage(float fadeTime){
		// Fade in the dialogue image.
		yield return StartCoroutine(Helper_Manager.instance.FadeImage(dialogueImage, fadeTime, 0f, _initialDialogueUIAlpha));
	}

	public IEnumerator FadeInText(float textTimeSpan, string txt){
		// Start the Fade of the text.
		yield return StartCoroutine(Helper_Manager.instance.FadeText(dialogueText, textTimeSpan, 0f, _initialDialogueTextAlpha));
	}

	public IEnumerator FadeOutImage(float timeSpan){
		// Fade out the dialogue image.
		yield return StartCoroutine(Helper_Manager.instance.FadeImage(dialogueImage, timeSpan, dialogueImage.color.a, 0f));
	}

	public IEnumerator FadeOutText(float timeSpan){
		// Fade out the dialogue text.
		yield return StartCoroutine(Helper_Manager.instance.FadeText(dialogueText, timeSpan, dialogueText.color.a, 0f));
	}

	public IEnumerator TypeText(float textPauseTime, string txt, AudioClip typeSound){
		// Type out the dialogue text.
		yield return StartCoroutine(Helper_Manager.instance.TypeText(dialogueText, textPauseTime, txt, typeSound));
	}
	
	public IEnumerator Grow(float resizeTime, string txt){
		// IF the dialogue image scale IS NOT the initial size
		if((Vector2)dialogueImage.transform.localScale != _initialDialogueScale){
			// Grow the dialogue
			yield return StartCoroutine(Helper_Manager.instance.GrowShrinkImage(dialogueImage, resizeTime, dialogueImage.transform.localScale.x, dialogueImage.transform.localScale.y, _initialDialogueScale.x, _initialDialogueScale.y));
		}
	}

	public IEnumerator Shrink(float shrinkTime){
		// Shrink the dialogue.
		yield return StartCoroutine(Helper_Manager.instance.GrowShrinkImage(dialogueImage, shrinkTime, dialogueImage.transform.localScale.x, dialogueImage.transform.localScale.y, 0f, 0f));
		// Assign the text to "".
		SwitchText("");
	}
}
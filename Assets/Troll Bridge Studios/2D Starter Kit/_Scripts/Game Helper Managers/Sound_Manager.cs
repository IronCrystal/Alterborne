using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (AudioSource))]
public class Sound_Manager : MonoBehaviour {

	// Used for singleton.
	static Sound_Manager _instance = null;

	// The Background music to be used for any manipulations.
	[Tooltip("The background music AudioSource.  Drag the AudioSource in the inspector that is attached to this GameObject to this variable.")]
	public AudioSource bgMusicSource;
	
	// Allow the user to decide if music should be on or off.
	[Tooltip("If you want music to play when a scene starts.")]
	public bool musicOn = true;
	// Allow the user to decide if sound should be on or off.
	[Tooltip("If you want sound effects to play.")]
	public bool sfxOn = true;
	
	// The sound volume.
	[Range(0,1)]
	[Tooltip("The volume of the sound effects.")]
	public float sfxVolume = 0.5f;

	// The music slider that is used for dictating the volume of the music.
	private Slider musicSlider;
	// the music toggle that is used for muting and unmuting the music.
	private Toggle muteMusicToggle;
	// The sfx slider that is used for dictating the volume of the sfx.
	private Slider sfxSlider;
	// The sfx toggle that is used for muting and unmuting the sfx.
	private Toggle muteSFXToggle;
	

	public static bool isActive { 
		get { 
			return _instance != null; 
		} 
	}
	
	public static Sound_Manager instance {
		get {
			if (_instance == null) {
				_instance = Object.FindObjectOfType(typeof(Sound_Manager)) as Sound_Manager;
				
				if (_instance == null) {
					GameObject go = new GameObject("_Sound_Manager");
					DontDestroyOnLoad(go);
					_instance = go.AddComponent<Sound_Manager>();
				}
			}
			return _instance;
		}
	}

	void OnValidate(){
		// When this is changed, also change the Audio Source settings.
		ValidateMusic(musicOn);
	}

	void Awake(){
		// Load saved settings from Player Preferences. (soon)

		// Don't destroy this object on scene changes.
		DontDestroyOnLoad(gameObject);
	}

	// Stop the Music.
	void NoMusic(){
		// Stop the Music.
		bgMusicSource.Stop();
	}

	void ValidateMusic(bool inspectorMusic){
		// IF we want to play music.
		// ELSE we dont want to play music.
		if(inspectorMusic){
			bgMusicSource.mute = false;
		}else{
			bgMusicSource.mute = true;
		}
	}

	void SetToggles(){
		// IF we have a mute music toggle in place.
		if(muteMusicToggle != null){
			// IF the music is on.
			// ELSE the music is off.
			if(musicOn){
				muteMusicToggle.isOn = false;
			}else{
				muteMusicToggle.isOn = true;
			}
		}

		// IF we have a mute sfx toggle in place.
		if(muteSFXToggle != null){
			// IF the music is on.
			// ELSE the music is off.
			if(sfxOn){
				muteSFXToggle.isOn = false;
			}else{
				muteSFXToggle.isOn = true;
			}
		}
	}

	// Set all the sliders and toggles for the sound.
	public void SetSlidersAndToggles(Slider musSlider, Toggle musToggle, Slider soundSlide, Toggle soundToggle){
		musicSlider = musSlider;
		muteMusicToggle = musToggle;
		sfxSlider = soundSlide;
		muteSFXToggle = soundToggle;

		if (musicSlider != null) {
			musicSlider.value = bgMusicSource.volume;
		}

		if (sfxSlider != null) {
			sfxSlider.value = sfxVolume;
		}

		// Set the mute toggles.
		SetToggles();

	}

	// Mute or UnMute the background song.
	public void MuteUnMuteBGMusic(bool muteMusic){
		// IF we want to mute music.
		// ELSE we want to hear music.
		if(muteMusic){
			bgMusicSource.mute = true;
			musicOn = false;
		}else{
			bgMusicSource.mute = false;
			musicOn = true;
		}
	}
	
	// Mute or UnMute the sounds.
	public void MuteUnMuteSound(bool muteSFX){
		if(muteSFX){
			sfxOn = false;
		}else{
			sfxOn = true;
		}
	}

	// Change the volume of the music.
	public void ChangeMusicVolume(float newVol){
		// Set the new volume for the inspector.
		bgMusicSource.volume = newVol;
		// IF the user has the Music Slider Assigned.
		if(musicSlider != null){
			// Assign the music slider.
			musicSlider.value = newVol;
		}
	}
	
	// Change the volume of the SFX.
	public void ChangeSFXVolume(float newVol){
		// Set the new volume for the inspector.
		sfxVolume = newVol;

		// IF the user has the SFX Slider Assigned.
		if(sfxSlider != null){
			// Assign the sfx slider.
			sfxSlider.value = newVol;
		}
	}

	// Play a background song.
	public void PlayBGMusic(AudioClip song){
		// IF the clip playing is the same as the one requested.
		// ELSE IF the song is null then we do not play any music on this scene.
		if(bgMusicSource.clip == song){
			// The same song is playing so continue on what it was doing.
			return;
		}else if(song == null){
			// Stop the music!
			NoMusic();
			return;
		}

		// Stop the song that is currently playing.
		NoMusic();
		// Assign the background song.
		bgMusicSource.clip = song;
		// Loop the background music.
		bgMusicSource.loop = true;
		// Play it.
		bgMusicSource.Play();
	}

	// Play a sound.
	public AudioSource PlaySound(AudioClip sfx, Vector3 location, float minPitch, float maxPitch){
		// IF the sounds is muted we do nothing.
		if(!sfxOn){
			return null;
		}
		// Create a temp host for the Audio.
		GameObject tempSfxHost = new GameObject("TempSfx");
		// Set the sound location.
		tempSfxHost.transform.position = location;
		// Add AudioSource.
		AudioSource audioSource = tempSfxHost.AddComponent<AudioSource>() as AudioSource;
		// Set the Audio Clip the sfx.
		audioSource.clip = sfx;
		// Set the volume of the sfx.
		audioSource.volume = sfxVolume;

		// Random pitch if desired.
		audioSource.pitch = Random.Range(minPitch, maxPitch);

		// Play Sound.
		audioSource.Play();
		// Destroy the sfx based on the length.
		Destroy(tempSfxHost, sfx.length);
		// Send back the audioSource incase we need to manipulate it.
		return audioSource;
	}
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour {

	public Vector3 position = new Vector3(1000f,1000f,1000f); // initial position
	public Vector3 resetPosition = new Vector3(1000f,1000f,1000f);
	public float lightHighIntensity = 0.25f; //high light
	public float lightLowIntensity = 0f;
	public float fadeSpeed = 7f;
	public float musicFadeSpeed = 1f;

	private AlarmLight alarmScript;  //alarmlight's object
	private Light mainLight;
	private AudioSource music; //background music
	private AudioSource panicAudio;  // when role in dangerous situation, playing such music
	private AudioSource[] sirens;    //alarm music
	private const float muteVolume = 0f; //sound off
	private const float normalVolume = 0.8f; //normal volume

	private void Awake(){

		alarmScript = GameObject.FindWithTag (Tags.AlarmLight).GetComponent<AlarmLight> ();
		mainLight = GameObject.FindWithTag (Tags.MainLight).GetComponent<Light> ();
		music = GetComponent<AudioSource> ();
		panicAudio = GameObject.Find ("secondary_music").GetComponent<AudioSource> ();
		GameObject[] sirenGameObjects = GameObject.FindGameObjectsWithTag (Tags.Siren);
		sirens = new AudioSource[sirenGameObjects.Length];

		for (int i = 0; i < sirens.Length; ++i) {

			sirens [i] = sirenGameObjects [i].GetComponent<AudioSource> ();
		}
	}

	private void Update(){

		SwitchAlarms ();
		MusicFading ();
	}

	private void SwitchAlarms(){

		alarmScript.alarmOn = (position != resetPosition);
		float newIntensity;

		if (position != resetPosition)
			newIntensity = lightLowIntensity;
		else
			newIntensity = lightHighIntensity;

		mainLight.intensity = Mathf.Lerp (mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

		for (int i = 0; i < sirens.Length; ++i) {

			if (position != resetPosition && sirens [i].isPlaying)
				sirens [i].Play ();
			else if (position == resetPosition)
				sirens [i].Stop ();
		}
	}

	private void MusicFading(){

		if (position != resetPosition) {

			music.volume = Mathf.Lerp (music.volume, muteVolume, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, normalVolume, musicFadeSpeed * Time.deltaTime);
		} else {

			music.volume = Mathf.Lerp (music.volume, normalVolume, musicFadeSpeed * Time.deltaTime);
			panicAudio.volume = Mathf.Lerp (panicAudio.volume, muteVolume, musicFadeSpeed * Time.deltaTime);
		}
	}
}

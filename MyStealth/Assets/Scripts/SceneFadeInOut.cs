using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneFadeInOut : MonoBehaviour {

	public float fadeSpeed = 1.5f;
	private bool sceneStarting = true;
	private RawImage rawImage = null;

	private void Awake(){

		rawImage = GetComponent<RawImage> ();
	}

	private void Update(){

		if (sceneStarting) {

			StartScene ();
		}
	}

	private void FadeToClear(){

		rawImage.color = Color.Lerp (rawImage.color, Color.clear, fadeSpeed * Time.deltaTime);
	}

	private void FadeBlack(){

		rawImage.color = Color.Lerp (rawImage.color, Color.black, fadeSpeed * Time.deltaTime);
	}

	private void StartScene(){

		FadeToClear ();
		if (rawImage.color.a <= 0.05f) {

			rawImage.color = Color.clear;
			rawImage.enabled = false;
			sceneStarting = false;
		}
	}

	public void EndScene(){

		rawImage.enabled = true;
		FadeBlack ();

		if (rawImage.color.a >= 0.95f) {

			Application.LoadLevel (0);
		}
	}
}

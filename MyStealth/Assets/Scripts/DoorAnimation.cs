using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAnimation : MonoBehaviour {

	public bool requireKey;
	public AudioClip doorSwitchClip;
	public AudioClip accessDeniedClip;

	private Animator anim;
	private HashID hash;
	private GameObject player;
	private PlayerInventory playerInventory;
	private int count;

	private void Awake(){

		anim = GetComponent<Animator> ();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashID> ();
		player = GameObject.FindWithTag (Tags.Player);
		playerInventory = player.GetComponent<PlayerInventory> ();
	}

	private void Update(){

		anim.SetBool (hash.openBool, count > 0);
		AudioSource audio = GetComponent<AudioSource> ();

		if (anim.IsInTransition (0) && !audio.isPlaying) {

			audio.clip = doorSwitchClip;
			audio.Play ();
		}

	}

	private void OnTriggerEnter(Collider other){

		AudioSource audio = GetComponent<AudioSource> ();

		if (other.gameObject == player) {

			if (requireKey) {

				if (playerInventory.hashKey)
					++count;
				else {

					audio.clip = accessDeniedClip;
					audio.Play ();
				}
			} else
				++count;
		} else if (other.gameObject.tag == Tags.Enemy) {

			if (other is CapsuleCollider)
				++count;
		}
	}

	private void OnTriggerExit(Collider other){

		if (other.gameObject == player || (other.gameObject.tag == Tags.Enemy && other is CapsuleCollider))
			count = Mathf.Max (0, count - 1);
	}
		
}

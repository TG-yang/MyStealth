using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour {

	public AudioClip keyGrab; //当角色拿到钥匙卡时播放音频

	private GameObject player;
	private PlayerInventory playerInventory;

	private void Awake(){

		player = GameObject.FindWithTag (Tags.Player);
		playerInventory = player.GetComponent<PlayerInventory> ();
	}

	private void OnTriggerEnter(Collider other){

		if (other.gameObject == player) {

			AudioSource.PlayClipAtPoint (keyGrab, transform.position);
			playerInventory.hashKey = true;
			Destroy (gameObject);
		}
	}
}

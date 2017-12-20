using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPlayerDetection : MonoBehaviour {

	private GameObject player;
	private LastPlayerSighting lastPlayerSighting;

	private void Start(){

		player = GameObject.FindWithTag (Tags.Player);
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).GetComponent<LastPlayerSighting> ();
	}

	private void OnTriggerStay(Collider other){

		if(GetComponent<Renderer>().enabled){

			if (other.gameObject == player)
				lastPlayerSighting.position = other.transform.position;
		}
	}
}

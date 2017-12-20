using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	public float smooth = 1.5f;

	private Transform player;
	private Vector3 relCameraPos;
	private float relCameraPosMag; //the distnace between player and main_camera
	private Vector3 newPos;

	private void Awake(){

		player = GameObject.FindWithTag (Tags.Player).transform;
		relCameraPos = transform.position - player.position;
		relCameraPosMag = relCameraPos.magnitude - 0.5f;
	}

	private void FixedUpdate(){

		Vector3 standardPos = player.position + relCameraPos;
		Vector3 abovePos = player.position + Vector3.up * relCameraPosMag;

		Vector3[] checkPoints = new Vector3[5];
		checkPoints [0] = standardPos;
		checkPoints [1] = Vector3.Lerp (standardPos, abovePos, 0.25f);
		checkPoints [2] = Vector3.Lerp (standardPos, abovePos, 0.5f);
		checkPoints [3] = Vector3.Lerp (standardPos, abovePos, 0.75f);
		checkPoints [4] = abovePos;

		for(int i = 0; i<checkPoints.Length;++i){

			if (ViewingPositionCheck (checkPoints [i]))
				break;
		}

		transform.position = Vector3.Lerp (transform.position, newPos, smooth * Time.deltaTime);

		SmoothLookAt ();
	}

	//检测给定位置是否合适 
	private bool ViewingPositionCheck(Vector3 checkPos){

		RaycastHit hit;

		if (Physics.Raycast (checkPos, player.position - checkPos, out hit, relCameraPosMag)) {

			if (hit.transform != player)
				return false;
		}

		newPos = checkPos;
		return true;
	}

	private void SmoothLookAt(){

		Vector3 relPlayerPosition = player.position - transform.position;
		Quaternion lookAtRotation = Quaternion.LookRotation (relPlayerPosition, Vector3.up);
		transform.rotation = Quaternion.Lerp (transform.rotation, lookAtRotation, smooth * Time.deltaTime);
	}
}

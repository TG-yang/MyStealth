using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSwitchDeactivation : MonoBehaviour {

	public GameObject laster;//激光栅栏对象，由外部指定
	public Material unlockedMat; //表示锁开纹理
	public GameObject player; //角色对象

	private void LaserDeactivation(){

		laster.SetActive (false);
		Renderer screen = transform.Find ("prop_switchUnit_screen").GetComponent<Renderer> ();
		screen.material = unlockedMat;
		GetComponent<AudioSource> ().Play ();
	}

	private void OnTriggerStay(Collider other){

		if (other.gameObject == player) {

			if (Input.GetButton ("Switch"))
				LaserDeactivation ();
		}
	}
}

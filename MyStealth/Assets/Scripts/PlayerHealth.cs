using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public float health = 100f;
	public float resetAfterDeathTime = 5f;
	public AudioClip deathClip;

	private Animator anim;
	private PlayerMovement playerMovement;
	private HashID hash;
	private SceneFadeInOut sceneFadeInOut;
	private LastPlayerSighting lastPlayerSighting;
	private float timer;
	private bool playerDead;

	private void Awake(){

		anim = GetComponent<Animator> ();
		playerMovement = GetComponent<PlayerMovement> ();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashID> ();
		sceneFadeInOut = GameObject.FindWithTag (Tags.Fader).GetComponent<SceneFadeInOut> ();
		lastPlayerSighting = GameObject.FindWithTag (Tags.GameController).GetComponent<LastPlayerSighting> ();
	}

	private void Update(){

		if (health <= 0f) {

			if (!playerDead)
				PlayerDying ();
			else {
				PlayerDeath ();
				LevelReset ();
			}
		}
	}
	//player垂死状态
	private void PlayerDying(){

		playerDead = true;
		anim.SetBool (hash.deadBool, playerDead);
		AudioSource.PlayClipAtPoint (deathClip, transform.position);
	}
	//player死亡
	private void PlayerDeath(){

		if (anim.GetCurrentAnimatorStateInfo (0).nameHash == hash.dyingState) {

			anim.SetBool (hash.deadBool, false);
		}

		anim.SetFloat (hash.speedFloat, 0f);
		playerMovement.enabled = false;
		lastPlayerSighting.position = lastPlayerSighting.resetPosition;
		GetComponent<AudioSource> ().Stop ();
	}

	private void LevelReset(){

		timer += Time.deltaTime;
		if (timer >= resetAfterDeathTime)
			sceneFadeInOut.EndScene ();
	}

	public void TakeDamage(float amount){

		health -= amount;
	}
}

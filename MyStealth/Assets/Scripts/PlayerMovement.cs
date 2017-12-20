using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	public AudioClip shoutingClip;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	private Animator animator;
	private HashID hash;

	private void Awake(){

		animator = GetComponent<Animator> ();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashID> ();
		animator.SetLayerWeight (1, 1f);//设置序号为1的动画层权重为1f 即shouting动画层 1f
	}

	private void Update(){

		bool shout = Input.GetButtonDown ("Attract");
		animator.SetBool (hash.shoutingBool, shout);
		AudioManagement (shout);
	}

	private void FixedUpdate(){

		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool sneak = Input.GetButton ("Sneak");
		MovementManagement (h, v, sneak);
	}

	private void Rotating(float h,float v){

		Vector3 targetDir = new Vector3 (h, 0, v);
		Quaternion targetRotation = Quaternion.LookRotation (targetDir, Vector3.up);//创建一个相对于Player对象targetDir为前进方向 Vector3.up为垂直向上方向的旋转量
		Rigidbody r = GetComponent<Rigidbody> ();
		Quaternion newRotation = Quaternion.Lerp (r.rotation, targetRotation, turnSmoothing * Time.deltaTime);
		r.MoveRotation (newRotation);
	}

	private void MovementManagement(float h,float v,bool sneaking){

		animator.SetBool (hash.sneakingBool, sneaking);

		if (h != 0 || v != 0) {

			Rotating (h, v);
			animator.SetFloat (hash.speedFloat, 5.5f, speedDampTime, Time.deltaTime);
		} else {

			animator.SetFloat (hash.speedFloat, 0f);
		}
	}

	private void AudioManagement(bool shout){

		AudioSource audioSource = GetComponent<AudioSource> ();

		if (animator.GetCurrentAnimatorStateInfo (0).nameHash == hash.locomotionState) {
			//在AnimatorController 中的layer(这里是0层）中获取当前状态的信息，返回值
			if (!audioSource.isPlaying)
				audioSource.Play ();
		} else
			audioSource.Stop ();

		if (shout) {

			AudioSource.PlayClipAtPoint (shoutingClip, transform.position);
		}
	}
}

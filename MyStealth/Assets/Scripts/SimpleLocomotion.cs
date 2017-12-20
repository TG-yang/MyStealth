using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLocomotion {

	public float speedDampTime = 0.1f; //表示Animator.SetFloat中参数Speed达到目标值的时间
	public float angularSpeedDampTime = 0.7f;//表示Animator.SetFloat中参数AngularSpeed达到目标值的时间
	public float angleResponseTime = 0.6f;//表示计算角速度时，变化角度angle所需的时间
	private Animator anim;
	private HashID hash;

	public SimpleLocomotion(Animator animator,HashID hashIDs){
		anim = animator;
		hash = hashIDs;
	}

	public void Do(float speed,float angle){

		float angularSpeed = angle / angleResponseTime;
		anim.SetFloat (hash.speedFloat, speed, speedDampTime, Time.deltaTime);
		anim.SetFloat (hash.angularSpeedFloat, angularSpeed, angularSpeedDampTime, Time.deltaTime);
	}
}

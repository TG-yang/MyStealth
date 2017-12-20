using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMovement : MonoBehaviour {

//	public float deadZone = 5f;					// The number of degrees for which the rotation isn't controlled by Mecanim.
//
//
//	private Transform player;					// Reference to the player's transform.
//	private EnemySight enemySight;			// Reference to the EnemySight script.
//	private UnityEngine.AI.NavMeshAgent nav;					// Reference to the nav mesh agent.
//	private Animator anim;						// Reference to the Animator.
//	private HashID hash;					// Reference to the HashIDs script.
//	//private AnimatorSetup animSetup;		// An instance of the AnimatorSetup helper class.
//	private SimpleLocomotion locomotion;
//
//	void Awake ()
//	{
//		// Setting up the references.
//		player = GameObject.FindGameObjectWithTag(Tags.Player).transform;
//		enemySight = GetComponent<EnemySight>();
//		nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
//		anim = GetComponent<Animator>();
//		hash = GameObject.FindGameObjectWithTag(Tags.GameController).GetComponent<HashID>();
//
//		// Making sure the rotation is controlled by Mecanim.
//		nav.updateRotation = false;
//
//		// Creating an instance of the AnimatorSetup class and calling it's constructor.
//		locomotion = new SimpleLocomotion(anim,hash);
//
//		// Set the weights for the shooting and gun layers to 1.
//		anim.SetLayerWeight(1, 1f);
//		anim.SetLayerWeight(2, 1f);
//
//		// We need to convert the angle for the deadzone from degrees to radians.
//		deadZone *= Mathf.Deg2Rad;
//	}
//
//
//	void Update () 
//	{
//		// Calculate the parameters that need to be passed to the animator component.
//		NavAnimSetup();
//	}
//
//
//	void OnAnimatorMove()
//	{
//		// Set the NavMeshAgent's velocity to the change in position since the last frame, by the time it took for the last frame.
//		nav.velocity = anim.deltaPosition / Time.deltaTime;
//
//		// The gameobject's rotation is driven by the animation's rotation.
//		transform.rotation = anim.rootRotation;
//	}
//
//
//	void NavAnimSetup ()
//	{
//		// Create the parameters to pass to the helper function.
//		float speed;
//		float angle;
//
//		// If the player is in sight...
//		if(enemySight.playerInSight)
//		{
//			// ... the enemy should stop...
//			speed = 0f;
//
//			// ... and the angle to turn through is towards the player.
//			angle = FindAngle(transform.forward, player.position - transform.position, transform.up);
//		}
//		else
//		{
//			// Otherwise the speed is a projection of desired velocity on to the forward vector...
//			speed = Vector3.Project(nav.desiredVelocity, transform.forward).magnitude;
//
//			// ... and the angle is the angle between forward and the desired velocity.
//			angle = FindAngle(transform.forward, nav.desiredVelocity, transform.up);
//
//			// If the angle is within the deadZone...
//			if(Mathf.Abs(angle) < deadZone)
//			{
//				// ... set the direction to be along the desired direction and set the angle to be zero.
//				transform.LookAt(transform.position + nav.desiredVelocity);
//				angle = 0f;
//			}
//		}
//
//		// Call the Setup function of the helper class with the given parameters.
//		locomotion.Do(speed, angle);
//	}
//
//
//	float FindAngle (Vector3 fromVector, Vector3 toVector, Vector3 upVector)
//	{
//		// If the vector the angle is being calculated to is 0...
//		if(toVector == Vector3.zero)
//			// ... the angle between them is 0.
//			return 0f;
//
//		// Create a float to store the angle between the facing of the enemy and the direction it's travelling.
//		float angle = Vector3.Angle(fromVector, toVector);
//
//		// Find the cross product of the two vectors (this will point up if the velocity is to the right of forward).
//		Vector3 normal = Vector3.Cross(fromVector, toVector);
//
//		// The dot product of the normal with the upVector will be positive if they point in the same direction.
//		angle *= Mathf.Sign(Vector3.Dot(normal, upVector));
//
//		// We need to convert the angle we've found from degrees to radians.
//		angle *= Mathf.Deg2Rad;
//
//		return angle;
//	}

	public float deadZone = 5f;

	private Transform player;
	private EnemySight enemySight;
	private NavMeshAgent nav;
	private Animator anim;
	private HashID hash;
	private SimpleLocomotion locomotion;

	private void Awake(){

		player = GameObject.FindWithTag (Tags.Player).transform;
		enemySight = GetComponent<EnemySight> ();
		nav = GetComponent<NavMeshAgent> ();
		anim = GetComponent<Animator> ();
		hash = GameObject.FindWithTag (Tags.GameController).GetComponent<HashID> ();

		nav.updatePosition = false;//表示不允许Nav Mesh Agent 更新Enemy001对象的方向
		locomotion = new SimpleLocomotion (anim, hash);
		anim.SetLayerWeight (1, 1f);
		anim.SetLayerWeight (2, 1f);
		deadZone*=Mathf.Deg2Rad;//将deadZone从角度转到弧度
	}

	private void Update(){

		NavAnimSetup ();
	}

	private void OnAnimatorMove(){

		nav.velocity = anim.deltaPosition / Time.deltaTime;//anim.deltaPosition 表示在前一帧Animator移动的距离 除以帧间隔时间为速度
		transform.rotation = anim.rootRotation;
	}

	private float FindAngle(Vector3 fromVector, Vector3 toVector, Vector3 upVector){

		if (toVector == Vector3.zero)
			return 0f;
		float angle = Vector3.Angle (fromVector, toVector);
		Vector3 normal = Vector3.Cross (fromVector, toVector);
		angle *= Mathf.Sign (Vector3.Dot (normal, upVector));
		angle *= Mathf.Deg2Rad;

		return angle;
	}

	private void NavAnimSetup(){

		float speed;
		float angle;

		if (enemySight.playerInSight) {

			speed = 0f;
			angle = FindAngle (transform.forward, player.position - transform.position, transform.up);
		} else {

			speed = Vector3.Project (nav.desiredVelocity, transform.forward).magnitude;
			angle = FindAngle (transform.forward, nav.desiredVelocity, transform.up);

			if (Mathf.Abs (angle) < deadZone) {

				transform.LookAt (transform.position + nav.desiredVelocity);
				angle = 0f;
			}
		}
		locomotion.Do (speed, angle);
	}
}

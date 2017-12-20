using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBlinking : MonoBehaviour {

	public float onTime;  //间隔onTime后灭灯
	public float offTime; // 间隔offTime后亮灯

	private float timer;  //记录流逝的时间
	private Renderer laserRender; //Laser对象上的Renderer组件对象 即Laser上Light组件 上添加的Point Light
	private Light laserLight;

	private void Awake(){

		laserRender = GetComponent<Renderer> ();
		laserLight = GetComponent<Light> ();
		timer = 0f;
	}

	private void Update(){

		timer += Time.deltaTime;
		if (laserRender.enabled && timer >= onTime)
			SwitchBeam ();
		else if (!laserRender.enabled && timer >= offTime)
			SwitchBeam ();
	}

	private void SwitchBeam(){

		timer = 0f;
		//切换renderer组件和light组件的enabled
		laserRender.enabled =! laserRender.enabled;
		laserLight.enabled =! laserLight.enabled;
	}
}

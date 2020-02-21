using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
	[SerializeField] float distMul = 2f;
	[SerializeField] float speedMul = 2f;
	[Space]
	[SerializeField] float rotSpeed = 180f; //180* per sec

	static bool autoMove = false; // was tired of draging each instance to UI Toggle
	public bool AutoMove { 
		set {
			autoMove = value;
		}
	}
	static bool autoRotate = false;
	public bool AutoRotate
	{
		set
		{
			autoRotate = value;
		}
	}

	Vector3 iPos;
	Vector3 iDir;
	private void OnEnable()
	{
		iPos = transform.position;
		iDir = transform.right;
	}
	private void Update()
	{
		if (autoMove)
		{
			var mult = Mathf.Sin(Time.time * speedMul) * distMul;
			transform.position = iPos + iDir * mult;
		}
		if (autoRotate)
		{
			transform.Rotate(Vector3.forward * rotSpeed * Time.deltaTime);
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))] //Just in my case
public class RayReflector : MonoBehaviour
{
	[SerializeField] LineRenderer lineRend;
	[SerializeField] [Range(0.05f, 0.5f)] float rayWidth = 0.2f;
	[SerializeField] int maxBounce = 6;
	void Awake()
	{
		if (lineRend == null)
			lineRend = GetComponent<LineRenderer>();
		lineRend.startWidth = rayWidth;
	}

	//Almsot forgot you wanna do rotation :D
	public void Set_Rotation(float z)
	{
		transform.eulerAngles = Vector3.forward * z;
	}

	public List<Vector3> RayPath2D(Vector2 origin, Vector2 dir)
	{
		List<Vector3> path = new List<Vector3>(); //V3? Thanks to line Renderer -_-!
		path.Add(origin);

		int count = 0;
		RaycastHit2D hitInfo;
		while (count < maxBounce)
		{
			hitInfo = Physics2D.Raycast(origin, dir);
			if (!hitInfo) 
				break;

			dir = Vector2.Reflect(dir, hitInfo.normal);
			origin = hitInfo.point + (dir * 0.001f); //To avoid Multiple Collision with same collider
			path.Add(origin);
			++count;
		}
		path.Add(origin + dir * 20F); //1E4F maybe, float.MaxValue or something that make sense
		return path;
	}

	//just for the show of realtime, "RayPath2D" should be triggered when needed
	public void LateUpdate()
	{
		var path = RayPath2D(transform.position, transform.up);
		lineRend.positionCount = path.Count;
		lineRend.SetPositions(path.ToArray());
	}

#if UNITY_EDITOR
	[Header("Editor Test")]
	[SerializeField] bool visualizeRay = false;
	[SerializeField] bool continuous = false;
	private void OnDrawGizmos() //was using OnValidate, but this works better for me
	{
		if (visualizeRay && lineRend != null)
		{
			if (!continuous)
				visualizeRay = false;

			lineRend.startWidth = rayWidth;
			lineRend.endWidth = rayWidth;
			LateUpdate();
		}
	}
#endif
}
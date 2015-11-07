using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	//oh wow this code is so awful
	public static CameraFollow inst;

	void Start()
	{
		inst = this;
	}

	public void UpdatePos(Vector3 pos)
	{
		this.transform.position = new Vector3 (pos.x, pos.y, -10);
	}
}

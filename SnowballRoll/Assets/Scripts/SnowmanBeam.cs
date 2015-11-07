using UnityEngine;
using System.Collections;

public class SnowmanBeam : MonoBehaviour {

	public GameObject headAttach;

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Player") {
			PlayerMovement.inst.NeearrrooommmSnowmanBeam(headAttach.transform);
		}
	}
}

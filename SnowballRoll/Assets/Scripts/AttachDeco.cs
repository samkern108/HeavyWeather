using UnityEngine;
using System.Collections;

public class AttachDeco : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.transform.tag == "Player") {
			transform.parent = other.transform;
		}
	}

}

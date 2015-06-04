using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour {

	public static float speed = 5f;

	
	void Start ()
	{
		rigidbody.velocity = Vector3.left * speed;
	}

}

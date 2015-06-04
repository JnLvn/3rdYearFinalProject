using UnityEngine;
using System.Collections;

public class TestDestroyByContact : MonoBehaviour {

	void OnTriggerEnter(Collider other) 
	{
		Destroy(other.gameObject);
		Destroy(gameObject);
	}




}

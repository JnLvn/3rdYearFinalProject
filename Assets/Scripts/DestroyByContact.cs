﻿using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {
	
	private GameController gameController;

	void Start ()
	{
		GameObject gameControllerObject = GameObject.FindWithTag ("GameController");
		if (gameControllerObject != null)
		{
			gameController = gameControllerObject.GetComponent <GameController>();
		}
		if (gameController == null)
		{
			Debug.Log ("Cannot find 'GameController' script");
		}
	}
	
	void OnTriggerEnter(Collider other) 
	{
		if (other.tag == "Boundary")
		{
			return;
		}
		if (other.tag == "Ground")
		{
			return;
		}
		//Instantiate(explosion, transform.position, transform.rotation);
		if (other.tag == "Player")
		{
			//Instantiate(playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		}
		//gameController.AddScore (scoreValue);
		Destroy(other.gameObject);
		Destroy(gameObject);

	}

}

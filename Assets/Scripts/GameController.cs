using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float spawnWait;
	public float startWait;
	public float waveWait;
	private static int level = 0;

	public GameObject ground;
	public float spawnWait1;
	public float startWait1;
	public float waveWait1;

	public GUIText scoreText;
	public GUIText gameOverText;
	
	private static int score;
	private int highScore;
	private bool gameOver;
	//private bool restart;
	private string highScoreKey = "HighScore";

	public Mover mover;
	
	void Start ()
	{
		gameOverText.text = "";
		gameOver = false;
		//score = 0;
		UpdateScore ();
		StartCoroutine (SpawnWaves ());
		StartCoroutine (SpawnGround ());

		//Get the highScore from player prefs if it is there, 0 otherwise.
		highScore = PlayerPrefs.GetInt(highScoreKey,0);
	}

	void Update ()
	{
		/*if (restart)
		{
			if (Input.GetKeyDown (KeyCode.R))
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}*/
	}
	
	IEnumerator SpawnWaves ()
	{
		yield return new WaitForSeconds (startWait);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (spawnValues.x,Random.Range( 0.5f, 3f), spawnValues.z);
				Quaternion spawnRotation = Quaternion.Euler(0f, 270f, 0f);
				Instantiate (hazard, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait);
			}
			yield return new WaitForSeconds (waveWait);

			level++;

			Mover.speed +=1f;

			if (gameOver)
			{

				break;
			}
			score++;
			UpdateScore();
			if(level == 3)
			{
				Application.LoadLevel("Test2");
			}
			if(level == 6)
			{
				Application.LoadLevel("Test3");
			}
			if(level == 9)
			{
				Application.LoadLevel("Test4");
			}
		}
	}

	IEnumerator SpawnGround()
	{
		yield return new WaitForSeconds (startWait1);
		while (true)
		{
			for (int i = 0; i < hazardCount; i++)
			{
				Vector3 spawnPosition = new Vector3 (spawnValues.x, Random.Range( 1.0f, 2.5f), spawnValues.z);
				Quaternion spawnRotation = Quaternion.identity;
				Instantiate (ground, spawnPosition, spawnRotation);
				yield return new WaitForSeconds (spawnWait1);
				
			}
			yield return new WaitForSeconds (waveWait1);

			if (gameOver)
			{
				
				break;
			}
		}
	}
	
	/*public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}*/

	void UpdateScore ()
	{
		scoreText.text = "Score: " + score;
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
	}

	void OnGUI()
	{
		const int buttonWidth = 100;
		const int buttonHeight = 50;
		
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
		Rect buttonRect = new Rect(0, 400, buttonWidth, buttonHeight);

		Rect buttonRect2 = new Rect (550, 400, buttonWidth, buttonHeight);
		
		// Draw a button to bring you to menu
		if(GUI.Button(buttonRect,"Menu"))
		{
			score = 0;
			level = 0;
			Mover.speed = 5f;
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("GameMenu");
		}
		// Draw a button to restart the game
		if(GUI.Button(buttonRect2,"Restart"))
		{
			score = 0;
			level = 0;
			Mover.speed = 5f;
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
			Application.LoadLevel("Test1");
		}
	}  
	
	void OnDisable()
	{
		//If our scoree is greter than highscore, set new higscore and save.
		if (score > highScore) {
				PlayerPrefs.SetInt (highScoreKey, score);
				PlayerPrefs.Save ();
		}
	}
}
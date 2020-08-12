using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**
 * This class represents the game manager, it will initialize and monitor the game process.
 */
public class GameManager : MonoBehaviour {

	// Hold a game manager object
	public static GameManager gm;

	// Hold a player object
	[Tooltip("If not set, the player will default to the gameObject tagged as Player.")]
	public GameObject player;

	// Use an enum to hold 4 different results of the player.
	public enum gameStates {Playing, Death, GameOver, BeatLevel};
	
	// Initialize the gameState to 'Playing'
	public gameStates gameState = gameStates.Playing;

	// Initialize the score to 0.
	public int score = 0;
	
	// Set boolean variable canBeatLevel1 to false(a flag).
	public bool canBeatLevel = false;
	// Initialize the beatLevelScore to zero.
	public int beatLevelScore = 0;

	// Initialize all fields we need later.
	public GameObject mainCanvas;
	public Text mainScoreDisplay;
	public Text gameOverScoreDisplay;
	public GameObject gameOverCanvas;
	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public GameObject beatLevelCanvas;
	public AudioSource backgroundMusic;
	public AudioClip gameOverSFX;
	[Tooltip("Only need to set if canBeatLevel is set to true.")]
	public AudioClip beatLevelSFX;
	private Health playerHealth;

	// Use this for initialization
	private void Start () {
		// Check if the game manager is set properly.
		if (gm == null) 
			gm = gameObject.GetComponent<GameManager>();

		// Check if the player is set properly.
		if (player == null) {
			player = GameObject.FindWithTag("Player");
		}

		// Set player's health.
		playerHealth = player.GetComponent<Health>();

		// setup score display
		Collect (0);

		// make other UI inactive
		gameOverCanvas.SetActive (false);
		if (canBeatLevel)
			beatLevelCanvas.SetActive (false);
	}

	// Update is called once per frame
	private void Update () {
		// Check gameState
		switch (gameState)
		{
			// If the player is now playing.
			case gameStates.Playing:
				// Check if the player is dead
				if (playerHealth.isAlive == false)
				{
					// update gameState
					gameState = gameStates.Death;

					// set the end game score
					gameOverScoreDisplay.text = mainScoreDisplay.text;

					// switch which GUI is showing
					// switch from mainCanvas to gameOver canvas
					mainCanvas.SetActive (false);
					gameOverCanvas.SetActive (true);
				}
				// Else if the player beats this level
				else if (canBeatLevel && score >= beatLevelScore) {
					// update gameState
					gameState = gameStates.BeatLevel;

					// hide the player so game doesn't continue playing
					player.SetActive(false);

					// switch which GUI is showing	
					// switch from mainCanvas to beatLevelCanvas
					mainCanvas.SetActive (false);
					beatLevelCanvas.SetActive (true);
				}
				break;
			
			// If the player is dead
			case gameStates.Death:
				// Background music volume keeping minus 0.01
				backgroundMusic.volume -= 0.01f;
				// If no music volume at all then play game over sound at game manager's position.
				if (backgroundMusic.volume <= 0.0f) {
					AudioSource.PlayClipAtPoint (gameOverSFX,gameObject.transform.position);
					// Set game state to GameOver.
					gameState = gameStates.GameOver;
				}
				break;
			
			// If the player beats the level
			case gameStates.BeatLevel:
				// Background music volume keeping minus 0.01
				backgroundMusic.volume -= 0.01f;
				if (backgroundMusic.volume <= 0.0f) {
					// If no music volume at all then play beat level sound at game manager's position.
					AudioSource.PlayClipAtPoint (beatLevelSFX,gameObject.transform.position);
					// Set game state to GameOver.
					gameState = gameStates.GameOver;
				}
				break;
			case gameStates.GameOver:
				// nothing
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}

	}


	/**
	 * Collect method to update player's current score.
	 */
	public void Collect(int amount) {
		// Update score with coins' numbers
		score += amount;
		
		if (canBeatLevel) {
			// Exhibit the current number of coins.
			mainScoreDisplay.text = score.ToString () + " of "+beatLevelScore.ToString ();
		} else
			mainScoreDisplay.text = score.ToString();

	}
}

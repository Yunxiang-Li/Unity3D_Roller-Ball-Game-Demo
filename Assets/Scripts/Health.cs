using System;
using UnityEngine;
using System.Collections;

/**
 * This is a class that represents the ball's actions according to its health.
 */
public class Health : MonoBehaviour {
	
	// Use a enum to store 2 different dead situations.
	public enum deathAction {loadLevelWhenDead,doNothingWhenDead};
	
	// Initialize health points and re-spawn health points.
	public float healthPoints = 1f;
	public float respawnHealthPoints = 1f;		
	// Initialize player's lives
	public int numberOfLives = 1;					
	// Use a flag to
	public bool isAlive = true;	

	// Initialize a GameObject to store the explosionPrefab
	public GameObject explosionPrefab;
	
	// Initialize the deathAction to doing nothing.
	public deathAction onLivesGone = deathAction.doNothingWhenDead;
	
	// Initialize level to load
	public string LevelToLoad = "";
	
	// Declare respawn settings.
	private Vector3 respawnPosition;
	private Quaternion respawnRotation;
	

	// Use this for initialization
	private void Start () 
	{
		// store initial position as respawn location
		respawnPosition = transform.position;
		respawnRotation = transform.rotation;
		
		// default to current scene 
		if (LevelToLoad == "") 
		{
			// load level one
			LevelToLoad = Application.loadedLevelName;
		}
	}
	
	// Update is called once per frame
	private void Update ()
	{
		// if the player is still alive, do nothing.
		if (healthPoints > 0) 
			return; 
		
		// decrement # of lives, update lives GUI
		numberOfLives--;					
		
		// Check if we can instantiate the explosion prefab.
		if (explosionPrefab != null) {
			Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		}
		
		// Re spawn the ball since the player still has lives.
		if (numberOfLives > 0) { 
			// reset the player to re spawn position
			transform.position = respawnPosition;	
			transform.rotation = respawnRotation;
			// give the player full health again
			healthPoints = respawnHealthPoints;	
		} 
		// here is where you do stuff once ALL lives are gone
		else { 
			isAlive = false;
				
			// Doing different actions according to the value of onLivesGone
			switch(onLivesGone)
			{
				// Load specific level again immediately
				case deathAction.loadLevelWhenDead:
					Application.LoadLevel (LevelToLoad);
					break;
				// do nothing, death must be handled in another way elsewhere
				case deathAction.doNothingWhenDead:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
			// Destroy the ball
			Destroy(gameObject);
		}
	}
	
	/**
	 * When the ball collides with enemy or death zone, minus specific (amount) health points.
	 */
	public void ApplyDamage(float amount)
	{	
		healthPoints -= amount;	
	}
	
	/**
	 * When the ball collides with something that can heal, plus specific (amount) health points.
	 * No healing things in this demo D:.
	 */
	public void ApplyHeal(float amount)
	{
		healthPoints += amount;
	}
	
	/**
	 * When the ball collides with something that can add bonus life, plus specific (amount) amount of lives.
	 * No bonus lives in this demo D:.
	 */
	public void ApplyBonusLife(int amount)
	{
		numberOfLives += amount;
	}
	
}

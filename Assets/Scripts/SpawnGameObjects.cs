using UnityEngine;
using System.Collections;

/**
 * This class is used to spawn objects(coins and enemies in this game).
 */
public class SpawnGameObjects : MonoBehaviour {

	// Store the GameObject that we want to spawn.
	public GameObject spawnPrefab;

	// Initialization for the range of interval time of spawning.
	public float minSecondsBetweenSpawning = 3.0f;
	public float maxSecondsBetweenSpawning = 6.0f;
	
	// Store the object to be chased
	public Transform chaseTarget;
	
	// Store time data to spawn correctly.
	private float savedTime;
	private float secondsBetweenSpawning;

	// Use this for initialization
	private void Start () {
		// Store the beginning time.
		savedTime = Time.time;
		// Get first round of game's spawning interval time.
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}
	
	// Update is called once per frame
	private void Update ()
	{	
		// Check if it is the time to spawn the next object.
		if (!(Time.time - savedTime >= secondsBetweenSpawning)) 
			return;
		// If yes, spawn the object.
		MakeThingToSpawn();
		
		// Store current time for next spawn
		savedTime = Time.time; 
		// Get next round of game's spawning interval time.
		secondsBetweenSpawning = Random.Range (minSecondsBetweenSpawning, maxSecondsBetweenSpawning);
	}

	/**
	 * Spawn a spawnPrefab object as a game object and set its chasing target.
	 */
	private void MakeThingToSpawn()
	{
		// create a new gameObject to store the spawnPrefab.
		var clone = Instantiate(spawnPrefab, transform.position, transform.rotation) as GameObject;
	}
}

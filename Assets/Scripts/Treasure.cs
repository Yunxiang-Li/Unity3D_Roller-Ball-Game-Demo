using UnityEngine;
using System.Collections;

/**
 * This class deals with actions when treasure(coins) collides with player.
 */
public class Treasure : MonoBehaviour {

	// Initialize treasure's value and explosion prefab.
	public int value = 10;
	public GameObject explosionPrefab;

	/**
	 * Run if the treasure collides with others.
	 */
	private void OnTriggerEnter (Collider other)
	{
		// If the collider is not tagged as player then do nothing.
		if (other.gameObject.tag != "Player")
			return;
		
		// Tell the game manager to Collect
		if (GameManager.gm!=null)
		{
			GameManager.gm.Collect (value);
		}
			
		// Explode if specified
		if (explosionPrefab != null) {
			Instantiate (explosionPrefab, transform.position, Quaternion.identity);
		}
			
		// destroy after collection
		Destroy (gameObject);
	}
}

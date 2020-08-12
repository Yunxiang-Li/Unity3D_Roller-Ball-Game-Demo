using UnityEngine;
using System.Collections;

/**
 * This class represents damage handle when player collides or triggers with something.
 */
public class Damage : MonoBehaviour {
	
	// Fields Initializations.
	public float damageAmount = 10.0f;
	public bool damageOnTrigger = true;
	public bool damageOnCollision = false;
	public bool continuousDamage = false;
	
	public float continuousTimeBetweenHits = 0;
	// variables dealing with exploding on impact (area of effect)
	public bool destroySelfOnImpact = false;	
	public float delayBeforeDestroy = 0.0f;
	public GameObject explosionPrefab;

	private float savedTime = 0;

	/**
	 * Called for things are triggers. 
	 */
	private void OnTriggerEnter(Collider collision)
	{
		// Check if the collider object is triggered.
		if (!damageOnTrigger) 
			return;
		
		// If the player got hit with it's own bullets, ignore it(No bullets in this game).
		if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")
			return;
		// Check if the hit object has the Health script
		if (collision.gameObject.GetComponent<Health>() == null)
			return; 
		// If the hit object has the Health script on it, deal damage
		collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
		
		// Destroy the object whenever it hits something if destroySelfOnImpact is set to true.
		if (destroySelfOnImpact) {
			// Destroy the current object after a specific delay time.
			Destroy (gameObject, delayBeforeDestroy);	  
		}
			
		// If no explosion prefab is set, just instantiate one with explosion prefab.
		if (explosionPrefab != null) {
			Instantiate (explosionPrefab, transform.position, transform.rotation);
		}
	}

	/**
	 * Called for things are collider type. 
	 */
	private void OnCollisionEnter(Collision collision) 						
	{
		if (!damageOnCollision) return;
		
		// If the player got hit with it's own bullets, ignore it(No bullets in this game).
		if (this.tag == "PlayerBullet" && collision.gameObject.tag == "Player")	
			return;
		
		// If the hit object has the Health script on it, deal damage
		if (collision.gameObject.GetComponent<Health>() == null)
			return; 
		// If the hit object has the Health script on it, deal damage
		collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
			
		// Destroy the object whenever it hits something if destroySelfOnImpact is set to true.
		if (destroySelfOnImpact) {
			// Destroy the current object after a specific delay time.
			Destroy (gameObject, delayBeforeDestroy);	  
		}
			
		// If no explosion prefab is set, just instantiate one with explosion prefab.
		if (explosionPrefab != null) {
			Instantiate (explosionPrefab, transform.position, transform.rotation);
		}
	}


	// Called for continuous damage.(No continuous damage in this game).
	private void OnCollisionStay(Collision collision) 
	{
		// If continuousDamage is not allowed, do nothing.
		if (!continuousDamage) return;
		
		// is only triggered if whatever it hits is the player
		if (collision.gameObject.tag != "Player" || collision.gameObject.GetComponent<Health>() == null)
			return; 
		
		if (!(Time.time - savedTime >= continuousTimeBetweenHits))
			return;
		savedTime = Time.time;
		
		// If the hit object has the Health script on it, deal damage
		collision.gameObject.GetComponent<Health> ().ApplyDamage (damageAmount);
	}
	
}
using UnityEngine;
using System.Collections;

//[RequireComponent(typeof(CharacterController))]

/**
 *  This class represents all chasing actions and settings.
 */
public class Chaser : MonoBehaviour {
	// Initialize chasing speed
	public float speed = 20.0f;
	public float minDist = 1f;
	public Transform target;

	// Use this for initialization
	private void Start ()
	{
		// If target is already specified, do nothing
		if (target != null)
			return;
		// If this is a game object with tag player
		if (GameObject.FindWithTag ("Player") != null)
		{
			// Then set this game object's transform as target.
			target = GameObject.FindWithTag ("Player").GetComponent<Transform>();
		}
	}
	
	// Update is called once per frame
	private void Update () 
	{
		// If target is null, do nothing.
		if (target == null)
			return;

		// Always face the target
		transform.LookAt(target);

		//Get the distance between the chaser and the target
		var distance = Vector3.Distance(transform.position,target.position);

		//So long as the chaser is farther away than the minimum distance, move towards it at rate speed.
		if(distance > minDist)	
			transform.position += transform.forward * speed * Time.deltaTime;	
	}

}

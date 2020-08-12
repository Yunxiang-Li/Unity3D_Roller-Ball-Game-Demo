using UnityEngine;
using System.Collections;

/**
 * This class will help destruct objects after a certain time.
 */
public class TimedObjectDestructor : MonoBehaviour {

	public float timeOut = 1.0f;
	public bool detachChildren = false;

	// Use this for initialization
	private void Awake () {
		// invoke the DestroyNow function to run after timeOut seconds
		Invoke ("DestroyNow", timeOut);
	}

	/**
	 * Destroy the game object(if it has children, detaches children first, no children in this game).
	 */
	private void DestroyNow ()
	{
		// detach the children before destroying if specified
		if (detachChildren) { 
			transform.DetachChildren();
		}
		DestroyObject (gameObject);
	}
}

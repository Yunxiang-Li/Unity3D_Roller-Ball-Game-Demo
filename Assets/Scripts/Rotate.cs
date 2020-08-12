using System;
using UnityEngine;
using System.Collections;

/**
 * This class represents the rotation of coins.
 */
public class Rotate : MonoBehaviour {
	
	// Initialize for the rotation speed.
	public float speed = 10.0f;
	
	// Enum for rotation direction.
	public enum whichWayToRotate {AroundX, AroundY, AroundZ}

	// Initialization the rotation of coin as AroundY.
	public whichWayToRotate way = whichWayToRotate.AroundY;

	// Update is called once per frame
	private void Update () {

		// Deal with different rotation direction situations
		switch(way)
		{
			// Rotate around X axis.
			case whichWayToRotate.AroundX:
				transform.Rotate(Vector3.right * Time.deltaTime * speed);
				break;
			// Rotate around Y axis.
			case whichWayToRotate.AroundY:
				transform.Rotate(Vector3.up * Time.deltaTime * speed);
				break;
			// Rotate around Z axis.
			case whichWayToRotate.AroundZ:
				transform.Rotate(Vector3.forward * Time.deltaTime * speed);
				break;
			default:
				throw new ArgumentOutOfRangeException();
		}	
	}
}
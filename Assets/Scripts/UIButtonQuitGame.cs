using UnityEngine;
using System.Collections;

/**
 * This class is used to quit the game after the player beat all levels.
 */
public class UIButtonQuitGame : MonoBehaviour {

	/**
	 * Quit the game.
	 */
	public void quitGame()
	{
		//Closes the game
		Application.Quit();
	}
}
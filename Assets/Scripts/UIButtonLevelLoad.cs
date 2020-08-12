using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

/**
 * This class is used to load the level after clicking a specific button.
 */
public class UIButtonLevelLoad : MonoBehaviour {
	
	// Store the level to load
	public string LevelToLoad;
	
	/**
	 * Load the specific level.
	 */
	public void loadLevel() {
		//Load the level from LevelToLoad
		SceneManager.LoadScene(LevelToLoad);
	}
}

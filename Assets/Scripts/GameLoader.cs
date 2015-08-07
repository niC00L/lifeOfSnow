using UnityEngine;
using System.Collections;

public class GameLoader : MonoBehaviour {

	public void continueGame() {
		Application.LoadLevel(1);
	}

	public void newGame() {
		Application.LoadLevel(1);
	}

	public void loadGame() {
		Application.LoadLevel(1);
	}

	public void settings() {
		Application.Quit();
	}

	public void exit() {
		Application.Quit();
	}
}

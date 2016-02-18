using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ExitScript : MonoBehaviour {

	public Canvas quitMenu;
	public Canvas HelpMenu;
	public Button startText;
	public Button exitText;
	public Button backText;
	public Button pauseButton;
	public Button returnButton;
	public Button quitToMenu;
	public Canvas pauseMenu;
	public Canvas gameScreen;
	public Canvas startScreen;

	// Use this for initialization
	void Start () {
		
		startScreen = startScreen.GetComponent<Canvas> ();
		gameScreen = gameScreen.GetComponent<Canvas> ();
		quitMenu = quitMenu.GetComponent<Canvas> ();
		startText = startText.GetComponent<Button> ();
		exitText = exitText.GetComponent<Button> ();
		HelpMenu = HelpMenu.GetComponent<Canvas> ();
		pauseButton = pauseButton.GetComponent<Button> ();
		pauseMenu = pauseMenu.GetComponent<Canvas> ();
		quitToMenu = quitToMenu.GetComponent<Button> ();
		returnButton = returnButton.GetComponent<Button> ();
		quitMenu.enabled = false;
		HelpMenu.enabled = false;
	
	}

	public void playGame(){
		startScreen.enabled = false;
		quitMenu.enabled = false;
		HelpMenu.enabled = false;
		pauseMenu.enabled = false;
		gameScreen.enabled = true;
	}

	public void pauseGame(){
		startScreen.enabled = false;
		gameScreen.enabled = false;
		pauseMenu.enabled = true;
	}

	public void returnGame(){
		startScreen.enabled = false;
		pauseMenu.enabled = false;
		gameScreen.enabled = true;
	}

	public void exitToMenu(){
		pauseMenu.enabled = false;
		gameScreen.enabled = false;
		startScreen.enabled = true;
	}

	public void exitPress(){
		quitMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void noPress(){
		quitMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}

	public void exitGame(){
		Application.Quit ();
	}

	public void helpPress(){
		HelpMenu.enabled = true;
		startText.enabled = false;
		exitText.enabled = false;
	}

	public void backPress(){
		HelpMenu.enabled = false;
		startText.enabled = true;
		exitText.enabled = true;
	}
}
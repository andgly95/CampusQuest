using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleBtnMngr : MonoBehaviour {

	[SerializeField]private string newLevel;
	[SerializeField]private string loadLevel;

	public void NewGameBtn(string newLevel){
		SceneManager.LoadScene (newLevel);
	}
	public void LoadGameBtn(string loadLevel){// <- have to find a way to get loadLevel to change basedon when the player saves
		SceneManager.LoadScene (loadLevel);
	}
	public void ExitGame(){
		Application.Quit (); // <-Closes the game. Won't do anything until the game is built though.
	}
}

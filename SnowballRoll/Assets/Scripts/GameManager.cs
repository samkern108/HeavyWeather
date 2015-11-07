using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	/*
	 * TO DO LIST FOR TOM :D
	 * 
	 * 1:  Remove the script from the snowball whose reference is missing.  
	 * 		I deleted it... but I don't want to fuck shit up so I left it on the prefab lol
	 * 
	 * 2:  Basic instructions text that displays for a few seconds when the game starts
	 * 
	 * 3:  Button to start the game again when gameover/victory happens
	 * 
	 * 4:  Button to quit the game when gameover/victory happens
	 */

	public static GameManager inst;

	public void GameOver()
	{
		PlayerMovement.inst.FreezeMovement ();
	}

	public void Victory()
	{
		PlayerMovement.inst.FreezeMovement ();
	}

	public void StartGame()
	{
		DisplayInstructions ();
		PlayerMovement.inst.StartOver ();
	}

	public void DisplayInstructions()
	{

	}
	
	void Start () {
		inst = this;
		StartGame ();
	}
}

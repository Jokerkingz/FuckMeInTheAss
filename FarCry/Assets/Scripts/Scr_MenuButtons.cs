using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_MenuButtons : MonoBehaviour {
	//public GameObject vPlayer;
	public GameObject vCanvas;
	// Use this for initialization
	
	// Update is called once per frame

	public void ExecuteButton(){
		Debug.Log ("Button Pressed");
		vCanvas.SendMessage ("StartGame");
	}
	public void QuitButton(){
		Application.Quit ();
	}
}

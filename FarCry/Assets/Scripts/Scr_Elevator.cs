using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scr_Elevator : MonoBehaviour {
	public bool vDoorsOpen = true;
	public bool vPlayerHere = false;
	public bool vMove = false;
	public int vFloor = 1;
	public float vCountDown;
	public Animator cANI;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vMove) {
			if (vCountDown > 0)
				vCountDown -= .03f;
			else {
				vMove = false;
				vCountDown = 0;
				cANI.SetBool ("Open", true);
				if (vPlayerHere) {
					Debug.Log ("Floor Changed");
					SceneManager.LoadScene (vFloor);
				}
				else
					Debug.Log ("Player is not here");
			}
		}
	}

	void OpenDoor(){
		cANI.SetBool ("Open", true);

	}

	void NextFloor(int tFloor){
		vMove = true;
		vCountDown = 10;
		cANI.SetBool ("Open", false);
		vFloor = tFloor;
		Debug.Log ("Going NextFLoor");
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_PlaySound : MonoBehaviour {
	public bool vPlay;
	public AudioSource vSound;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (vPlay) {
			vPlay = false;
			vSound.Play ();
		}
			
	}
}

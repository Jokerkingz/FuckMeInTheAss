using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerCamera : MonoBehaviour {
	public GameObject vPlayer;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider Other){
		if (Other.tag == "Player") {
			Other.GetComponent<Scr_Player> ().vActing = false;
			Other.GetComponent<Scr_Player> ().vIntro = false;
		}

	}
}

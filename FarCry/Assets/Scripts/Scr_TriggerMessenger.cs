using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerMessenger : MonoBehaviour {
	public GameObject vSource;
	private Scr_Elevator cSE;

	void Start(){
		cSE = vSource.GetComponent<Scr_Elevator> ();
	}

	void OnTriggerEnter(Collider Other){
		if (Other.tag == "Player") 
		{
			cSE.vPlayerHere = true;
			Debug.Log ("Player Here");
		}
	}


	void OnTriggerExit(Collider Other){
		if (Other.tag == "Player"){
			cSE.vPlayerHere = false;
			Debug.Log ("Player NOT Here");
		}

	}
}

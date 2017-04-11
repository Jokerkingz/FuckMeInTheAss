using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_TriggerDoors : MonoBehaviour {
	public GameObject vDoorLeft;
	public GameObject vDoorRight;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(){
		Debug.Log ("Door Open");
		vDoorLeft.GetComponent<Animation> ().Play ();
		vDoorRight.GetComponent<Animation> ().Play ();
	}
}

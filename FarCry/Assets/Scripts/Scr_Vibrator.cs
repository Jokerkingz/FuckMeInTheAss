using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_Vibrator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate (Random.Range (-5f, 5f), 0f, Random.Range (-5f, 5f));
	}
}

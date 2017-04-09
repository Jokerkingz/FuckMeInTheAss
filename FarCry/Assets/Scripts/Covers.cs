using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Covers : MonoBehaviour {

	public GameObject player;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 lookAtPosition = player.transform.position;
		lookAtPosition.y = transform.position.y;
		transform.LookAt (lookAtPosition);
	}
}
